using System;
using System.Windows.Input;

namespace WebCrawlerDesktop
{
    public class CommandClass:ICommand
    {
        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }

        public event EventHandler CanExecuteChanged;
    }
}