using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;

namespace HNFactoryAutoSystem.Data
{
    #region 类基础对象

    public class EntityBase
    {
        public int Id { get; set; }
    }

    public class ListBase<T> : List<T> 
    {
        /// <summary>
        /// List集合转换成对象
        /// </summary>
        /// <param name="list"></param>
        public void ListToConvert(List<T> list)
        {
            if (list != null)
            {
                //先移除对象里面的集合数据
                this.Clear();
                foreach (T t in list)
                {
                    this.Add(t);
                }
            }
        }

        #region 处理方法
        /// <summary>
        /// 将对象转换成Json字符串
        /// </summary>
        /// <returns></returns>
        public string ToJsonString()
        {
            string strJson = string.Empty;
            try
            {
                IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
                //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式  'HH':'mm':'ss"     
                timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd HH':'mm':'ss";
                strJson = JsonConvert.SerializeObject(this, Formatting.Indented, timeConverter);
            }
            catch (Exception ex) { }
            return strJson;
        }
        #endregion
    }
    #endregion

    #region 基础设置对象

    #region 工厂信息表
    public class FactoryInfo : EntityBase
    {
        #region 属性
        /// <summary>
        /// 工厂唯一编号
        /// </summary>
        public string FactoryId { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryTitle { get; set; }
        /// <summary>
        /// 所在城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 参数1
        /// </summary>
        public string Parameter1 { get; set; }
        /// <summary>
        /// 参数2
        /// </summary>
        public string Parameter2 { get; set; }
        #endregion

        #region 构造函数

        #endregion
    }
    public class FactoryInfoCollection : ListBase<FactoryInfo> { }
    #endregion

    #region 工厂生产线表
    public class AssemblyLineInfo : EntityBase
    {
        #region 属性
        /// <summary>
        /// 生产线唯一编号
        /// </summary>
        public string AssemblyLineId { get; set; }
        /// <summary>
        /// 对应的工艺流程编号
        /// </summary>
        public string ProcessId { get; set; }
        /// <summary>
        /// 对应的工厂编号
        /// </summary>
        public string FactoryId { get; set; }
        /// <summary>
        /// 生产线名称
        /// </summary>
        public string AssemblyLineTitle { get; set; }
        /// <summary>
        /// 参数1
        /// </summary>
        public string Parameter1 { get; set; }
        /// <summary>
        /// 参数2
        /// </summary>
        public string Parameter2 { get; set; }
        #endregion

        #region 扩展属性
        /// <summary>
        /// 工艺主流程说明
        /// </summary>
        public string ProcessTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProductsType { get; set; }

        /// <summary>
        /// 生产线生产状态（未生产，生产中）
        /// </summary>
        public string AssemblyLineStatus { get; set; }
        #endregion

        #region 构造函数
        public AssemblyLineInfo() {
            AssemblyLineStatus = "未生产";
        }

        internal AssemblyLineInfo(DataModels.FAssemblylineinfo data) : this()
        {
            this.Id = data.Id;
            this.AssemblyLineId = data.AssemblyLineId;
            this.AssemblyLineTitle = data.AssemblyLineTitle;
            this.ProcessId = data.ProcessId;
            this.FactoryId = data.FactoryId;
            this.Parameter1 = data.Parameter1;
            this.Parameter2 = data.Parameter2;
        }

        internal AssemblyLineInfo(DataModels.VAssemblylineinfo data) : this()
        {
            this.Id = data.Id;
            this.AssemblyLineId = data.AssemblyLineId;
            this.AssemblyLineTitle = data.AssemblyLineTitle;
            this.ProcessId = data.ProcessId;
            this.FactoryId = data.FactoryId;
            this.Parameter1 = data.Parameter1;
            this.Parameter2 = data.Parameter2;
            this.ProcessTitle = data.ProcessTitle;
            this.ProductsType = data.ProductsType;
        }
        #endregion
    }
    public class AssemblyLineInfoCollection : ListBase<AssemblyLineInfo>
    {

    }
    #endregion

    #region 设备基础信息表
    public class DeviceInfo : EntityBase
    {
        #region 属性
        /// <summary>
        /// 设备唯一编号
        /// </summary>
        public string DeviceId { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceType { get; set; }
        /// <summary>
        /// 对应的工艺设备编号
        /// </summary>
        public string ProcessDeviceId { get; set; }
        /// <summary>
        /// 设备当前状态（投放/停止/搅拌等）
        /// </summary>
        public string DeviceStatus { get; set; }
        /// <summary>
        /// 参数1
        /// </summary>
        public string Parameter1 { get; set; }
        /// <summary>
        /// 参数2
        /// </summary>
        public string Parameter2 { get; set; }
        #endregion

        #region 扩展属性
        /// <summary>
        /// 生产线编码
        /// </summary>
        public string AssemblyLineId { get; set; }
        #endregion

        #region 集合属性
        public SensorInfoCollection Sensors { get; set; }
        #endregion

        #region 构造函数
        public DeviceInfo() {
            Sensors = new SensorInfoCollection();
        }

        #endregion
    }
    public class DeviceInfoCollection : ListBase<DeviceInfo> { }
    #endregion

    #region 设备生产线关联表

    #endregion

    #region 传感器基础信息表
    public class SensorInfo : EntityBase
    {
        #region 属性
        /// <summary>
        /// 传感器唯一编号
        /// </summary>
        public string SensorId { get; set; }
        /// <summary>
        /// 传感器名称
        /// </summary>
        public string SensorName { get; set; }
        /// <summary>
        /// 功率
        /// </summary>
        public string Power { get; set; }
        /// <summary>
        /// 电压
        /// </summary>
        public string Voltage { get; set; }
        /// <summary>
        /// 电机类型
        /// </summary>
        public string StarterType { get; set; }
        /// <summary>
        /// 数字输入信号
        /// </summary>
        public int? IO_DI { get; set; }
        /// <summary>
        /// 数字输出信号
        /// </summary>
        public int? IO_DO { get; set; }
        /// <summary>
        /// 模拟输入
        /// </summary>
        public int? IO_AI { get; set; }
        /// <summary>
        /// 模拟输出
        /// </summary>
        public int? IO_AO { get; set; }
        /// <summary>
        /// Modbus输出
        /// </summary>
        public int? IO_Modbus { get; set; }
        /// <summary>
        /// 最小参数
        /// </summary>
        public decimal? MinEU { get; set; }
        /// <summary>
        /// 最大参数
        /// </summary>
        public decimal? MaxEU { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Units { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        public SysHelper.Enums.DeviceParameterType ParType { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string SComment { get; set; }
        /// <summary>
        /// 传感器状态（1:启用/0:关闭/-1:报废）
        /// </summary>
        public int SensorStatus { get; set; }
        /// <summary>
        /// 所在PLC地址编号
        /// </summary>
        public string PLC_Id { get; set; }
        /// <summary>
        /// 所在设备编号
        /// </summary>
        public string DeviceId { get; set; }

        public string ToDeviceId { get; set; }

        /// <summary>
        /// 传感器在PLC里面的地址编码
        /// </summary>
        public string PLC_Address { get; set; }
        /// <summary>
        /// 传感器在PLC里面的数值所占位置长度
        /// </summary>
        public int ValueLength { get; set; }
        #endregion

        #region 构造函数
        public SensorInfo()
        {

        }

        internal SensorInfo(DataModels.FSensorinfo data) : this()
        {
            this.Id = data.Sid;
            this.SensorId = data.SensorId;
            this.SensorName = data.SensorName;
            this.Power = data.Power;
            this.Voltage = data.Voltage;
            this.StarterType = data.StarterType;
            this.IO_DI = data.IoDi;
            this.IO_DO = data.IoDo;
            this.IO_AI = data.IoAi;
            this.IO_AO = data.IoAo;
            this.IO_Modbus = data.IoModbus;
            this.MinEU = data.MinEU;
            this.MaxEU = data.MaxEu;
            this.Units = data.Units;
            this.ParType = SysHelper.Enums.EnumHelper.Parse<SysHelper.Enums.DeviceParameterType>(data.ParType);
            this.SComment = data.SComment;
            this.SensorStatus = data.SensorStatus;
            this.PLC_Id = data.PlcId;
            this.DeviceId = data.DeviceId;
            this.ToDeviceId = data.ToDeviceId;
            this.PLC_Address = data.PlcAddress;
            this.ValueLength = data.ValueLength;
        }
        #endregion
    }

    public class SensorInfoCollection : ListBase<SensorInfo>
    {

    }
    #endregion

    #endregion

    #region 生产日志数据

    #region 设备生产日志

    /// <summary>
    /// 设备生产日志
    /// </summary>
    public class DeviceProduceLog : EntityBase
    {
        #region 属性
        /// <summary>
        /// 日志流水编号
        /// </summary>
        public string DeviceProduceLogId { get; set; }
        /// <summary>
        /// 设备唯一编号
        /// </summary>
        public string DeviceId { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime? Created { get; set; }
        /// <summary>
        /// 是否是当前最新状态
        /// </summary>
        public bool IsCurrent { get; set; }
        /// <summary>
        /// 设备当前状态
        /// </summary>
        public SysHelper.Enums.DeviceActionType DeviceStatus { get; set; }

        /// <summary>
        /// 采集来源传感器编号
        /// </summary>
        public string SensorId { get; set; }

        /// <summary>
        /// 数值对应的状态类型
        /// </summary>
        public string ValueType { get; set; }
        /// <summary>
        /// 传感器当前状态值
        /// </summary>
        public int SensorStatusValue { get; set; }
        /// <summary>
        /// 传感器当前状态
        /// </summary>
        public SysHelper.Enums.SenserStatusType SensorStatus { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        public SysHelper.Enums.DeviceParameterType ParType { get; set; }
        /// <summary>
        /// 参数单位
        /// </summary>
        public string ParUnit { get; set; }
        /// <summary>
        /// 浮点数值类参数值(重量，温度，搅拌速度,PH值)
        /// </summary>
        public decimal? ParValue { get; set; }
        #endregion

        #region 扩展的视图属性
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 传感器名称
        /// </summary>
        public string SensorName { get; set; }
        /// <summary>
        /// 生产线唯一编号
        /// </summary>
        public string AssemblyLineId { get; set; }
        /// <summary>
        /// 生产线名称
        /// </summary>
        public string AssemblyLineTitle { get; set; }
        /// <summary>
        /// 工艺主流程说明
        /// </summary>
        public string ProcessTitle { get; set; }
        #endregion

        #region 构造函数
        public DeviceProduceLog()
        {
            ParValue = decimal.Zero;
        }

        internal DeviceProduceLog(DataModels.LDeviceproducelog data)
        {
            this.Id = data.Id;
            this.DeviceProduceLogId = data.DeviceProduceLogId;
            this.DeviceId = data.DeviceId;
            this.Created = data.Created;
            this.IsCurrent = Convert.ToBoolean(data.IsCurrent);
            this.DeviceStatus = SysHelper.Enums.EnumHelper.Parse<SysHelper.Enums.DeviceActionType>(data.DeviceStatus);
            this.SensorId = data.SensorId;
            this.ValueType = data.ValueType;
            this.SensorStatusValue = data.SensorStatusValue;
            this.SensorStatus = SysHelper.Enums.EnumHelper.Parse<SysHelper.Enums.SenserStatusType>(data.SensorStatus);
            this.ParType = SysHelper.Enums.EnumHelper.Parse<SysHelper.Enums.DeviceParameterType>(data.ParType);
            this.ParUnit = data.ParUnit;
            this.ParValue = data.ParValue;
        }

        internal DeviceProduceLog(DataModels.VDeviceproducelog data)
        {
            this.Id = data.Id;
            this.DeviceProduceLogId = data.DeviceProduceLogId;
            this.DeviceId = data.DeviceId;
            this.Created = data.Created;
            this.IsCurrent = Convert.ToBoolean(data.IsCurrent);
            this.DeviceStatus = SysHelper.Enums.EnumHelper.Parse<SysHelper.Enums.DeviceActionType>(data.DeviceStatus);
            this.SensorId = data.SensorId;
            this.ValueType = data.ValueType;
            this.SensorStatusValue = data.SensorStatusValue;
            this.SensorStatus = SysHelper.Enums.EnumHelper.Parse<SysHelper.Enums.SenserStatusType>(data.SensorStatus);
            this.ParType = SysHelper.Enums.EnumHelper.Parse<SysHelper.Enums.DeviceParameterType>(data.ParType);
            this.ParUnit = data.ParUnit;
            this.ParValue = data.ParValue;
            this.DeviceName = data.DeviceName;
            this.SensorName = data.SensorName;
            this.AssemblyLineId = data.AssemblyLineId;
            this.AssemblyLineTitle = data.AssemblyLineTitle;
            this.ProcessTitle = data.ProcessTitle;
        }
        #endregion

        #region 转换方法
        internal DataModels.LDeviceproducelog ToData()
        {
            DataModels.LDeviceproducelog data = new DataModels.LDeviceproducelog();

            data.Id = this.Id;
            data.DeviceProduceLogId = this.DeviceProduceLogId;
            data.DeviceId = this.DeviceId;
            data.Created = this.Created;
            data.IsCurrent = Convert.ToSByte(this.IsCurrent);
            data.DeviceStatus = Enum.GetName(this.DeviceStatus.GetType(), this.DeviceStatus);
            data.SensorId = this.SensorId;
            data.ValueType = this.ValueType;
            data.SensorStatusValue = this.SensorStatusValue;
            data.SensorStatus = Enum.GetName(this.SensorStatus.GetType(), this.SensorStatus); 
            data.ParType = Enum.GetName(this.ParType.GetType(), this.ParType); 
            data.ParUnit = this.ParUnit;
            data.ParValue = this.ParValue;

            return data;
        }
        #endregion
    }

    /// <summary>
    /// 设备生产日志集合
    /// </summary>
    public class DeviceProduceLogCollection : ListBase<DeviceProduceLog> { }

    #endregion

    #region 设备操作日志

    /// <summary>
    /// 设备操作日志对象
    /// </summary>
    public class DeviceActionLog : EntityBase
    {
        #region 属性
        /// <summary>
        /// 日志编号
        /// </summary>
        public string DeviceActionLogId { get; set; }
        /// <summary>
        /// 设备唯一编号
        /// </summary>
        public string DeviceId { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// 操作人或设备编号
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 控制的传感器编号
        /// </summary>
        public string SensorId { get; set; }
        /// <summary>
        /// 操作类型（排出/搅拌等）
        /// </summary>
        public SysHelper.Enums.SenserStatusType ActionType { get; set; }
        /// <summary>
        /// 参数类型
        /// </summary>
        public SysHelper.Enums.DeviceParameterType ParType { get; set; }
        /// <summary>
        /// 参数单位
        /// </summary>
        public string ParUnit { get; set; }
        /// <summary>
        /// 浮点数值类参数值(重量，温度，搅拌速度,PH值)
        /// </summary>
        public decimal ParValue { get; set; }
        /// <summary>
        /// 目标设备编号
        /// </summary>
        public string ToDeviceId { get; set; }
        /// <summary>
        /// 目标传感器编号
        /// </summary>
        public string ToSensorId { get; set; }
        #endregion

        #region 构造函数

        #endregion

        #region 转换方法
        internal DataModels.LDeviceactionlog ToData()
        {
            DataModels.LDeviceactionlog data = new DataModels.LDeviceactionlog();
            data.Id = this.Id;
            data.DeviceActionLogId = this.DeviceActionLogId;
            data.DeviceId = this.DeviceId;
            data.Created = this.Created;
            data.CreateUser = this.CreateUser;
            data.SensorId = this.SensorId;
            data.ActionType = this.ActionType.ToString();
            data.ParType = this.ParType.ToString();
            data.ParUnit = this.ParUnit;
            data.ParValue = this.ParValue;
            data.ToDeviceId = this.ToDeviceId;
            data.ToSensorId = this.ToSensorId;

            return data;
        }
        #endregion
    }

    #endregion

    #region 用来接收传感器日志数据的对象

    /// <summary>
    /// 传感器日志对象
    /// </summary>
    public class SensorData : EntityBase
    {
        #region 属性
        /// <summary>
        /// 工厂编号
        /// </summary>
        public string FactoryId { get; set; }
        /// <summary>
        /// 所在PLC机柜编号
        /// </summary>
        public string PLCId { get; set; }
        /// <summary>
        /// 传感器在PLC机柜里面的地址编码
        /// </summary>
        public string PLCAddress { get; set; }
        /// <summary>
        /// 传感器控制器编号
        /// </summary>
        public string SensorId { get; set; }
        /// <summary>
        /// 数值对应的状态类型
        /// </summary>
        public string ValueType { get; set; }
        /// <summary>
        /// 传感器的数值
        /// </summary>
        public decimal SensorValue { get; set; }
        /// <summary>
        /// 读取传感器的时间
        /// </summary>
        public DateTime ReceiveTime { get; set; }
        #endregion
    }
    /// <summary>
    /// 传感器日志对象集合
    /// </summary>
    public class SensorDataCollection : ListBase<SensorData>
    {

    }

    #endregion

    #region 特殊未录入的传感器日志

    public class NewSensorLog : EntityBase
    {
        #region 属性
        /// <summary>
        /// 采集来源传感器编号
        /// </summary>
        public string SensorId { get; set; }
        /// <summary>
        /// 是否是当前最新状态
        /// </summary>
        public bool IsCurrent { get; set; }

        /// <summary>
        /// 数值对应的状态类型
        /// </summary>
        public string ValueType { get; set; }
        /// <summary>
        /// 传感器当前状态值
        /// </summary>
        public int SensorStatusValue { get; set; }
        /// <summary>
        /// 传感器当前状态
        /// </summary>
        public SysHelper.Enums.SenserStatusType SensorStatus { get; set; }
        /// <summary>
        /// 浮点数值类参数值(重量，温度，搅拌速度,PH值)
        /// </summary>
        public decimal? ParValue { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime? Created { get; set; }
        #endregion
    }

    #endregion

    #region 未启用

    public class DeviceProduceLogPar : EntityBase
    {
        #region 属性
        /// <summary>
        /// 流水编号
        /// </summary>
        public string LogParId { get; set; }
        /// <summary>
        /// 设备日志编号
        /// </summary>
        public string DeviceProduceLogId { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// 采集来源传感器编号
        /// </summary>
        public string SensorId { get; set; }
        /// <summary>
        /// 传感器当前状态
        /// </summary>
        public SysHelper.Enums.SenserStatusType SensorStatus { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        public SysHelper.Enums.DeviceParameterType ParType { get; set; }
        /// <summary>
        /// 参数单位
        /// </summary>
        public string ParUnit { get; set; }
        /// <summary>
        /// 浮点数值类参数值(重量，温度，搅拌速度,PH值)
        /// </summary>
        public decimal? ParValue { get; set; }
        #endregion

        #region 构造函数

        #endregion

        #region 转换方法
        internal DataModels.LDeviceproducelogpar ToData()
        {
            DataModels.LDeviceproducelogpar data = new DataModels.LDeviceproducelogpar();

            data.Id = this.Id;
            data.LogParId = this.LogParId;
            data.DeviceProduceLogId = this.DeviceProduceLogId;
            data.Created = this.Created;
            data.SensorId = this.SensorId;
            data.SensorStatus = this.SensorStatus.ToString();
            data.ParType = this.ParType.ToString();
            data.ParUnit = this.ParUnit;
            data.ParValue = this.ParValue;

            return data;
        }
        #endregion
    }

    public class DeviceProduceLogParCollection : ListBase<DeviceProduceLogPar> { }

    #endregion

    #endregion
}
