using MiraiUI.Expends;
using MiraiUI.Items;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiraiUI.Models
{
    /// <summary>
    /// mirai 控制台
    /// </summary>
    public class MiraiConsole : IDisposable
    {
        /// <summary>
        /// javar路径
        /// </summary>
        public static string JavaPath { get; set; } = @"Mirai\runtime\bin\java.exe";
        /// <summary>
        /// 日志
        /// </summary>
        public Logger Logger => Logger.LogInstance;
        /// <summary>
        /// 依赖
        /// </summary>
        public static string Libs { get; set; } = @"Mirai\libs\*";
        /// <summary>
        /// 启动类
        /// </summary>
        public static string MainClass { get; set; } = "net.mamoe.mirai.console.terminal.MiraiConsoleTerminalLoader";
        /// <summary>
        /// 控制台
        /// </summary>
        public Process Process { get; set; }
        /// <summary>
        /// 机器人
        /// </summary>
        public Bot Bot { get; set; }
        /// <summary>
        /// 进程id
        /// </summary>
        public int ProcessId { get; set; }
        /// <summary>
        /// shjuru 
        /// </summary>
        private StreamWriter Writer => Process.StandardInput;
        /// <summary>
        /// 输出
        /// </summary>
        private StreamReader Reader => Process.StandardOutput;
        /// <summary>
        /// 构造函数
        /// </summary>
        public MiraiConsole(Bot bot)
        {
            this.Bot = bot;
            Process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo(JavaPath, "-cp {0} {1}".Format(Libs, MainClass));
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;//不使用系统外壳程序启动，重定向时此处必须设为false
            startInfo.RedirectStandardOutput = true; //重定向输出，而不是默认的显示在dos控制台上
            startInfo.RedirectStandardInput = true; //重定向输入，使用程序进行重定向输入
            startInfo.RedirectStandardError = true;
            
            Process.StartInfo = startInfo;
            //控制台输出数据接受
            Process.OutputDataReceived += Process_OutputDataReceived;
            Process.ErrorDataReceived += Process_ErrorDataReceived;
            //Writer.AutoFlush = true;
        }
        /// <summary>
        /// 错误信息接受
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Logger.Error(e.Data);
        }

        /// <summary>
        /// 接受事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Logger.Log(e.Data);
        }
        /// <summary>
        /// 输入命令
        /// </summary>
        /// <param name="command"></param>
        public void Command(string command)
        {
            Writer.WriteLine(command);
        }

        /// <summary>
        /// 启动
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            if (!Process.Start()) return false;
            Process.BeginOutputReadLine();
            Process.BeginErrorReadLine();
            ProcessId = Process.Id;
            Command("{0} {1} {2}".Format("login", Bot.Account, Bot.Password));
            return true;
        }
        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            if(Process != null)
            {
                Process.CancelOutputRead();
                Process.CancelErrorRead();
                Process.Kill();
                Process.Close();
            }
        }

    }
}
