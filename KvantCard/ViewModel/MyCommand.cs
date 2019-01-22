using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KvantCard
{
    public class MyCommand : ICommand
    {
        public delegate void SomeMethod();
        SomeMethod action;
        public MyCommand(SomeMethod action)
        {
            this.action = action;
        }
        public delegate void SomeMethodWithArg(object p);
        SomeMethodWithArg action2;
        public MyCommand(SomeMethodWithArg action2)
        {
            this.action2 = action2;
        }
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            if (parameter == null && action == null)
                return;
            else if (parameter == null)
                action();
            else
                action2(parameter);
        }
    }
}
