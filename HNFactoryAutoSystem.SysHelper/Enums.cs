using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        Weight = 1,
        /// <summary>
        /// 温度
        /// </summary>
        Temperature = 2,
        /// <summary>
        /// 湿度
        /// </summary>
        Humidity = 3,
        /// <summary>
        /// PH值
        /// </summary>
        PH = 4,
        /// <summary>
        /// 搅拌转速
        /// </summary>
        RPM = 5
    }
    /// <summary>
    /// 生产设备的操作类型
    /// </summary>
    public enum DeviceActionType
    {
        None = 0,
        /// <summary>
        /// 投放材料
        /// </summary>
        Putin = 1,
        /// <summary>
        /// 启动搅拌
        /// </summary>
        StartStir = 2,
        /// <summary>
        /// 保持搅拌
        /// </summary>
        KeepStir = 3,
        /// <summary>
        /// 关闭搅拌
        /// </summary>
        CloseStir = 4
    }
}
