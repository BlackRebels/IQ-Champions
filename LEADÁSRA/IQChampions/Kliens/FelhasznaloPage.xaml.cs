using iqchampion_design.ServiceReference;
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
    /// Interaction logic for FelhasznaloPage.xaml
    /// </summary>
    public partial class FelhasznaloPage : Page
    {
        public IQServiceClient Client
        {
            get { return Login.Client; }
        }

        public FelhasznaloPage()
        {
            InitializeComponent();
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(".\\Resources\\keret.png", UriKind.Relative));
            this.Background = myBrush;
        }
        public FelhasznaloPage(string username)
        {
            InitializeComponent();
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(".\\Resources\\keret.png", UriKind.Relative));
            this.Background = myBrush;

            // u.Online.ToString(), u.Point.ToString(), u.State.ToString()

            String[] stats = Client.getUserStats(username);
            uname.Content = stats[0];
            switch (stats[1])
            {
                case "true" :
                    stat1.Content = "Online";
                    break;
                default:
                    stat1.Content = "Offline";
                    break;
 
            }
            stat2.Content = stats[2];
            stat3.Content = stats[3];
            stat4.Content = "";
            
        }

        public FelhasznaloPage(bool isRoom, string username)
        {
            InitializeComponent();
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(".\\Resources\\keret.png", UriKind.Relative));
            this.Background = myBrush;

            // u.Online.ToString(), u.Point.ToString(), u.State.ToString()

            String[] stats = Client.getUserStats(username);
            uname.Content = stats[0];
            switch (stats[1])
            {
                case "true":
                    stat1.Content = "Online";
                    break;
                default:
                    stat1.Content = "Offline";
                    break;

            }
            stat2.Content = stats[2];
            stat3.Content = stats[3];
            stat4.Content = "";

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
