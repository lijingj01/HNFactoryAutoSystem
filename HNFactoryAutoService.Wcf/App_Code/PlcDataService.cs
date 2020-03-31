using HNFactoryAutoSystem.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“PlcDataService”。
public class PlcDataService : IPlcDataService
{
    public void DoWork()
    {
    }

    public void RequestPlcDataToDB(string strJsonText)
    {
        try
        {
            if (!string.IsNullOrEmpty(strJsonText))
            {
                int receiveNumber = strJsonText.Length;
                DataLogHelper logHelper = new DataLogHelper();

                logHelper.SocketLog(strJsonText, receiveNumber);

                #region 对接收到的传感器日志数据进行转换操作
                try
                {
                    HNFactoryAutoSystem.Business.DeviceBusiness deviceBusiness = new HNFactoryAutoSystem.Business.DeviceBusiness();
                    SensorDataCollection datas = JsonConvert.DeserializeObject<SensorDataCollection>(strJsonText);
                    //将传感器读取的数据写入生产日志表
                    foreach (SensorData data in datas)
                    {
                        //判断值类型来提取传感器的状态信息
                        string strDeviceStatus = "P";
                        string strSensorId = data.SensorId;
                        string strSensorValueType = data.ValueType;
                        decimal deParValue = data.SensorValue;

                        deviceBusiness.AddDeviceProduceLog(strDeviceStatus, strSensorId, strSensorValueType, deParValue);
                    }
                }
                catch { }
                #endregion
            }
        }
        catch { }
    }
}
