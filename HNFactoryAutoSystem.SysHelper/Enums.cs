using System;
using System.ComponentModel;
using System.Reflection;

namespace HNFactoryAutoSystem.SysHelper.Enums
{
    public static class EnumHelper
    {
        public static T Parse<T>(string strEnum)
        {
            try
            {
                T c = (T)Enum.Parse(typeof(T), strEnum, true);
                return c;
            }
            catch
            {
                return (T)Enum.Parse(typeof(T), "None", true);
            }
        }

        public static T Parse<T>(int i)
        {
            if (Enum.IsDefined(typeof(T), i))
            {
                return (T)Enum.ToObject(typeof(T), i);
            }
            else
            {
                return (T)Enum.ToObject(typeof(T), 0);
            }
        }

        /// <summary>    
        /// 获取枚举项描述信息 例如GetEnumDesc(Days.Sunday)    
        /// </summary>    
        /// <param name="en">枚举项 如Days.Sunday</param>    
        /// <returns></returns>    
        public static string GetEnumDesc<T>(T en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return en.ToString();
        }

        #region 特殊的枚举中文转换
        /// <summary>
        /// 生产设备参数类型说明
        /// </summary>
        /// <param name="parameterType"></param>
        /// <returns></returns>
        public static string GetDeviceParameterTypeString(DeviceParameterType parameterType)
        {
            string strReturn = string.Empty;
            switch (parameterType)
            {
                case DeviceParameterType.W:
                    strReturn = "重量";
                    break;
                case DeviceParameterType.T:
                    strReturn = "温度";
                    break;
                case DeviceParameterType.H:
                    strReturn = "湿度";
                    break;
                case DeviceParameterType.P:
                    strReturn = "PH值";
                    break;
                case DeviceParameterType.R:
                    strReturn = "搅拌转速";
                    break;
                case DeviceParameterType.D:
                    strReturn = "时间";
                    break;
                case DeviceParameterType.Pump:
                    strReturn = "泵";
                    break;
                case DeviceParameterType.Valve:
                    strReturn = "阀门";
                    break;
                case DeviceParameterType.SW:
                    strReturn = "开关";
                    break;
                case DeviceParameterType.TR:
                    strReturn = "输送";
                    break;
                case DeviceParameterType.Hz:
                    strReturn = "频率";
                    break;
                default:
                    break;
            }
            return strReturn;
        }

        /// <summary>
        /// 获取设备状态说明
        /// </summary>
        /// <param name="parameterType"></param>
        /// <returns></returns>
        public static string GetDeviceActionTypeString(DeviceActionType actionType)
        {
            string strReturn = string.Empty;
            switch (actionType)
            {
                case DeviceActionType.P:
                    strReturn = "投放";
                    break;
                case DeviceActionType.S:
                    strReturn = "启动";
                    break;
                case DeviceActionType.M:
                    strReturn = "搅拌";
                    break;
                case DeviceActionType.E:
                    strReturn = "停止";
                    break;
                case DeviceActionType.H:
                    strReturn = "加热";
                    break;
                default:
                    break;
            }
            return strReturn;
        }
        /// <summary>
        /// 获取传感器的状态类型说明
        /// </summary>
        /// <param name="statusType"></param>
        /// <returns></returns>
        public static string GetSenserStatusTypeString(SenserStatusType statusType)
        {
            string strReturn = string.Empty;
            switch (statusType)
            {
                case SenserStatusType.Stop:
                    strReturn = "设备停止";
                    break;
                case SenserStatusType.Run:
                    strReturn = "设备运行";
                    break;
                case SenserStatusType.Off:
                    strReturn = "阀门全关";
                    break;
                case SenserStatusType.Open:
                    strReturn = "阀门全开";
                    break;
                case SenserStatusType.Middle:
                    strReturn = "阀门中间位置";
                    break;
                case SenserStatusType.Fault:
                    strReturn = "设备故障";
                    break;
                default:
                    break;
            }
            return strReturn;
        }

        /// <summary>
        /// 获取物料类型的说明
        /// </summary>
        /// <param name="materialType"></param>
        /// <returns></returns>
        public static string GetMaterialTypeString(MaterialTypeEnum materialType)
        {
            string strReturn = string.Empty;
            switch (materialType)
            {

                case MaterialTypeEnum.Y:
                    strReturn = "原料";
                    break;
                case MaterialTypeEnum.F:
                    strReturn = "辅料";
                    break;
                case MaterialTypeEnum.AP:
                    strReturn = "辅成品";
                    break;
                case MaterialTypeEnum.C:
                    strReturn = "主成品";
                    break;
                case MaterialTypeEnum.P:
                    strReturn = "包装材料";
                    break;
                case MaterialTypeEnum.Z:
                    strReturn = "中间物料";
                    break;
                case MaterialTypeEnum.RE:
                    strReturn = "渣";
                    break;
                case MaterialTypeEnum.WW:
                    strReturn = "废水";
                    break;
                case MaterialTypeEnum.WL:
                    strReturn = "废液";
                    break;
                case MaterialTypeEnum.WG:
                    strReturn = "废气";
                    break;
                default:
                    break;
            }
            return strReturn;
        }
        #endregion
    }

    /// <summary>
    /// 生产设备参数类型
    /// </summary>
    public enum DeviceParameterType
    {
        None = 0,
        /// <summary>
        /// 重量
        /// </summary>
        W = 1,
        /// <summary>
        /// 温度
        /// </summary>
        T = 2,
        /// <summary>
        /// 湿度
        /// </summary>
        H = 3,
        /// <summary>
        /// PH值
        /// </summary>
        P = 4,
        /// <summary>
        /// 搅拌转速
        /// </summary>
        R = 5,
        /// <summary>
        /// 时间
        /// </summary>
        D = 6,
        /// <summary>
        /// 泵
        /// </summary>
        Pump = 7,
        /// <summary>
        /// 阀门
        /// </summary>
        Valve = 8,
        /// <summary>
        /// 开关
        /// </summary>
        SW = 9,
        /// <summary>
        /// 输送
        /// </summary>
        TR = 10,
        /// <summary>
        /// 频率
        /// </summary>
        Hz = 11
    }
    /// <summary>
    /// 生产设备的操作类型
    /// </summary>
    public enum DeviceActionType
    {
        /// <summary>
        /// 无动作
        /// </summary>
        None = 0,
        /// <summary>
        /// 投放材料
        /// </summary>
        P = 1,
        /// <summary>
        /// 启动搅拌
        /// </summary>
        S = 2,
        /// <summary>
        /// 保持搅拌
        /// </summary>
        M = 3,
        /// <summary>
        /// 关闭搅拌
        /// </summary>
        E = 4,
        /// <summary>
        /// 加热
        /// </summary>
        H = 5
    }

    /// <summary>
    /// 传感器的状态类型
    /// </summary>
    public enum SenserStatusType
    {
        /// <summary>
        /// 无动作
        /// </summary>
        None = 0,
        /// <summary>
        /// 设备停止
        /// </summary>
        Stop = 1,
        /// <summary>
        /// 设备运行
        /// </summary>
        Run = 2,
        /// <summary>
        /// 阀门全关
        /// </summary>
        Off = 3,
        /// <summary>
        /// 阀门全开
        /// </summary>
        Open = 4,
        /// <summary>
        /// 阀门中间位置
        /// </summary>
        Middle = 5,
        /// <summary>
        /// 设备故障
        /// </summary>
        Fault = 6,
    }

    /// <summary>
    /// 流程的状态枚举
    /// </summary>
    public enum ProcessStatusType
    {
        /// <summary>
        /// 无动作
        /// </summary>
        None = 0,
        /// <summary>
        /// 启动
        /// </summary>
        Start = 1,
        /// <summary>
        /// 运行
        /// </summary>
        Move = 2,
        /// <summary>
        /// 暂停
        /// </summary>
        Sus = 3,
        /// <summary>
        /// 结束
        /// </summary>
        End = 4
    }

    /// <summary>
    /// 物料类型枚举
    /// </summary>
    public enum MaterialTypeEnum
    {
        /// <summary>
        /// 无动作
        /// </summary>
        None = 0,
        /// <summary>
        /// 原料
        /// </summary>
        Y = 1,
        /// <summary>
        /// 辅料
        /// </summary>
        F = 2,
        /// <summary>
        /// 辅成品
        /// </summary>
        AP = 3,
        /// <summary>
        /// 主成品
        /// </summary>
        C = 4,
        /// <summary>
        /// 包装材料
        /// </summary>
        P = 5,
        /// <summary>
        /// 中间物料
        /// </summary>
        Z = 6,
        /// <summary>
        /// 渣
        /// </summary>
        RE = 7,
        /// <summary>
        /// 废水
        /// </summary>
        WW = 8,
        /// <summary>
        /// 废液
        /// </summary>
        WL = 9,
        /// <summary>
        /// 废气
        /// </summary>
        WG = 10
    }

    /// <summary>
    /// 设备类型
    /// </summary>
    public enum DeviceTypeEnum
    {
        /// <summary>
        /// 无类型
        /// </summary>
        [Description("无类型")]
        None = 0,
        /// <summary>
        /// 桶
        /// </summary>
        [Description("桶")]
        BA = 1,
        /// <summary>
        /// 沉淀池
        /// </summary>
        [Description("沉淀池")]
        PP = 2,
        /// <summary>
        /// 釜
        /// </summary>
        [Description("釜")]
        KE = 3,
        /// <summary>
        /// 焙烧炉
        /// </summary>
        [Description("焙烧炉")]
        RF = 4,
        /// <summary>
        /// 氢气炉
        /// </summary>
        [Description("氢气炉")]
        HF = 5,
        /// <summary>
        /// 真空打包
        /// </summary>
        [Description("真空打包")]
        VP = 6,
        /// <summary>
        /// 加热
        /// </summary>
        [Description("加热")]
        RE = 7,
        /// <summary>
        /// 净水
        /// </summary>
        [Description("净水")]
        WP = 8,
        /// <summary>
        /// 蒸发
        /// </summary>
        [Description("蒸发")]
        EV = 9
    }


    #region 传感器状态相关枚举
    /// <summary>
    /// 泵状态值
    /// </summary>
    public enum PumpStatusEnum
    {
        /// <summary>
        /// 无动作
        /// </summary>
        None = 0,
        /// <summary>
        /// 设备停止
        /// </summary>
        Stop = 1,
        /// <summary>
        /// 设备运行
        /// </summary>
        Run = 2,
        /// <summary>
        /// 设备故障
        /// </summary>
        Fault = 3
    }
    /// <summary>
    /// 阀门状态值
    /// </summary>
    public enum ValveStatusEnum
    {
        /// <summary>
        /// 无动作
        /// </summary>
        None = 0,
        /// <summary>
        /// 阀门全关
        /// </summary>
        Off = 1,
        /// <summary>
        /// 阀门全开
        /// </summary>
        Open = 2,
        /// <summary>
        /// 阀门中间位置
        /// </summary>
        Middle = 3,
        /// <summary>
        /// 阀门故障
        /// </summary>
        Fault = 4
    }
    /// <summary>
    /// 过程值状态
    /// </summary>
    public enum NumberValueEnum
    {
        /// <summary>
        /// 无动作
        /// </summary>
        None = 0,
        /// <summary>
        /// 读取
        /// </summary>
        Read = 1,
        /// <summary>
        /// 写入
        /// </summary>
        Write = 2
    }
    #endregion

    #region 通用返回数据枚举
    /// <summary>
    /// 返回值通用枚举
    /// </summary>
    public enum ResultEnum
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("success")]
        SUCCESS = 0,
        /// <summary>
        /// 
        /// </summary>
        [Description("未知错误")]
        FAILED = 1,
        /// <summary>
        /// 
        /// </summary>
        [Description("服务暂不可用")]
        NOSERVICE = 2,
        /// <summary>
        /// 
        /// </summary>
        [Description("未知方法")]
        UNSUPPORTEDMETHOD = 3,
        /// <summary>
        /// 
        /// </summary>
        [Description("请求参数无效")]
        INVALIDPARAMETER = 4,
        /// <summary>
        /// 
        /// </summary>
        [Description("读取配置文件出错")]
        READCONFIGFAILED = 5,
        /// <summary>
        /// 
        /// </summary>
        [Description("数据库连接出错")]
        DBCONECTIONFAILED = 6,
        /// <summary>
        /// 
        /// </summary>
        [Description("notFound [数据不存在 或者 数据为空]")]
        NOT_FOUND = -1,
        /// <summary>
        /// 
        /// </summary>
        [Description("error [未知异常]")]
        ERROR = -2,
        /// <summary>
        /// 
        /// </summary>
        [Description("parameter error [参数异常:参数为空或者参数类型不符]")]
        PARAMETER_ERROR = -3


    }
    #endregion
}
