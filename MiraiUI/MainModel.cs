using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MiraiUI.CustomCommands;
using MiraiUI.Expends;
using MiraiUI.Items;
using MiraiUI.Models;
using PropertyChanged;

namespace MiraiUI
{
    [AddINotifyPropertyChangedInterface]
    public class MainModel
    {
        /// <summary>
        /// 插件路径
        /// </summary>
        public static string PluginPath { get; set; } = @"plugins/";
        /// <summary>
        /// 当前正在设置的机器人
        /// </summary>
        public Bot Bot { get; set; } = new Bot();
        /// <summary>
        /// 被选中的
        /// </summary>
        private Bot selectedBot { get; set; }
        /// <summary>
        /// 被选中的
        /// </summary>
        public Bot SelectedBot { get => selectedBot; set {
                selectedBot = value;
                Bot = value;
            } }
        /// <summary>
        /// /列宽
        /// </summary>
        public bool IsOpen { get; set; } = false;
        /// <summary>
        /// mirai控制台
        /// </summary>
        public MiraiConsole MiraiConsole { get; set; }
        /// <summary>
        /// 机器人集合
        /// </summary>
        public ObservableCollection<Bot> Bots { get; set; } = new ObservableCollection<Bot>();
        /// <summary>
        /// 插件集合
        /// </summary>
        public ObservableCollection<Plugin> Plugins { get; set; } = new ObservableCollection<Plugin>();

        /// <summary>
        /// 登陆命令
        /// </summary>
        public ICommand LoginCommand { get; set; }
        /// <summary>
        /// 新建命令
        /// </summary>
        public ICommand NewCommand { get; set; }
        /// <summary>
        /// 日志
        /// </summary>
        public Logger Logger => Logger.LogInstance;
        /// <summary>
        /// 构造函数
        /// </summary>
        public MainModel()
        {
            LoginCommand = new SimpleCommand(o => {

                var bot = Bots.Where(b => b.Account == Bot.Account).FirstOrDefault();
                if(bot != null && bot.IsLogin)
                {
                    Logger.Log("账号 : {0}，已经登陆了不要重复登陆!");
                    return;
                }
                if (bot == null)
                {
                    bot = new Bot() { Account = Bot.Account, IsLogin = false, Password = Bot.Password };
                    Bots.Add(bot);
                }
                if(MiraiConsole != null)
                {
                    MiraiConsole.Dispose();
                    MiraiConsole = null;
                }
                Logger.Log("开始登陆账号 : {0}", Bot.Account);
                MiraiConsole = new MiraiConsole(bot);
                MiraiConsole.Start();

            });

            NewCommand = new SimpleCommand(o => {
                Bot = new Bot();
                Logger.Log("请输入账号密码准备登陆新的账号!");
            });

            LoadPlugins();
        }

        /// <summary>
        /// 加载插件
        /// </summary>
        private void LoadPlugins()
        {
            if (!Directory.Exists(PluginPath)) return;
            string []files = Directory.GetFiles(PluginPath);
            foreach (var item in files)
            {
                FileInfo info = new FileInfo(item);
                Plugins.Add(new Plugin() { Name = Path.GetFileNameWithoutExtension(item), 
                    Path = item, IsActivated = true, DateTime = info.LastWriteTime, Size = info.Length / 1024
                });
            }
        }
    }
}
