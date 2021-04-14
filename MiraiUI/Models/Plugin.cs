using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiraiUI.Models
{
    public class Plugin : IChanged
    {
        private string name;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get => name; set { name = value; Changed(() => Name); } }
        private string path;
        /// <summary>
        /// 地址
        /// </summary>
        public string Path { get => path; set { path = value; Changed(() => Path); } }
        private bool isActivated;
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActivated { get => isActivated; set { isActivated = value; Changed(() => IsActivated); } }
        private DateTime dateTime;
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime DateTime { get => dateTime; set { dateTime = value; Changed(() => DateTime); } }
        private long size;
        /// <summary>
        /// 大小
        /// </summary>
        public long Size { get => size; set { size = value; Changed(() => Size); } }
    }
}
