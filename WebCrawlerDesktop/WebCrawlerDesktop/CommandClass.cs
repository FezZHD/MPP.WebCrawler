﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WebCrawlerDesktop
{
    internal class CommandClass:ICommand
    {

        private readonly Func<Task> actionTask;

        private bool isCanExecute = true;

        internal CommandClass(Func<Task> action)
        {
            actionTask = action;
        }


        public bool CanExecute(object parameter)
        {
            return isCanExecute;
        }

        public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }


        private Task ExecuteAsync(object paremeter)
        {
            return actionTask();
        }


        public void OnCanExecuteChanged(bool status)
        {
            isCanExecute = status;
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public event EventHandler CanExecuteChanged;
    }
}