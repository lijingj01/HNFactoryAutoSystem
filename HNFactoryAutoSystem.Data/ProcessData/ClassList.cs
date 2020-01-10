using System;
using System.Collections.Generic;

namespace HNFactoryAutoSystem.Data.ProcessData
{
    #region 工艺流程设置对象

    #region 工艺主流程

    /// <summary>
    /// 工艺流程类
    /// </summary>
    public class TechnologicalProcess : EntityBase
    {
        #region 属性
        /// <summary>
        /// 工艺主流程唯一编号
        /// </summary>
        public string ProcessId { get; set; }
        /// <summary>
        /// 工艺主流程说明
        /// </summary>
        public string ProcessTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProductsType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal PurityMin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal PurityMax { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ProcessYear { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Fineness { get; set; }
        #endregion

        #region 扩展属性

        #endregion

        #region 构造函数
        public TechnologicalProcess() { }

        internal TechnologicalProcess(DataModels.PMainprocess data):this()
        {
            this.Id = data.Id;
            this.ProcessId = data.ProcessId;
            this.ProcessTitle = data.ProcessTitle;
            this.ProductsType = data.ProductsType;
            this.PurityMin = data.PurityMin;
            this.PurityMax = data.PuritvMax;
            this.ProcessYear = data.ProcessYear;
            this.Fineness = data.Fineness;
        }
        #endregion

        #region 转换方法

        #endregion
    }

    public class TechnologicalProcessCollection : ListBase<TechnologicalProcess>
    {

    }
    #endregion

    #region 工艺子流程

    /// <summary>
    /// 工艺子流程对象
    /// </summary>
    public class TeExProcess : EntityBase
    {
        #region 属性
        /// <summary>
        /// 工艺子流程唯一编号
        /// </summary>
        public string ExProcessId { get; set; }
        /// <summary>
        /// 工艺子流程标题
        /// </summary>
        public string ExProcessTitle { get; set; }
        /// <summary>
        /// 工艺子流程短编码
        /// </summary>
        public string SortCode { get; set; }
        /// <summary>
        /// 主工艺流程编号
        /// </summary>
        public string ProcessId { get; set; }
        /// <summary>
        /// 启动流程条件的设备编号
        /// </summary>
        public string StartDeviceId { get; set; }

        /// <summary>
        /// 启动条件的设备参数类型
        /// </summary>
        public SysHelper.Enums.DeviceParameterType ParType { get; set; }

        /// <summary>
        /// 浮点数值类参数值(重量，温度，搅拌速度)
        /// </summary>
        public decimal ParValue { get; set; }
        /// <summary>
        /// 流程执行的顺序
        /// </summary>
        public int ProcessOrderId { get; set; }
        #endregion

        #region 集合参数
        /// <summary>
        /// 流程的步骤集合
        /// </summary>
        public ProcessStepCollection Steps { get; set; }
        #endregion

        #region 构造函数
        public TeExProcess()
        {
            Steps = new ProcessStepCollection();
        }

        internal TeExProcess(DataModels.PExprocess data) : this()
        {
            this.Id = data.Id;
            this.ExProcessId = data.ExProcessId;
            this.ExProcessTitle = data.ExProcessTitle;
            this.SortCode = data.SortCode;
            this.ProcessId = data.ProcessId;
            this.StartDeviceId = data.StartDeviceId;
            this.ParType = SysHelper.Enums.EnumHelper.Parse<SysHelper.Enums.DeviceParameterType>(data.ParType);
            this.ParValue = data.ParValue;
            this.ProcessOrderId = data.ProcessOrderId;
        }
        #endregion

        #region 转换方法

        #endregion
    }

    public class TeExProcessCollection : ListBase<TeExProcess>
    {

    }
    #endregion

    #region 工业设备基础表

    public class ProcessDevice : EntityBase
    {
        #region 属性
        /// <summary>
        /// 工艺设备唯一编号
        /// </summary>
        public string ProcessDeviceId { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string ProcessDeviceName { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceType { get; set; }
        /// <summary>
        /// 物料类型
        /// </summary>
        public string MaterialType { get; set; }
        /// <summary>
        /// 参数1
        /// </summary>
        public string Parameter1 { get; set; }
        /// <summary>
        /// 参数2
        /// </summary>
        public string Parameter2 { get; set; }
        /// <summary>
        /// 参数3
        /// </summary>
        public string Parameter3 { get; set; }
        #endregion

        #region 转换方法

        #endregion
    }

    public class ProcessDeviceCollection : ListBase<ProcessDevice>
    {

    }

    #endregion

    #region 工艺子流程步骤表
    public class ProcessStep : EntityBase
    {
        #region 属性
        /// <summary>
        /// 执行子流程步骤唯一编号
        /// </summary>
        public string StepId { get; set; }
        /// <summary>
        /// 步骤标题
        /// </summary>
        public string StepTitle { get; set; }
        /// <summary>
        /// 工艺子流程编号
        /// </summary>
        public string ExProcessId { get; set; }
        /// <summary>
        /// 流程执行顺序
        /// </summary>
        public int OrderIndex { get; set; }
        /// <summary>
        /// 对应的工艺设备编号
        /// </summary>
        public string ProcessDeviceId { get; set; }
        /// <summary>
        /// 操作类型（排出/搅拌等）
        /// </summary>
        public SysHelper.Enums.DeviceActionType ActionType { get; set; }
        /// <summary>
        /// 步骤和下一步骤是否是同步启动
        /// </summary>
        public bool IsSync { get; set; }
        /// <summary>
        /// 同步的步骤的间隔启动时间（分钟）
        /// </summary>
        public int SyncStepInterval { get; set; }
        /// <summary>
        /// 完成步骤对应的参数类型
        /// </summary>
        public SysHelper.Enums.DeviceParameterType FinishParType { get; set; }
        /// <summary>
        /// 完成步骤对应的参数单位
        /// </summary>
        public string FinishParUnit { get; set; }
        /// <summary>
        /// 完成步骤的浮点数值类参数值(重量，温度，搅拌速度,PH值,时间)
        /// </summary>
        public decimal? FinishParValue { get; set; }
        /// <summary>
        /// 上一步骤的编号
        /// </summary>
        public string BeforeStepId { get; set; }
        /// <summary>
        /// 完成步骤后出现的产品编号
        /// </summary>
        public string ResultsId { get; set; }
        #endregion

        #region 扩展集合数据
        /// <summary>
        /// 流程步骤的参数集合
        /// </summary>
        public ExProcessStepParsCollection StepPars { get; set; }
        #endregion

        #region 构造函数
        public ProcessStep()
        {
            StepPars = new ExProcessStepParsCollection();
        }

        internal ProcessStep(DataModels.PExprocessstep data)
        {
            this.Id = data.Id;
            this.StepId = data.StepId;
            this.StepTitle = data.StepTitle;
            this.ExProcessId = data.ExProcessId;
            this.OrderIndex = data.OrderIndex;
            this.ProcessDeviceId = data.ProcessDeviceId;
            this.ActionType = SysHelper.Enums.EnumHelper.Parse<SysHelper.Enums.DeviceActionType>(data.ActionType);
            this.IsSync = Convert.ToBoolean(data.IsSync);
            this.SyncStepInterval = data.SyncStepInterval;
            this.FinishParType = SysHelper.Enums.EnumHelper.Parse<SysHelper.Enums.DeviceParameterType>(data.FinishParType);
            this.FinishParUnit = data.FinishParUnit;
            this.FinishParValue = data.FinishParValue;
            this.BeforeStepId = data.BeforeStepId;
            this.ResultsId = data.ResultsId;
        }
        #endregion

        #region 转换方法

        #endregion
    }

    public class ProcessStepCollection : ListBase<ProcessStep> { }
    #endregion

    #region 工艺子流程步骤参数表

    public class ExProcessStepPars:EntityBase
    {
        #region 属性
        /// <summary>
        /// 参数编号
        /// </summary>
        public string ParId { get; set; }
        /// <summary>
        /// 子流程步骤编号
        /// </summary>
        public string StepId { get; set; }
        /// <summary>
        /// 对应的传感器编号
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
        /// 需要的时间数（记录分钟计算）
        /// </summary>
        public int ParTime { get; set; }
        /// <summary>
        /// 参数是否是步骤完成的条件
        /// </summary>
        public bool IsFinish { get; set; }
        #endregion

        #region 构造函数
        public ExProcessStepPars()
        {

        }
        internal ExProcessStepPars(DataModels.PExprocesssteppar data) : this()
        {
            this.Id = data.Id;
            this.ParId = data.ParId;
            this.StepId = data.StepId;
            this.SensorId = data.SensorId;
            this.ActionType = SysHelper.Enums.EnumHelper.Parse<SysHelper.Enums.SenserStatusType>(data.ActionType);
            this.ParType = SysHelper.Enums.EnumHelper.Parse<SysHelper.Enums.DeviceParameterType>(data.ParType);
            this.ParUnit = data.ParUnit;
            this.ParValue = data.ParValue.Value;
            this.ParTime = data.ParTime;
            this.IsFinish = Convert.ToBoolean(data.IsFinish);
        }
        #endregion
    }

    public class ExProcessStepParsCollection : ListBase<ExProcessStepPars> { }

    #endregion

    #endregion

    #region 工业流程执行日志对象

    #region 工艺流程执行日志主表

    /// <summary>
    /// 工艺流程执行日志主表
    /// </summary>
    public class ProcessLog : EntityBase
    {
        #region 属性
        /// <summary>
        /// 工业流程生产日志编号
        /// </summary>
        public string ProcessLogId { get; set; }
        /// <summary>
        /// 生产线编号
        /// </summary>
        public string AssemblyLineId { get; set; }
        /// <summary>
        /// 主工艺流程编号
        /// </summary>
        public string ProcessId { get; set; }
        /// <summary>
        /// 流程启动时间
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// 流程启动人/系统
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 流程完成时间
        /// </summary>
        public DateTime? FinishTime { get; set; }
        /// <summary>
        /// 流程运行状态
        /// </summary>
        public SysHelper.Enums.ProcessStatusType ProcessStatus { get; set; }
        /// <summary>
        /// 生产出物料类型
        /// </summary>
        public SysHelper.Enums.MaterialTypeEnum ProduceMaterialType { get; set; }
        /// <summary>
        /// 生产的物料产量（KG）
        /// </summary>
        public decimal? Production { get; set; }
        /// <summary>
        /// 总共花费的时间（分钟单位）
        /// </summary>
        public int? TakeTime { get; set; }
        #endregion

        #region 构造函数
        public ProcessLog() { }

        internal ProcessLog(DataModels.LProcesslog data) : this()
        {
            this.Id = data.Id;
            this.ProcessLogId = data.ProcessLogId;
            this.AssemblyLineId = data.AssemblyLineId;
            this.ProcessId = data.ProcessId;
            this.Created = data.Created;
            this.CreateUser = data.CreateUser;
            this.FinishTime = data.FinishTime;
            this.ProcessStatus = SysHelper.Enums.EnumHelper.Parse<SysHelper.Enums.ProcessStatusType>(data.ProcessStatus);
            this.ProduceMaterialType = SysHelper.Enums.EnumHelper.Parse<SysHelper.Enums.MaterialTypeEnum>(data.ProduceMaterialType);
            this.Production = data.Production;
            this.TakeTime = data.TakeTime;
        }
        #endregion

        #region 转换方法

        internal DataModels.LProcesslog ToData()
        {
            DataModels.LProcesslog data = new DataModels.LProcesslog();

            data.Id = this.Id;
            data.ProcessLogId = this.ProcessLogId;
            data.AssemblyLineId = this.AssemblyLineId;
            data.ProcessId = this.ProcessId;
            data.Created = this.Created;
            data.CreateUser = this.CreateUser;
            data.FinishTime = this.FinishTime;
            data.ProcessStatus = this.ProcessStatus.ToString();
            data.ProduceMaterialType = this.ProduceMaterialType.ToString();
            data.Production = this.Production;
            data.TakeTime = this.TakeTime;

            return data;
        }

        #endregion
    }

    /// <summary>
    /// 工艺流程执行日志主表集合
    /// </summary>
    public class ProcessLogCollection : ListBase<ProcessLog> { }

    #endregion

    #region 工艺子流程执行日志表
    /// <summary>
    /// 工艺子流程执行日志表
    /// </summary>
    public class ExProcessLog:EntityBase
    {
        #region 属性
        /// <summary>
        /// 工艺子流程生产日志编号
        /// </summary>
        public string ExProcessLogId { get; set; }
        /// <summary>
        /// 工艺流程生产日志编号
        /// </summary>
        public string ProcessLogId { get; set; }

        /// <summary>
        /// 工艺子流程编号
        /// </summary>
        public string ExProcessId { get; set; }
        /// <summary>
        /// 流程启动时间
        /// </summary>
        public DateTime? Created { get; set; }
        /// <summary>
        /// 流程启动人/系统
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 流程完成时间
        /// </summary>
        public DateTime? FinishTime { get; set; }
        /// <summary>
        /// 流程运行状态
        /// </summary>
        public SysHelper.Enums.ProcessStatusType ProcessStatus { get; set; }
        /// <summary>
        /// 生产出物料类型
        /// </summary>
        public SysHelper.Enums.MaterialTypeEnum ProduceMaterialType { get; set; }
        /// <summary>
        /// 生产的物料产量
        /// </summary>
        public decimal? Production { get; set; }
        /// <summary>
        /// 总共花费的时间（分钟单位)
        /// </summary>
        public int? TakeTime { get; set; }
        #endregion

        #region 构造函数
        public ExProcessLog()
        {

        }

        internal ExProcessLog(DataModels.LExprocesslog data) : this()
        {
            this.Id = data.Id;
            this.ExProcessLogId = data.ExProcessLogId;
            this.ProcessLogId = data.ProcessLogId;
            this.ExProcessId = data.ExProcessId;
            this.Created = data.Created;
            this.CreateUser = data.CreateUser;
            this.FinishTime = data.FinishTime;
            this.ProcessStatus = SysHelper.Enums.EnumHelper.Parse<SysHelper.Enums.ProcessStatusType>(data.ProcessStatus);
            this.ProduceMaterialType = SysHelper.Enums.EnumHelper.Parse<SysHelper.Enums.MaterialTypeEnum>(data.ProduceMaterialType);
            this.Production = data.Production;
            this.TakeTime = data.TakeTime;
        }
        #endregion

        #region 转换方法
        internal DataModels.LExprocesslog ToData()
        {
            DataModels.LExprocesslog data = new DataModels.LExprocesslog();

            data.Id = this.Id;
            data.ExProcessLogId = this.ExProcessLogId;
            data.ProcessLogId = this.ProcessLogId;
            data.ExProcessId = this.ExProcessId;
            data.Created = this.Created;
            data.CreateUser = this.CreateUser;
            data.FinishTime = this.FinishTime;
            data.ProcessStatus = this.ProcessStatus.ToString();
            data.ProduceMaterialType = this.ProduceMaterialType.ToString();
            data.Production = this.Production;
            data.TakeTime = this.TakeTime;

            return data;
        }
        #endregion
    }
    /// <summary>
    /// 工艺子流程执行日志表集合
    /// </summary>
    public class ExProcessLogCollection : ListBase<ExProcessLog> { }

    #endregion

    #region 工艺子流程步骤日志表
    /// <summary>
    /// 工艺子流程步骤日志表
    /// </summary>
    public class ExProcessStepLog : EntityBase
    {
        #region 属性
        /// <summary>
        /// 工艺子流程步骤日志编号
        /// </summary>
        public string ExProcessStepLogId { get; set; }

        /// <summary>
        /// 工艺子流程生产日志编号
        /// </summary>
        public string ExProcessLogId { get; set; }

        /// <summary>
        /// 流程启动时间
        /// </summary>
        public DateTime? Created { get; set; }

        /// <summary>
        /// 流程启动人/系统
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 对应的工艺设备编号
        /// </summary>
        public string ProcessDeviceId { get; set; }
        /// <summary>
        /// 对应的设备编号
        /// </summary>
        public string DeviceId { get; set; }
        /// <summary>
        /// 执行子流程步骤编号
        /// </summary>
        public string StepId { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        public SysHelper.Enums.DeviceActionType ActionType { get; set; }
        #endregion

        #region 构造函数
        public ExProcessStepLog()
        {

        }

        internal ExProcessStepLog(DataModels.LExprocesssteplog data) : this()
        {
            this.Id = data.Id;
            this.ExProcessStepLogId = data.ExProcessStepLogId;
            this.ExProcessLogId = data.ExProcessLogId;
            this.Created = data.Created;
            this.CreateUser = data.CreateUser;
            this.ProcessDeviceId = data.ProcessDeviceId;
            this.DeviceId = data.DeviceId;
            this.StepId = data.StepId;
            this.ActionType = SysHelper.Enums.EnumHelper.Parse<SysHelper.Enums.DeviceActionType>(data.ActionType);

        }
        #endregion

        #region 转换方法
        internal DataModels.LExprocesssteplog ToData()
        {
            DataModels.LExprocesssteplog data = new DataModels.LExprocesssteplog();

            data.Id = this.Id;
            data.ExProcessStepLogId = this.ExProcessStepLogId;
            data.ExProcessLogId = this.ExProcessLogId;
            data.Created = this.Created;
            data.CreateUser = this.CreateUser;
            data.ProcessDeviceId = this.ProcessDeviceId;
            data.DeviceId = this.DeviceId;
            data.StepId = this.StepId;
            data.ActionType = this.ActionType.ToString();

            return data;
        }
        #endregion
    }
    /// <summary>
    ///工艺子流程步骤日志表集合
    /// </summary>
    public class ExProcessStepLogCollection : ListBase<ExProcessStepLog> { }

    #endregion

    #region 工艺子流程步骤参数日志表
    /// <summary>
    /// 工艺子流程步骤参数日志表
    /// </summary>
    public class ExProcessStepParsLog
    {
        #region 属性
        /// <summary>
        /// 序列编号
        /// </summary>
        public string ParId { get; set; }
        /// <summary>
        /// 工艺子流程步骤日志编号
        /// </summary>
        public string ExProcessStepLogId { get; set; }
        /// <summary>
        /// 对应的工艺设备的传感器编码
        /// </summary>
        public string SensorId { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime? Created { get; set; }
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
        public ExProcessStepParsLog() { }

        internal ExProcessStepParsLog(DataModels.LExprocessstepparslog data)
        {
            this.ParId = data.ParId;
            this.ExProcessStepLogId = data.ExProcessStepLogId;
            this.SensorId = data.SensorId;
            this.Created = data.Created;
            this.ParType = SysHelper.Enums.EnumHelper.Parse<SysHelper.Enums.DeviceParameterType>(data.ParType);
            this.ParUnit = data.ParUnit;
            this.ParValue = data.ParValue;
        }
        #endregion

        #region 转换方法
        internal DataModels.LExprocessstepparslog ToData()
        {
            DataModels.LExprocessstepparslog data = new DataModels.LExprocessstepparslog();

            data.ParId = this.ParId;
            data.ExProcessStepLogId = this.ExProcessStepLogId;
            data.SensorId = this.SensorId;
            data.Created = this.Created;
            data.ParType = this.ParType.ToString();
            data.ParUnit = this.ParUnit;
            data.ParValue = this.ParValue;

            return data;
        }
        #endregion
    }
    /// <summary>
    /// 工艺子流程步骤参数日志表集合
    /// </summary>
    public class ExProcessStepParsLogCollection : ListBase<ExProcessStepParsLog> { }

    #endregion

    #endregion
}
