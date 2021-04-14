using ControlzEx.Theming;
using MahApps.Metro.Controls;
using MiraiUI.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using xyxandwxx.LobLib;
using MiraiUI.Expends;

namespace MiraiUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        /// <summary>
        /// 日志
        /// </summary>
        public Logger Logger => Logger.LogInstance;
        /// <summary>
        /// 数据
        /// </summary>
        public MainModel Model { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            //ThemeManager.Current.ChangeTheme(this, "Dark.Red");

            Logger.MessageCallBack += MessageCallBack;
            this.Loaded += MainWindow_Loaded;
            this.Closing += MainWindow_Closing;
        }
        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                Model.SaveConfig();
                if(Model.MiraiConsole != null)
                {
                    Model.MiraiConsole.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Model = new MainModel();
                this.DataContext = Model;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }
        }

        /// <summary>
        /// 日志回调
        /// </summary>
        /// <param name="obj"></param>
        private void MessageCallBack(LogMessage log)
        {
            this.Dispatcher.BeginInvoke(new Action<LogMessage>(obj => {
                string content = "[{0} - {1}] : {2}".Format(obj.Type.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), obj.NormalMessage + obj.DetailMessage + "\n");
                if (LogBox.Inlines.Count > 300)
                {
                    LogBox.Inlines.Remove(LogBox.Inlines.First());
                }

                Run run = new Run();
                run.Text = content;
                if (obj.Type == LogErroType.Normal)
                {
                    run.Foreground = new SolidColorBrush(Colors.Green);
                }
                else if (obj.Type == LogErroType.Debug)
                {
                    run.Foreground = new SolidColorBrush(Colors.Yellow);
                }
                else
                {
                    run.Foreground = new SolidColorBrush(Colors.Red);
                }

                LogBox.Inlines.Add(run);
            }), log);
        }
        /// <summary>
        /// 输入命令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandEnter(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            TextBox box = sender as TextBox;
            if(Model.MiraiConsole != null && box.Text != null && !box.Text.Equals(""))
            {
                Model.MiraiConsole.Command(box.Text);
            }
        }
    }
}
