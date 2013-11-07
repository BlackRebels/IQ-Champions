using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApplication1
{
    class ViewModel : INotifyPropertyChanged
    {
        public ICommand ButtonComm { get; set; }        
        public event PropertyChangedEventHandler PropertyChanged;
        private String name;

        public String Name
        {
            get { return name; }
            set
            {
                name = value;
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public ViewModel()
        {
            ButtonComm = new ButtonCommand(this);
        }
    }
}
