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
        /// <param name="strSensorValueType">传感器参数类型</param>
        /// <param name="deParValue">参数数值</param>
        /// <returns></returns>
        public bool AddDeviceProduceLog(string strDeviceStatus, string strSensorId,string strSensorValueType
                                    ,decimal deParValue)
        {
            try
            {
                bool isAdd = false;
                Data.DataHelper dataHelper = new Data.DataHelper();
                //string strDeviceId = dataHelper.GetDeviceIdBySensor(strSensorId);
                Data.SensorInfo sensor = dataHelper.GetSensorInfo(strSensorId);
                Data.DataLogHelper logHelper = new Data.DataLogHelper();

                #region 状态值分析

                int iSensorStatusValue = Convert.ToInt32(deParValue);
                string strSensorStatus = string.Empty;
                switch (strSensorValueType)
                {
                    case "NumberValue":
                        //过程值数据
                        iSensorStatusValue = 1;
                        SysHelper.Enums.SenserStatusType nSt = SysHelper.Enums.SenserStatusType.None;
                        strSensorStatus = Enum.GetName(nSt.GetType(), nSt);
                        break;
                    case "PumpStatus":
                        //泵状态值
                        SysHelper.Enums.PumpStatusEnum pumpStatus = SysHelper.Enums.EnumHelper.Parse<SysHelper.Enums.PumpStatusEnum>(iSensorStatusValue);
                        strSensorStatus = Enum.GetName(pumpStatus.GetType(), pumpStatus);
                        break;
                    case "ValveStatus":
                        //阀门状态值
                        SysHelper.Enums.ValveStatusEnum valveStatus = SysHelper.Enums.EnumHelper.Parse<SysHelper.Enums.ValveStatusEnum>(iSensorStatusValue);
                        strSensorStatus = Enum.GetName(valveStatus.GetType(), valveStatus);
                        break;
                    case "MotorStatus":
                        //搅拌电机状态
                        SysHelper.Enums.MotorStatusEnum motorStatus = SysHelper.Enums.EnumHelper.Parse<SysHelper.Enums.MotorStatusEnum>(iSensorStatusValue);
                        strSensorStatus = Enum.GetName(motorStatus.GetType(), motorStatus);
                        break;
                    case "ScrewStatus":
                        //螺旋输送机状态
                        SysHelper.Enums.ScrewStatusEnum screwStatus = SysHelper.Enums.EnumHelper.Parse<SysHelper.Enums.ScrewStatusEnum>(iSensorStatusValue);
                        strSensorStatus = Enum.GetName(screwStatus.GetType(), screwStatus);
                        break;
                    default: break;
                }
                SysHelper.Enums.SenserStatusType statusType = SysHelper.Enums.EnumHelper.Parse<SysHelper.Enums.SenserStatusType>(strSensorStatus);

                #endregion

                if (sensor != null)
                {
                    #region 将日志写入系统

                    Data.DeviceProduceLog log = new Data.DeviceProduceLog()
                    {
                        DeviceId = sensor.DeviceId,
                        Created = DateTime.Now,
                        DeviceStatus = SysHelper.Enums.EnumHelper.Parse<SysHelper.Enums.DeviceActionType>(strDeviceStatus),
                        SensorId = strSensorId,
                        ValueType = strSensorValueType,
                        SensorStatusValue = iSensorStatusValue,
                        SensorStatus = statusType,
                        ParType = sensor.ParType,
                        ParUnit = sensor.Units,
                        ParValue = deParValue
                    };

                    if (logHelper.AddDeviceProduceLog(log))
                    {
                        isAdd = true;
                    }

                    #endregion
                }
                else
                {
                    #region 未能找到数据的传感器信息也保存起来
                    Data.NewSensorLog log = new Data.NewSensorLog()
                    {
                        Created = DateTime.Now,
                        SensorId = strSensorId,
                        ValueType = strSensorValueType,
                        SensorStatusValue = iSensorStatusValue,
                        SensorStatus = statusType,
                        ParValue = deParValue
                    };

                    if (logHelper.AddNewSensorLog(log))
                    {
                        isAdd = true;
                    }
                    #endregion
                }

                return isAdd;
            }
            catch
            {
                return false;
            }
        }
    }
}
