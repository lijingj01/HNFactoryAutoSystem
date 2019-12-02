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
        public string ProcessId { get; set; }

        public string ProcessTitle { get; set; }

        #endregion

        #region 构造函数

        #endregion

    }

    public class TechnologicalProcessList : ListBase<TechnologicalProcess>
    {

    }
    #endregion

    #endregion
}
