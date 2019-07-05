using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Tms_Travel.ViewModel.Commands
{
    public class PicCommand : ICommand
    {
        private PicVM viewModel;
        

        public PicCommand(PicVM viewModel)
        {
           this.viewModel = viewModel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
           //to do
        }
    }
}
