using HNFactoryAutoSystem.Data.ProcessData;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using LinqToDB;

namespace HNFactoryAutoSystem.Data
{
    /// <summary>
    /// 读取数据库数据信息操作类
    /// </summary>
    public class DataHelper
    {

        #region 工业基础信息

        #region 工厂信息操作
        /// <summary>
        /// 获取工厂信息集合
        /// </summary>
        /// <returns></returns>
        public FactoryInfoCollection GetFactoryInfos()
        {
            try
            {
                FactoryInfoCollection infos = new FactoryInfoCollection();
                string strSQL = "select *from f_factoryinfo";
                List<MySqlParameter> sqlParameters = new List<MySqlParameter>();
                FactoryInfo info = new FactoryInfo();

                CreateDataList<FactoryInfo>(infos, strSQL, sqlParameters, info, null);

                return infos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 生产线数据操作


        /// <summary>
        /// 获取工厂的生产线信息
        /// </summary>
        /// <param name="strAssemblyLineId">生产线编号</param>
        /// <returns></returns>
        public AssemblyLineInfo GetAssemblyLine(string strAssemblyLineId)
        {
            try
            {
                DataModels.HnfactoryautodbDB dataContext = new DataModels.HnfactoryautodbDB();
                DataModels.FAssemblylineinfo data = dataContext.FAssemblylineinfoes.SingleOrDefault(c => c.AssemblyLineId == strAssemblyLineId);
                if(data != null)
                {
                    return new AssemblyLineInfo(data);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取工厂的生产线集合
        /// </summary>
        /// <param name="strFactoryId"></param>
        /// <returns></returns>
        public AssemblyLineInfoCollection GetAssemblyLineInfoCollection(string strFactoryId)
        {
            try
            {
                AssemblyLineInfoCollection infos = new AssemblyLineInfoCollection();
                string strSQL = "select *from v_assemblylineinfo where FactoryId=?FactoryId";
                List<MySqlParameter> sqlParameters = new List<MySqlParameter>
                {
                    new MySqlParameter("?FactoryId", strFactoryId)
                };
                AssemblyLineInfo info = new AssemblyLineInfo();

                CreateDataList<AssemblyLineInfo>(infos, strSQL, sqlParameters, info, null);

                return infos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取工厂指定工业的生产线集合
        /// </summary>
        /// <param name="strFactoryId"></param>
        /// <param name="strProcessId"></param>
        /// <returns></returns>
        public AssemblyLineInfoCollection GetAssemblyLineInfoCollection(string strFactoryId, string strProcessId)
        {
            try
            {
                AssemblyLineInfoCollection infos = new AssemblyLineInfoCollection();
                string strSQL = "select *from v_assemblylineinfo where FactoryId=?FactoryId and ProcessId=?ProcessId";
                List<MySqlParameter> sqlParameters = new List<MySqlParameter>
                {
                    new MySqlParameter("?FactoryId", strFactoryId),
                    new MySqlParameter("?ProcessId", strProcessId)
                };
                AssemblyLineInfo info = new AssemblyLineInfo();

                CreateDataList<AssemblyLineInfo>(infos, strSQL, sqlParameters, info, null);

                return infos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 设备基础信息操作

        /// <summary>
        /// 获取生产线的所有设备集合
        /// </summary>
        /// <param name="strAssemblyLineId"></param>
        /// <returns></returns>
        public DeviceInfoCollection GetAssemblyLineDevices(string strAssemblyLineId)
        {
            try
            {
                DeviceInfoCollection infos = new DeviceInfoCollection();
                string strSQL = "select *from v_assemblylinedevices where AssemblyLineId=?AssemblyLineId";
                List<MySqlParameter> sqlParameters = new List<MySqlParameter>
                {
                    new MySqlParameter("?AssemblyLineId", strAssemblyLineId)
                };
                DeviceInfo info = new DeviceInfo();

                CreateDataList<DeviceInfo>(infos, strSQL, sqlParameters, info, null);

                //按设备获取对应的传感器集合
                foreach(DeviceInfo item in infos)
                {
                    string strDeviceId = item.DeviceId;
                    SensorInfoCollection sensors = GetSensorInfos(strDeviceId);
                    item.Sensors = sensors;
                }

                return infos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 通过传感器得到对应的设备编号
        /// </summary>
        /// <param name="strSensorId">传感器编号</param>
        /// <returns></returns>
        public string GetDeviceIdBySensor(string strSensorId)
        {
            try
            {
                string strDeviceId = string.Empty;

                DataModels.HnfactoryautodbDB dataContext = new DataModels.HnfactoryautodbDB();
                DataModels.FSensorinfo data = dataContext.FSensorinfoes.SingleOrDefault(c => c.SensorId == strSensorId);
                if(data != null)
                {
                    strDeviceId = data.DeviceId;
                }

                return strDeviceId;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 传感器基础信息操作
        /// <summary>
        /// 获取设备的传感器列表
        /// </summary>
        /// <param name="strDeviceId"></param>
        /// <returns></returns>
        public SensorInfoCollection GetSensorInfos(string strDeviceId)
        {
            try
            {
                SensorInfoCollection infos = new SensorInfoCollection();
                string strSQL = "select * from f_sensorinfo where DeviceId=?DeviceId";
                List<MySqlParameter> sqlParameters = new List<MySqlParameter>
                {
                    new MySqlParameter("?DeviceId", strDeviceId)
                };
                SensorInfo info = new SensorInfo();

                Dictionary<string, Type> dEnum = new Dictionary<string, Type>();
                dEnum.Add("ParType", typeof(SysHelper.Enums.DeviceParameterType));


                CreateDataList<SensorInfo>(infos, strSQL, sqlParameters, info, dEnum);

                return infos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取传感器信息
        /// </summary>
        /// <param name="strSensorId">传感器编号</param>
        /// <returns></returns>
        public SensorInfo GetSensorInfo(string strSensorId)
        {
            try
            {
                DataModels.HnfactoryautodbDB dataContext = new DataModels.HnfactoryautodbDB();
                DataModels.FSensorinfo data = dataContext.FSensorinfoes.SingleOrDefault(c => c.SensorId == strSensorId);
                if (data != null)
                {
                    SensorInfo info = new SensorInfo(data);
                    return info;
                }
                else
                {
                    return null;
                }

            }catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #endregion

        #region 工业流程基础信息

        #region 工业主流程获取

        /// <summary>
        /// 获取工艺主流程
        /// </summary>
        /// <param name="strProcessId">主流程编号</param>
        /// <returns></returns>
        public TechnologicalProcess GetTechnologicalProcess(string strProcessId)
        {
            try
            {
                DataModels.HnfactoryautodbDB dataContext = new DataModels.HnfactoryautodbDB();
                DataModels.PMainprocess data = dataContext.PMainprocesses.SingleOrDefault(c => c.ProcessId == strProcessId);
                if (data != null)
                {
                    return new TechnologicalProcess(data);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取工业主流程集合
        /// </summary>
        /// <returns></returns>
        public TechnologicalProcessCollection GetTechnologicalProcesses()
        {
            try
            {
                TechnologicalProcessCollection infos = new TechnologicalProcessCollection();
                string strSQL = "select *from p_mainprocess";
                List<MySqlParameter> sqlParameters = new List<MySqlParameter>();
                TechnologicalProcess info = new TechnologicalProcess();

                CreateDataList<TechnologicalProcess>(infos, strSQL, sqlParameters, info, null);

                return infos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 工业子流程数据操作
        /// <summary>
        /// 获取工业主流程包含的子流程数据
        /// </summary>
        /// <param name="strProcessId"></param>
        /// <returns></returns>
        public TeExProcessCollection GetTeExProcesses(string strProcessId)
        {
            try
            {
                TeExProcessCollection infos = new TeExProcessCollection();

                //using (DataModels.HnfactoryautodbDB dataContext = new DataModels.HnfactoryautodbDB())
                //{
                //    var datas = (from c in dataContext.PExprocesses
                //                 where c.ProcessId == strProcessId
                //                 select c
                //                ).ToList();

                //    foreach(DataModels.PExprocess data in datas)
                //    {
                //        TeExProcess info = new TeExProcess();
                //        info.ProcessId = data.ProcessId;
                //        info.ExProcessId = data.ExProcessId;
                //        info.ExProcessTitle = data.ExProcessTitle;

                //        infos.Add(info);
                //    }

                //}


                string strSQL = "select *from p_exprocess where ProcessId=?ProcessId order by processOrderId";
                List<MySqlParameter> sqlParameters = new List<MySqlParameter>
                {
                    new MySqlParameter("?ProcessId", strProcessId)
                };
                TeExProcess info = new TeExProcess();
                Dictionary<string, Type> dEnum = new Dictionary<string, Type>();
                dEnum.Add("ParType", typeof(SysHelper.Enums.DeviceParameterType));

                //获取主数据
                CreateDataList<TeExProcess>(infos, strSQL, sqlParameters, info, dEnum);

                return infos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取工业子流程所有信息
        /// </summary>
        /// <param name="strExProcessId">工业子流程编号</param>
        /// <returns></returns>
        public TeExProcess GetTeExProcess(string strExProcessId)
        {
            try
            {
                TeExProcess item = new TeExProcess();
                string strSQL = "select *from p_exprocess where ExProcessId=?ExProcessId";
                List<MySqlParameter> sqlParameters = new List<MySqlParameter>
                {
                    new MySqlParameter("?ExProcessId", strExProcessId)
                };

                DataSet dataSet = MySqlHelper.GetDataSet(  CommandType.Text, strSQL, sqlParameters.ToArray());
                if (dataSet.Tables.Count > 0)
                {
                    DataTable table = dataSet.Tables[0];
                    if (table.Rows.Count > 0)
                    {
                        Dictionary<string, Type> dEnum = new Dictionary<string, Type>();
                        dEnum.Add("ParType", typeof(SysHelper.Enums.DeviceParameterType));
                        TeExProcessCollection infos = new TeExProcessCollection();

                        CreateDataList<TeExProcess>(infos, strSQL, sqlParameters, item, dEnum);
                        //item = EntityHelper.GetEntityListByDT<TeExProcess>(table.Rows[0], null);
                        item = infos[0];

                        #region 提前相关的步骤集合
                        ProcessStepCollection steps = GetProcessSteps(strExProcessId);
                        foreach (ProcessStep step in steps)
                        {
                            ExProcessStepParsCollection pars = GetExProcessStepPars(step.StepId);
                            step.StepPars = pars;
                        }
                        item.Steps = steps;
                        #endregion
                    }
                }


                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 按指定顺序编号获取对应的工艺子流程数据
        /// </summary>
        /// <param name="strProcessId"></param>
        /// <param name="iOrderIndex"></param>
        /// <returns></returns>
        public TeExProcess GetTeExProcess(string strProcessId,int iOrderIndex)
        {
            try
            {
               
                DataModels.HnfactoryautodbDB dataContext = new DataModels.HnfactoryautodbDB();
                //获取数据
                DataModels.PExprocess data = dataContext.PExprocesses.SingleOrDefault(c => c.ProcessId == strProcessId & c.ProcessOrderId == iOrderIndex);
                if(data != null)
                {
                    TeExProcess item = new TeExProcess(data);

                    //提取对应的子集合
                    string strExProcessId = item.ExProcessId;

                    ProcessStepCollection steps = GetProcessSteps(strExProcessId);
                    foreach (ProcessStep step in steps)
                    {
                        ExProcessStepParsCollection pars = GetExProcessStepPars(step.StepId);
                        step.StepPars = pars;
                    }
                    item.Steps = steps;

                    return item;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取子流程的步骤顺序集合
        /// </summary>
        /// <param name="strExProcessId"></param>
        /// <returns></returns>
        public ProcessStepCollection GetProcessSteps(string strExProcessId)
        {
            try
            {
                ProcessStepCollection items = new ProcessStepCollection();
                using (DataModels.HnfactoryautodbDB dataContext = new DataModels.HnfactoryautodbDB())
                {
                    var datas = (from c in dataContext.PExprocesssteps
                                 where c.ExProcessId == strExProcessId
                                 orderby c.OrderIndex
                                 select c
                                ).ToList();
                    foreach(DataModels.PExprocessstep data in datas)
                    {
                        ProcessStep item = new ProcessStep(data);
                        items.Add(item);
                    }
                }

                return items;

                //ProcessStepCollection infos = new ProcessStepCollection();
                //string strSQL = "select *from p_exprocessstep where ExProcessId=?ExProcessId order by orderindex";
                //List<MySqlParameter> sqlParameters = new List<MySqlParameter>
                //{
                //    new MySqlParameter("?ExProcessId", strExProcessId)
                //};
                //ProcessStep info = new ProcessStep();

                //Dictionary<string, Type> dEnum = new Dictionary<string, Type>();
                //dEnum.Add("ActionType", typeof(SysHelper.Enums.DeviceActionType));
                //dEnum.Add("FinishParType", typeof(SysHelper.Enums.DeviceParameterType));

                ////获取主数据
                //CreateDataList<ProcessStep>(infos, strSQL, sqlParameters, info, dEnum);

                //return infos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取子流程步骤的参数需求集合
        /// </summary>
        /// <param name="strStepId"></param>
        /// <returns></returns>
        public ExProcessStepParsCollection GetExProcessStepPars(string strStepId)
        {
            try
            {
                ExProcessStepParsCollection items = new ExProcessStepParsCollection();
                using (DataModels.HnfactoryautodbDB dataContext = new DataModels.HnfactoryautodbDB())
                {
                    var datas = (from c in dataContext.PExprocesssteppars
                                 where c.StepId == strStepId
                                 select c
                                ).ToList();
                    foreach (DataModels.PExprocesssteppar data in datas)
                    {
                        ExProcessStepPars item = new ExProcessStepPars(data);
                        items.Add(item);
                    }
                }

                return items;


                //ExProcessStepParsCollection infos = new ExProcessStepParsCollection();
                //string strSQL = "select *from p_exprocesssteppars where StepId=?StepId  ";
                //List<MySqlParameter> sqlParameters = new List<MySqlParameter>
                //{
                //    new MySqlParameter("?StepId", strStepId)
                //};
                //ExProcessStepPars info = new ExProcessStepPars();

                //Dictionary<string, Type> dEnum = new Dictionary<string, Type>();
                //dEnum.Add("ActionType", typeof(SysHelper.Enums.SenserStatusType));
                //dEnum.Add("ParType", typeof(SysHelper.Enums.DeviceParameterType));

                ////获取主数据
                //CreateDataList<ExProcessStepPars>(infos, strSQL, sqlParameters, info, dEnum);

                //return infos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #endregion



        #region 内部处理方法

        private void CreateDataList<T>(ListBase<T> infos, string strSQL, List<MySqlParameter> sqlParameters, T info, Dictionary<string, Type> dEnum)
        {

            //提取数据
            DataSet dataSet = MySqlHelper.GetDataSet( CommandType.Text, strSQL, sqlParameters.ToArray());
            if (dataSet.Tables.Count > 0)
            {
                DataTable table = dataSet.Tables[0];
                infos.ListToConvert(TableToObject<T>(table, info, dEnum));
            }
        }


        private List<TResult> TableToObject<TResult>(DataTable dt, TResult ob, Dictionary<string, Type> dEnum) //泛型方法,此处TResult为类型参数
        {
            List<PropertyInfo> prlist = new List<PropertyInfo>();//创建一个属性列表集合

            Type t = typeof(TResult); //获取实体对象的元数据Type类型

            PropertyInfo[] prArr = t.GetProperties(); //取得实体对象的所有属性到属性集合中

            foreach (PropertyInfo pr in prArr) //循环遍历属性集合到List集合
                prlist.Add(pr);
            //通过匿名方法自定义筛选条件  => 检查datatable中是否存在存在此列, 
            Predicate<PropertyInfo> prPredicate = delegate (PropertyInfo pr) { if (dt.Columns.IndexOf(pr.Name) != -1) return true; return false; };
            //从指定的条件中
            List<PropertyInfo> templist = prlist.FindAll(prPredicate);
            //创建一个实体集合
            List<TResult> oblist = new List<TResult>();
            //遍历DataTable每一行
            foreach (DataRow row in dt.Rows)
            {
                ob = (TResult)Activator.CreateInstance(t);  //通过Type类型创建对象,并强制转换成实体类型

                Action<PropertyInfo> prAction = delegate (PropertyInfo pr)
                {

                    if (dEnum != null)
                    {
                        string tempName = pr.Name;
                        object value = row[tempName];

                        var queryResult = from n in dEnum
                                          where n.Key == tempName
                                          select n;
                        //枚举集合中包含与当前属性名相同的项
                        if (queryResult.Count() > 0)
                        {
                            //将字符串转换为枚举对象
                            pr.SetValue(ob, Enum.Parse(queryResult.FirstOrDefault().Value, value.ToString()), null);

                        }
                        else
                        {
                            if (row[pr.Name] != DBNull.Value)
                            {
                                pr.SetValue(ob, row[pr.Name], null);
                            }
                        }
                    }
                    else
                    {
                        if (row[pr.Name] != DBNull.Value)
                        {
                            pr.SetValue(ob, row[pr.Name], null);
                        }
                    }
                };
                //把选择出来的属性集合的每一个属性设置成上面创建的对象的属性
                templist.ForEach(prAction);

                oblist.Add(ob); //把属性添加到实体集合
            }
            return oblist;
        }
        #endregion
    }
}
