using MiraiUI.Items;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MiraiUI.Converts
{
    public class ValueConvertToValue : IValueConverter
    {
        /// <summary>
        /// 项转换
        /// </summary>
        public List<ValueItem> ItemChanges { get; set; } = new List<ValueItem>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            OtherChange other = null;
            foreach (var item in ItemChanges)
            {
                if(value == null && item.From == null)
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
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (var item in ItemChanges)
            {
                if (value.Equals(item.To))
                {
                    return item.From;
                }
            }
            return default(object);
        }
    }
}
