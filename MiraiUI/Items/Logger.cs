using MiraiUI.Expends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xyxandwxx.LobLib;

namespace MiraiUI.Items
{

    public class Logger
    {
        /// <summary>
        /// 日志
        /// </summary>
        public LogInfo LogInfo { get; set; }
        /// <summary>
        /// 日志
        /// </summary>
        private static Logger logger;
        /// <summary>
        /// 信息回调
        /// </summary>
        public Action<LogMessage> MessageCallBack { get; set; }
        /// <summary>
        /// 单例
        /// </summary>
        public static Logger LogInstance { get {
                if (logger == null)
                    logger = new Logger();
                return logger;
            } }


        private Logger()
        {
            LogInfo = new LogInfo();
            LogInfo.FileName = "日志";
            LogInfo.ErroStringEvent += LogInfo_ErroStringEvent;
        }
        /// <summary>
        /// 日志回调
        /// </summary>
        /// <param name="obj"></param>
        private void LogInfo_ErroStringEvent(LogMessage obj)
        {
            MessageCallBack?.Invoke(obj);
        }

        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="format"></param>
        /// <param name="pas"></param>
        public void Log(string format, params object [] pas)
        {
            LogInfo.log(format.Format(pas));
        }
        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="format"></param>
        /// <param name="pas"></param>
        public void Debug(string format, params object[] pas)
        {
            LogInfo.debug(format.Format(pas));
        }
        /// <summary>
        /// 异常日志
        /// </summary>
        /// <param name="format"></param>
        /// <param name="pas"></param>
        public void Error(string format, params object[] pas)
        {
            LogInfo.erro(format.Format(pas));
        }
    }
}
