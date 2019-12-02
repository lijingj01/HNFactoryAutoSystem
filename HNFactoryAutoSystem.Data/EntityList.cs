using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HNFactoryAutoSystem.Data
{
    #region 类基础对象

    public class EntityBase
    {

    }

    public class ListBase<T> : List<T>
    {

    }
    #endregion

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

        #endregion

        #region 构造函数

        #endregion

    }

    public class TechnologicalProcessList : ListBase<TechnologicalProcess>
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
        public decimal ParNumerical { get; set; }
        #endregion

        #region 构造函数

        #endregion
    }

    public class TeExProcessList : ListBase<TeExProcess>
    {

    }
    #endregion

    #region 工艺子流程步骤表
    public class ProcessStep:EntityBase
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
        /// 上一步骤的编号
        /// </summary>
        public string BeforeStepId { get; set; }
        /// <summary>
        /// 完成步骤后出现的产品编号
        /// </summary>
        public string ResultsId { get; set; }
        #endregion

        #region 构造函数

        #endregion
    }

    public class ProcessSteps : ListBase<ProcessStep> { }
    #endregion

    #endregion


}
