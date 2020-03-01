using HNFactoryAutoSystem.Data;
using HNFactoryAutoSystem.Data.ProcessData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HNFactoryAutoSystem.Business
{
    /// <summary>
    /// 工业流程的业务操作方法
    /// </summary>
    public class ProcessBusiness
    {
        #region 操作设备的委托方法

        public delegate bool DeviceAction(string strDeviceId
                , string strSonserId
                , SysHelper.Enums.SenserStatusType senserStatusType
                , SysHelper.Enums.DeviceParameterType parameterType
                , decimal deSetValue);

        #endregion


        /// <summary>
        /// 生产线启动生产流程
        /// </summary>
        /// <param name="strAssemblyLineId"></param>
        public void StartProcess(string strAssemblyLineId, string strCreateUser, DeviceAction deviceAction)
        {
            try
            {
                //读取生产线数据
                DataHelper dataHelper = new DataHelper();
                DataLogHelper logHelper = new DataLogHelper();
                AssemblyLineInfo assembly = dataHelper.GetAssemblyLine(strAssemblyLineId);
                //线程休眠的基础单位
                int iSleepUnit = 1000;

                if (assembly != null)
                {
                    //获取生产线的所有设备信息
                    DeviceInfoCollection devices = dataHelper.GetAssemblyLineDevices(strAssemblyLineId);

                    //获取相应的工艺流程信息
                    TechnologicalProcess process = dataHelper.GetTechnologicalProcess(assembly.ProcessId);

                    DateTime dtMainStart = DateTime.Now;
                    #region 主工艺流程日志记录
                    ProcessLog proLog = new ProcessLog()
                    {
                        AssemblyLineId = strAssemblyLineId,
                        ProcessId = process.ProcessId,
                        Created = DateTime.Now,
                        CreateUser = strCreateUser,
                        ProcessStatus = SysHelper.Enums.ProcessStatusType.Start,
                        ProduceMaterialType = SysHelper.Enums.MaterialTypeEnum.Z,
                        Production = decimal.Zero,
                        TakeTime = 0
                    };
                    logHelper.AddMainProcessLog(proLog);
                    #endregion

                    //子工艺流程步骤数量
                    int iExProcessNum = 3;

                    #region 工艺子流程循环

                    for (int iExIndex = 1; iExIndex <= iExProcessNum; iExIndex++)
                    {

                        //提取子工艺流程审批环节
                        TeExProcess exProcess = dataHelper.GetTeExProcess(process.ProcessId, iExIndex);

                        if (exProcess != null)
                        {
                            //子工艺开始时间
                            DateTime dtExStart = DateTime.Now;
                            #region 子工艺流程日志记录

                            ExProcessLog exLog = new ExProcessLog()
                            {
                                ProcessLogId = proLog.ProcessLogId,
                                ExProcessId = exProcess.ExProcessId,
                                Created = DateTime.Now,
                                CreateUser = strCreateUser,
                                ProcessStatus = SysHelper.Enums.ProcessStatusType.Start,
                                ProduceMaterialType = SysHelper.Enums.MaterialTypeEnum.Z,
                                Production = decimal.Zero,
                                TakeTime = 0
                            };
                            //先记录数据
                            logHelper.AddExProcessLog(exLog);
                            #endregion

                            //是否达到启动条件
                            bool isStartExProcess = false;

                            #region 判断启动的设备参数是否达到启动条件

                            DeviceProduceLog deviceLog = logHelper.GetDeviceProduceLog(exProcess.StartDeviceId, exProcess.ParType);

                            if (deviceLog != null)
                            {
                                if (deviceLog.ParValue.Value >= exProcess.ParValue)
                                {
                                    isStartExProcess = true;
                                }
                            }

                            //循环等待达到启动条件
                            while (!isStartExProcess)
                            {
                                //每个循环先暂停10秒
                                int iStartSleep = iSleepUnit * 10;
                                System.Threading.Thread.Sleep(iStartSleep);

                                deviceLog = logHelper.GetDeviceProduceLog(exProcess.StartDeviceId, exProcess.ParType);
                                if (deviceLog != null)
                                {
                                    if (deviceLog.ParValue.Value >= exProcess.ParValue)
                                    {
                                        isStartExProcess = true;
                                    }
                                }
                            }

                            #endregion

                            if (isStartExProcess)
                            {
                                #region 循环启动设备

                                foreach (ExProcessStep step in exProcess.Steps)
                                {
                                    //先找到需要操作的设备
                                    foreach (DeviceInfo device in devices)
                                    {
                                        if (device.ProcessDeviceId == step.ProcessDeviceId)
                                        {
                                            string strDeviceId = device.DeviceId;
                                            //定义设备的操作类型
                                            //提取步骤的参数控制数
                                            foreach (ExProcessStepPars par in step.StepPars)
                                            {
                                                #region 记录操作的日志
                                                DeviceActionLog actionLog = new DeviceActionLog()
                                                {
                                                    DeviceId = strDeviceId,
                                                    Created = DateTime.Now,
                                                    CreateUser = strCreateUser,
                                                    SensorId = par.SensorId,
                                                    ActionType = par.ActionType,
                                                    ParType = par.ParType,
                                                    ParUnit = par.ParUnit,
                                                    ParValue = par.ParValue,
                                                    ToDeviceId = string.Empty,
                                                    ToSensorId = string.Empty
                                                };
                                                logHelper.AddDeviceActionLog(actionLog);

                                                #endregion

                                                //将操作放入委托里面进行执行(步骤执行完成才执行后续步骤)
                                                if (deviceAction(strDeviceId, par.SensorId, par.ActionType, par.ParType, par.ParValue))
                                                {
                                                    //判断是否需要等待
                                                    if (par.ParTime > 0)
                                                    {

                                                        int iSleepSet = par.ParTime * 60 * iSleepUnit;
                                                        System.Threading.Thread.Sleep(iSleepSet);
                                                    }

                                                    //判断是否是完成参数
                                                    if (par.IsFinish)
                                                    {

                                                    }
                                                }
                                            }
                                            break;
                                        }
                                    }
                                }

                                #endregion
                            }
                            //子工艺完成时间
                            DateTime dtExEnd = DateTime.Now;

                            TimeSpan tSpan = dtExEnd - dtExStart;
                            //得到子工艺完成花费的分钟
                            exLog.TakeTime = Convert.ToInt32(tSpan.TotalMinutes);
                            exLog.FinishTime = dtExEnd;
                            exLog.ProcessStatus = SysHelper.Enums.ProcessStatusType.End;
                            //exLog.ProduceMaterialType = SysHelper.Enums.MaterialTypeEnum.

                            logHelper.UpdateExProcessLog(exLog);
                        }
                    }

                    #endregion

                    DateTime dtMainEnd = DateTime.Now;
                    TimeSpan mSpan = dtMainEnd - dtMainStart;
                    proLog.TakeTime = Convert.ToInt32(mSpan.TotalMinutes);
                    proLog.FinishTime = dtMainEnd;
                    proLog.ProcessStatus = SysHelper.Enums.ProcessStatusType.End;
                    logHelper.UpdateMainProcessLog(proLog);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
