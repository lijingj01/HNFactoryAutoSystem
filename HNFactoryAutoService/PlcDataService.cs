using HNFactoryAutoSystem.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HNFactoryAutoService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Service1”。
    public class PlcDataService : IPlcDataService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public Student GetStudentById(string Id)
        {
            return UserList.Instance.Users[int.Parse(Id)];
        }

        public IList<Student> GetStudentList()
        {
            return UserList.Instance.Users;
        }

        /// <summary>
        /// 将读取到的PLC原始数据写入服务库
        /// </summary>
        /// <param name="strJsonText"></param>
        public void RequestPlcDataToDB(string strJsonText)
        {
            try
            {
                if (!string.IsNullOrEmpty(strJsonText))
                {
                    int receiveNumber = strJsonText.Length;
                    DataLogHelper logHelper = new DataLogHelper();
                    HNFactoryAutoSystem.Business.DeviceBusiness deviceBusiness = new HNFactoryAutoSystem.Business.DeviceBusiness();

                    logHelper.SocketLog(strJsonText, receiveNumber);

                    #region 对接收到的传感器日志数据进行转换操作
                    try
                    {
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
}
