using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiraiUI.Expends
{
    public static class StringExpend
    {
        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="format"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public static string Format(this string format, params object [] ps)
        {
            try
            {
                return string.Format(format, ps);
            }
            catch (Exception)
            {
                return format;
            }
        }

    }
}
