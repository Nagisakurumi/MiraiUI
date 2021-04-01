using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace MiraiUI.Models
{
    [AddINotifyPropertyChangedInterface]
    public class Plugin
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActivated { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime DateTime { get; set; }
        /// <summary>
        /// 大小
        /// </summary>
        public long Size { get; set; }
    }
}
