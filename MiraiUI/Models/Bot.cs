using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiraiUI.Models
{
    public class Bot : IChanged
    {
        /// <summary>
        /// 不固化
        /// </summary>
        [JsonIgnore]
        private bool isLogin = false;
        /// <summary>
        /// 是否登陆
        /// </summary>
        [JsonIgnore]
        public bool IsLogin { get => isLogin; 
            set { isLogin = value;Changed(() => IsLogin); } }
        private string path;
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get => path; set { path = value; Changed(()=> Path); } }
        private string headerPath;
        /// <summary>
        /// 头像路径
        /// </summary>
        public string HeaderPath { get => headerPath; set { headerPath = value;Changed(() => HeaderPath); } }
        private long account;
        /// <summary>
        /// 账号
        /// </summary>
        public long Account { get => account; set { account = value; Changed(() => Account); } }
        private string password;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get => password; set { password = value; Changed(() => Password); } }

    }
}
