using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace MiraiUI.Models
{
    [AddINotifyPropertyChangedInterface]
    public class Bot
    {
        /// <summary>
        /// 是否登陆
        /// </summary>
        public bool IsLogin { get; set; } = false;
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 头像路径
        /// </summary>
        public string HeaderPath { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public long Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

    }
}
