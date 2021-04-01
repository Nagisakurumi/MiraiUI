using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiraiUI.Items
{
    public class ValueItem
    {
        /// <summary>
        /// 转换前的对象
        /// </summary>
        public object From { get; set; }
        /// <summary>
        /// 转换到的对象
        /// </summary>
        public object To { get; set; }
    }

    /// <summary>
    /// 其余转换
    /// </summary>
    public class OtherChange : ValueItem
    {
        public new object To { get; set; }
    }
}
