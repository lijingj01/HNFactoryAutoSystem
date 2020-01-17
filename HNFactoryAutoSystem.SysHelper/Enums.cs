using System;

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
        Valve = 8
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
}
