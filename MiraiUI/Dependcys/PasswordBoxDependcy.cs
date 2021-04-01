using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MiraiUI.Dependcys
{
    public class PasswordBoxDependcy
    {
        /// <summary>
        /// 密码值
        /// </summary>
        public static DependencyProperty PasswordValueProperty = DependencyProperty.RegisterAttached("PasswordValue", typeof(string), typeof(PasswordBoxDependcy), new FrameworkPropertyMetadata(string.Empty, PasswordValueChanged));

        /// <summary>
        /// 值改变
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void PasswordValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox password = (PasswordBox)d;
            if (password == null) return;

            password.PasswordChanged -= Password_PasswordChanged;

            string newPassword = (string)e.NewValue;
            if (!password.Password.Equals(newPassword))
            {
                password.Password = newPassword;
            }

            password.PasswordChanged += Password_PasswordChanged;
        }
        /// <summary>
        /// 密码改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox password = (PasswordBox)sender;
            SetPasswordValue(password, password.Password);
        }

        /// <summary>
        /// 获取依赖值
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        public static string GetPasswordValue(DependencyObject dp)
        {
            return (string)dp.GetValue(PasswordValueProperty);
        }
        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="d"></param>
        /// <param name="vallue"></param>
        public static void SetPasswordValue(DependencyObject d, string value)
        {
            d.SetValue(PasswordValueProperty, value);
        }
    }
}
