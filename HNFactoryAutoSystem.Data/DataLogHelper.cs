using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;

namespace HNFactoryAutoSystem.Data
{
    public class DataLogHelper
    {
        #region 工艺流程操作日志数据

        #region 工艺主流程日志

        /// <summary>
        /// 添加工艺主流程日志
        /// </summary>
        /// <param name="log">日志对象</param>
        /// <returns></returns>
        public bool AddMainProcessLog(ProcessData.ProcessLog log)
        {
            try
            {
                bool isAdd = false;
                if (log != null)
                {
                    //提取得到新的流水号
                    //{生产线编号}-{年月日}-{2位流水号}
                    //string strYMD = DateTime.Now.ToString("yyMMdd");
                    using (DataModels.HnfactoryautodbDB dataContext = new DataModels.HnfactoryautodbDB())
                    {
                        //先查询日志集合
                        string strSQL = "select count(*) from l_processlog where AssemblyLineId=?AssemblyLineId and DATE_FORMAT(created,'%Y%m%d') = ?NowDate";
                        List<MySqlParameter> sqlParameters = new List<MySqlParameter>
                        {
                            new MySqlParameter("?AssemblyLineId", log.AssemblyLineId)
                            ,new MySqlParameter("?NowDate",log.Created)
                        };
                        int iNowSum = Convert.ToInt32(MySqlHelper.ExecuteScalar(CommandType.Text, strSQL, sqlParameters.ToArray()));

                        string strNewLogCode = string.Format("{0}-{1:yyMMdd}-{2:00}", log.AssemblyLineId, log.Created, iNowSum);

                        log.ProcessLogId = strNewLogCode;

                        DataModels.LProcesslog data = log.ToData();
                        dataContext.Insert<DataModels.LProcesslog>(data);

                        isAdd = true;
                    }
                }
                return isAdd;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 更新工艺主流程日志
        /// </summary>
        /// <param name="log">日志对象</param>
        /// <returns></returns>
        public bool UpdateMainProcessLog(ProcessData.ProcessLog log)
        {
            try
            {
                bool isUpdate = false;

                if (log != null)
                {
                    using (DataModels.HnfactoryautodbDB dataContext = new DataModels.HnfactoryautodbDB())
                    {
                        DataModels.LProcesslog data = dataContext.LProcesslogs.SingleOrDefault(c => c.ProcessLogId == log.ProcessLogId);
                        data.FinishTime = log.FinishTime;
                        data.ProcessStatus = log.ProcessStatus.ToString();
                        data.ProduceMaterialType = log.ProduceMaterialType.ToString();
                        data.Production = log.Production;
                        data.TakeTime = log.TakeTime;

                        dataContext.Update<DataModels.LProcesslog>(data);

                        isUpdate = true;
                    }
                }

                return isUpdate;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region 工艺子流程日志
        /// <summary>
        /// 添加工艺子流程日志数据
        /// </summary>
        /// <param name="log">日志对象</param>
        /// <returns></returns>
        public bool AddExProcessLog(ProcessData.ExProcessLog log)
        {
            try
            {
                bool isAdd = false;
                if (log != null)
                {
                    using (DataModels.HnfactoryautodbDB dataContext = new DataModels.HnfactoryautodbDB())
                    {
                        //{工艺主流程日志编号}-{子流程编码}-{2位流水号}
                        //获取工艺子流程数据
                        string strSQL = "select SortCode from p_exprocess where ExProcessId=?ExProcessId ";
                        List<MySqlParameter> sqlParameters = new List<MySqlParameter>
                        {
                            new MySqlParameter("?ExProcessId", log.ExProcessId)
                        };
                        string strSortCode = MySqlHelper.ExecuteScalar(CommandType.Text, strSQL, sqlParameters.ToArray()).ToString();

                        //获取已经执行的流程数量
                        strSQL = "select count(*) from L_ExProcessLog where ProcessLogId=?ProcessLogId and ExProcessId=?ExProcessId";
                        sqlParameters.Clear();
                        sqlParameters.Add(new MySqlParameter("?ProcessLogId", log.ProcessLogId));
                        sqlParameters.Add(new MySqlParameter("?ExProcessId", log.ExProcessId));
                        int iNowSum = Convert.ToInt32(MySqlHelper.ExecuteScalar(CommandType.Text, strSQL, sqlParameters.ToArray()));

                        string strNewLogCode = string.Format("{0}-{1}-{2:00}", log.ProcessLogId, strSortCode, iNowSum);

                        log.ExProcessLogId = strNewLogCode;

                        DataModels.LExprocesslog data = log.ToData();

                        dataContext.Insert<DataModels.LExprocesslog>(data);
                        isAdd = true;
                    }

                }
                return isAdd;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 更新工艺子流程日志
        /// </summary>
        /// <param name="log">日志对象</param>
        /// <returns></returns>
        public bool UpdateExProcessLog(ProcessData.ExProcessLog log)
        {
            try
            {
                bool isUpdate = false;

                if (log != null)
                {
                    using (DataModels.HnfactoryautodbDB dataContext = new DataModels.HnfactoryautodbDB())
                    {
                        DataModels.LExprocesslog data = dataContext.LExprocesslogs.SingleOrDefault(c => c.ExProcessLogId == log.ExProcessLogId);
                        data.FinishTime = log.FinishTime;
                        data.ProcessStatus = log.ProcessStatus.ToString();
                        data.ProduceMaterialType = log.ProduceMaterialType.ToString();
                        data.Production = log.Production;
                        data.TakeTime = log.TakeTime;

                        dataContext.Update<DataModels.LExprocesslog>(data);

                        isUpdate = true;
                    }
                }

                return isUpdate;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 工艺子流程步骤日志表
        /// <summary>
        /// 添加工艺子流程步骤日志表
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public bool AddExProcessStepLog(ProcessData.ExProcessStepLog log)
        {
            try
            {
                bool isAdd = false;

                if (log != null)
                {
                    using (DataModels.HnfactoryautodbDB dataContext = new DataModels.HnfactoryautodbDB())
                    {
                        //获取已经执行的步骤数量
                        string strSQL = "select count(*) from l_exprocesssteplog where ExProcessLogId=?ExProcessLogId";
                        List<MySqlParameter> sqlParameters = new List<MySqlParameter>();
                        sqlParameters.Add(new MySqlParameter("?ExProcessLogId", log.ExProcessLogId));
                        int iNowSum = Convert.ToInt32(MySqlHelper.ExecuteScalar(CommandType.Text, strSQL, sqlParameters.ToArray()));

                        //{工艺子流程日志编号}-{2位流水号}
                        string strNewLogCode = string.Format("{0}-{1:00}", log.ExProcessLogId, iNowSum);

                        log.ExProcessStepLogId = strNewLogCode;

                        DataModels.LExprocesssteplog data = log.ToData();
                        dataContext.Insert<DataModels.LExprocesssteplog>(data);
                        isAdd = true;
                    }
                }


                return isAdd;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 工艺子流程步骤参数日志表
        /// <summary>
        /// 添加工艺子流程步骤参数日志数据
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public bool AddExProcessStepParLog(ProcessData.ExProcessStepParsLog log)
        {
            try
            {
                bool isAdd = false;

                if (log != null)
                {
                    using (DataModels.HnfactoryautodbDB dataContext = new DataModels.HnfactoryautodbDB())
                    {

                        DataModels.LExprocessstepparslog data = log.ToData();
                        dataContext.Insert<DataModels.LExprocessstepparslog>(data);
                        isAdd = true;
                    }
                }

                return isAdd;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #endregion

        #region 设备参数日志记录

        #region 日志登记操作

        /// <summary>
        /// 添加设备状态日志记录
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public bool AddDeviceProduceLog(DeviceProduceLog log)
        {
            try
            {
                bool isAdd = false;

                if (log != null)
                {
                    using (DataModels.HnfactoryautodbDB dataContext = new DataModels.HnfactoryautodbDB())
                    {
                        //日志流水编号{设备编号}-{年月日时分秒}
                        string strNewLogCode = string.Format("{0}-{1:yyMMddHHmmss}", log.DeviceId, DateTime.Now);
                        log.DeviceProduceLogId = strNewLogCode;

                        //DataModels.LDeviceproducelog data = log.ToData();
                        //dataContext.Insert<DataModels.LDeviceproducelog>(data);

                        #region 调用存储过程方式来添加日志
                        string strProName = "proc_AddDeviceProduceLog";
                        List<MySqlParameter> sqlParameters = new List<MySqlParameter>
                        {
                            new MySqlParameter("?mDeviceProduceLogId", log.DeviceProduceLogId)
                            ,new MySqlParameter("?mDeviceId", log.DeviceId)
                            ,new MySqlParameter("?mCreated", log.Created)
                            ,new MySqlParameter("?mDeviceStatus", Enum.GetName(log.DeviceStatus.GetType(), log.DeviceStatus) )
                            ,new MySqlParameter("?mSensorId", log.SensorId)
                            ,new MySqlParameter("?mValueType", log.ValueType)
                            ,new MySqlParameter("?mSensorStatusValue", log.SensorStatusValue)
                            ,new MySqlParameter("?mSensorStatus", Enum.GetName(log.SensorStatus.GetType(), log.SensorStatus))
                            ,new MySqlParameter("?mParType", log.ParType)
                            ,new MySqlParameter("?mParUnit", log.ParUnit)
                            ,new MySqlParameter("?mParValue", log.ParValue)

                        };

                        MySqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, strProName, sqlParameters.ToArray());
                        #endregion


                        isAdd = true;
                    }
                }

                return isAdd;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 添加新传感器状态日志记录
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public bool AddNewSensorLog(NewSensorLog log)
        {
            try
            {
                bool isAdd = false;

                if (log != null)
                {
                    using (DataModels.HnfactoryautodbDB dataContext = new DataModels.HnfactoryautodbDB())
                    {

                        #region 调用存储过程方式来添加日志
                        string strProName = "proc_AddSensorLog";
                        List<MySqlParameter> sqlParameters = new List<MySqlParameter>
                        {
                            new MySqlParameter("?mCreated", log.Created)
                            ,new MySqlParameter("?mSensorId", log.SensorId)
                            ,new MySqlParameter("?mValueType", log.ValueType)
                            ,new MySqlParameter("?mSensorStatusValue", log.SensorStatusValue)
                            ,new MySqlParameter("?mSensorStatus", Enum.GetName(log.SensorStatus.GetType(), log.SensorStatus))
                            ,new MySqlParameter("?mParValue", log.ParValue)

                        };

                        MySqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, strProName, sqlParameters.ToArray());
                        #endregion


                        isAdd = true;
                    }
                }

                return isAdd;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="strDeviceId"></param>
        /// <returns></returns>
        public bool AddDeviceProduceLogPar(DeviceProduceLogPar log, string strDeviceId)
        {
            try
            {
                bool isAdd = false;

                if (log != null)
                {
                    using (DataModels.HnfactoryautodbDB dataContext = new DataModels.HnfactoryautodbDB())
                    {

                        //日志流水编号{设备编号}-{年月日时分秒}
                        string strNewLogCode = string.Format("{0}-{1}-{2:yyMMddHHmmss}", strDeviceId, log.ParType.ToString(), DateTime.Now);

                        log.LogParId = strNewLogCode;

                        DataModels.LDeviceproducelogpar data = log.ToData();
                        dataContext.Insert<DataModels.LDeviceproducelogpar>(data);
                        isAdd = true;
                    }
                }

                return isAdd;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 添加设备操作日志记录
        /// </summary>
        /// <param name="log">日志对象</param>
        /// <returns></returns>
        public bool AddDeviceActionLog(DeviceActionLog log)
        {
            try
            {
                bool isAdd = false;

                if (log != null)
                {
                    using (DataModels.HnfactoryautodbDB dataContext = new DataModels.HnfactoryautodbDB())
                    {

                        //日志流水编号{设备编号}-{年月日时分秒}
                        string strNewLogCode = string.Format("Log-{0}-{1:yyMMddHHmmss}", log.SensorId, DateTime.Now);
                        log.DeviceActionLogId = strNewLogCode;

                        DataModels.LDeviceactionlog data = log.ToData();
                        dataContext.Insert<DataModels.LDeviceactionlog>(data);
                        isAdd = true;
                    }
                }

                return isAdd;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region 获取数据

        /// <summary>
        /// 获取设备指定参数最近的日志数据
        /// </summary>
        /// <param name="strDeviceId">设备编号</param>
        /// <param name="parType">参数类型</param>
        /// <returns></returns>
        public DeviceProduceLog GetDeviceProduceLog(string strDeviceId, SysHelper.Enums.DeviceParameterType parType)
        {
            try
            {
                using (DataModels.HnfactoryautodbDB dataContext = new DataModels.HnfactoryautodbDB())
                {
                    DataModels.LDeviceproducelog data = (from d in dataContext.LDeviceproducelogs
                                                         where d.DeviceId == strDeviceId & d.ParType == parType.ToString()
                                                         orderby d.Created descending
                                                         select d).FirstOrDefault();
                    if (data != null)
                    {
                        return new DeviceProduceLog(data);
                    }
                    else
                    {
                        return null;
                    }


                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取生产线所有设备传感器当前状态
        /// </summary>
        /// <param name="strAssemblyLineId">生产线编号</param>
        /// <returns></returns>
        public DeviceProduceLogCollection GetDeviceProduceLogCollection(string strAssemblyLineId)
        {
            try
            {
                DeviceProduceLogCollection items = new DeviceProduceLogCollection();
                using (DataModels.HnfactoryautodbDB dataContext = new DataModels.HnfactoryautodbDB())
                {
                    var datas = (from c in dataContext.VDeviceproducelogs
                                 where c.AssemblyLineId == strAssemblyLineId
                                 select c
                                 ).ToList();
                    foreach(DataModels.VDeviceproducelog data in datas)
                    {
                        DeviceProduceLog item = new DeviceProduceLog(data);
                        items.Add(item);
                    }
                }

                return items;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #endregion

        #region 测试方法

        public void SocketLog(string strLogText,int iTextLength)
        {
            string strSQL = @"INSERT INTO tmp_stocktest(LogTime,LogText,LogLength)VALUES(?LogTime,?LogText,?LogLength)";
            List<MySqlParameter> sqlParameters = new List<MySqlParameter>
                {
                    new MySqlParameter("?LogTime", DateTime.Now)
                    ,new MySqlParameter("?LogText",strLogText)
                    ,new MySqlParameter("?LogLength",iTextLength)
                };

            MySqlHelper.ExecuteNonQuery(CommandType.Text, strSQL, sqlParameters.ToArray());
        }

        #endregion
    }
}
