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
using Newtonsoft.Json;

namespace MiraiUI
{
    public class MainModel : IChanged
    {
        /// <summary>
        /// 插件路径
        /// </summary>
        public static string PluginPath { get; set; } = @"plugins/";
        /// <summary>
        /// 配置文件地址
        /// </summary>
        public static string ConfigPath { get; set; } = @"config/";
        /// <summary>
        /// 配置
        /// </summary>
        public static string ConfigFilePath { get; set; } = @"config/miraiui.config";

        private Bot bot = new Bot();
        /// <summary>
        /// 当前正在设置的机器人
        /// </summary>
        [JsonIgnore]
        public Bot Bot { get => bot; set { 
                bot = value; Changed(() => Bot); } }
        /// <summary>
        /// 被选中的
        /// </summary>
        [JsonIgnore]
        private Bot selectedBot { get; set; }
        /// <summary>
        /// 被选中的
        /// </summary>
        [JsonIgnore]
        public Bot SelectedBot { get => selectedBot; set {
                selectedBot = value;
                Bot = value;
                Changed(()=> SelectedBot);
            } }

        private bool isOpen = false;
        /// <summary>
        /// /列宽
        /// </summary>
        public bool IsOpen { get => isOpen; set { isOpen = value; Changed(() => IsOpen); } }
        /// <summary>
        /// mirai控制台
        /// </summary>
        [JsonIgnore]
        public MiraiConsole MiraiConsole { get; set; }
        /// <summary>
        /// 机器人集合
        /// </summary>
        public ObservableCollection<Bot> Bots { get; set; } = new ObservableCollection<Bot>();
        /// <summary>
        /// 插件集合
        /// </summary>
        [JsonIgnore]
        public ObservableCollection<Plugin> Plugins { get; set; } = new ObservableCollection<Plugin>();
        private ICommand loginCommand;
        /// <summary>
        /// 登陆命令
        /// </summary>
        public ICommand LoginCommand { get => loginCommand; set { loginCommand = value; Changed(() => LoginCommand); } }
        private ICommand newCommand;
        /// <summary>
        /// 新建命令
        /// </summary>
        public ICommand NewCommand { get => newCommand; set { newCommand = value; Changed(() => NewCommand); } }
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
                    this.SelectedBot = bot;
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
                SelectedBot = null;
                Logger.Log("请输入账号密码准备登陆新的账号!");
            });
            LoadConfig();
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
        /// <summary>
        /// 保存配置
        /// </summary>
        public void SaveConfig()
        {
            if (Directory.Exists(ConfigPath))
            {
                Directory.CreateDirectory(ConfigPath);
            }
            System.IO.File.WriteAllBytes(ConfigFilePath, System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Bots)));
        }
        /// <summary>
        /// 加载配置
        /// </summary>
        public void LoadConfig()
        {
            if (File.Exists(ConfigFilePath))
            {
                this.Bots = JsonConvert.DeserializeObject<ObservableCollection<Bot>>(Encoding.UTF8.GetString(File.ReadAllBytes(ConfigFilePath)));
            }
            foreach (var item in this.Bots)
            {
                item.IsLogin = false;
            }
        }
    }
}
