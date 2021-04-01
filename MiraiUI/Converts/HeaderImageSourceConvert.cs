using MiraiUI.Items;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MiraiUI.Converts
{

    public class HeaderImageSourceConvert : IValueConverter
    {
        /// <summary>
        /// 项转换
        /// </summary>
        public List<ValueItem> ItemChanges { get; set; } = new List<ValueItem>();

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string path = value as string;
            OtherChange other = null;
            foreach (var item in ItemChanges)
            {
                if (value == null && item.From == null)
                {
                    return item.To;
                }
                else if (value.Equals(item.From))
                {
                    return item.To;
                }
                if (item is OtherChange)
                {
                    other = item as OtherChange;
                }
            }
            if (other != null)
            {
                return other.To;
            }
            return new BitmapImage(new Uri(path));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
