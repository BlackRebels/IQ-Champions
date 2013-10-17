using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfApplication1
{
    class ButtonCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            if (parameter != null)
            {
                return !String.IsNullOrEmpty(parameter.ToString());
            }
            return false;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            MessageBox.Show("Hello " + parameter.ToString());
        }

        public ButtonCommand(INotifyPropertyChanged notify)
        {
            notify.PropertyChanged += notify_PropertyChanged;
        }

        void notify_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, e);
            }
        }
    }
}
