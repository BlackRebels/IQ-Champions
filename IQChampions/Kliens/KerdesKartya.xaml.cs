using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace iqchampion_design
{
    /// <summary>
    /// Interaction logic for KerdesKartya.xaml
    /// </summary>
    public partial class KerdesKartya : Page
    {
        public int sorszam;

        public void setTime(string time)
        {
            this.time.Content = time; 
        }

        public void setKerdes(string kerdes, string[] valaszok)
        {
            kerdess.Content = kerdes;
            valasz1.Content = valaszok[0];
            valasz2.Content = valaszok[1];
            valasz3.Content = valaszok[2];
            valasz4.Content = valaszok[3];
        }

        public KerdesKartya(Boolean valaszolhatsz)
        {
            InitializeComponent();

            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(".\\Resources\\keret.png", UriKind.Relative));
            this.Background = myBrush;

            if(valaszolhatsz)
            {
                valaszbutton.IsEnabled = true;
            }
            else
            {
                valaszbutton.IsEnabled = false;
            }
        }

        public KerdesKartya()
        {
            InitializeComponent();
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(".\\Resources\\keret.png", UriKind.Relative));
            this.Background = myBrush;
        }

        private void valasz(object sender, RoutedEventArgs e)
        {
            if (valasz1.IsChecked == true) sorszam = 0;
            if (valasz2.IsChecked == true) sorszam = 1;
            if (valasz3.IsChecked == true) sorszam = 2;
            if (valasz4.IsChecked == true) sorszam = 3;
        }
    }
}
