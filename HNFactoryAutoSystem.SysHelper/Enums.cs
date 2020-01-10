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
        D = 6
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
        /// 运行
        /// </summary>
        Run = 1,
        /// <summary>
        /// 故障
        /// </summary>
        Fault = 2,
        /// <summary>
        /// 开启
        /// </summary>
        Open = 3,
        /// <summary>
        /// 关闭
        /// </summary>
        Off = 4,
        /// <summary>
        /// 启动
        /// </summary>
        Start = 5,
        /// <summary>
        /// 停止
        /// </summary>
        Stop = 6
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
}
