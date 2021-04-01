using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiraiUI.CustomCommands
{
    public class SimpleCommand : ICommand
    {



        /// <summary>
        /// 时间
        /// </summary>
        public event EventHandler CanExecuteChanged;
        /// <summary>
        /// 执行动作
        /// </summary>
        public Action<object> ExecuteAction { get; set; }
        /// <summary>
        /// 是否可以执行动作
        /// </summary>
        public Func<object, bool> CanExecuteAction { get; set; }


        public SimpleCommand(Func<object, bool> canExecuteAction, Action<object> executeAction)
        {
            this.CanExecuteAction = canExecuteAction;
            this.ExecuteAction = executeAction;

        }

        public SimpleCommand(Action<object> executeAction)
        {
            this.ExecuteAction = executeAction;
        }

        public bool CanExecute(object parameter)
        {
            if(CanExecuteAction == null)
            {
                return true;
            }
            else
                return CanExecuteAction.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            ExecuteAction?.Invoke(parameter);
        }
    }
}
