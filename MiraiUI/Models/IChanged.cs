using MiraiUI.Expends;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MiraiUI.Models
{
    public abstract class IChanged : INotifyPropertyChanged
    {
        /// <summary>
        /// 属性变更通知
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 改变
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        public void Changed<T>(Expression<Func<T>> expression)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(expression.GetExpressionProperty()));
        }

    }
}
