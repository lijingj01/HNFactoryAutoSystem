using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HNFactoryAutoSystem.Business
{
    /// <summary>
    /// 设备信息操作方法
    /// </summary>
    public class DeviceBusiness
    {
        /// <summary>
        /// 通过传感器状态参数来添加设备状态日志
        /// </summary>
        /// <param name="strDeviceStatus">设备状态</param>
        /// <param name="strSensorId">传感器编号</param>
        /// <param name="strSensorStatus">传感器状态</param>
        /// <param name="strParType">参数类型</param>
        /// <param name="strParUnit">参数单位</param>
        /// <param name="deParValue">参数数值</param>
        /// <returns></returns>
        public bool AddDeviceProduceLog(string strDeviceStatus, string strSensorId,string strSensorStatus,string strParType,string strParUnit,decimal deParValue)
        {
            try
            {
                bool isAdd = false;
                Data.DataHelper dataHelper = new Data.DataHelper();
                string strDeviceId = dataHelper.GetDeviceIdBySensor(strSensorId);
                if (!string.IsNullOrEmpty(strDeviceId))
                {
                    Data.DeviceProduceLog log = new Data.DeviceProduceLog()
                    {
                        DeviceId = strDeviceId,
                        Created = DateTime.Now,
                        DeviceStatus = SysHelper.Enums.EnumHelper.Parse<SysHelper.Enums.DeviceActionType>(strDeviceStatus),
                        SensorId = strSensorId,
                        SensorStatus = SysHelper.Enums.EnumHelper.Parse<SysHelper.Enums.SenserStatusType>(strSensorStatus),
                        ParType = SysHelper.Enums.EnumHelper.Parse<SysHelper.Enums.DeviceParameterType>(strParType),
                        ParUnit = strParUnit,
                        ParValue = deParValue
                    };

                    Data.DataLogHelper logHelper = new Data.DataLogHelper();
                    if (logHelper.AddDeviceProduceLog(log))
                    {
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
    }
}
