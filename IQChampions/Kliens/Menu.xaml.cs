using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using iqchampion_design.ServiceReference;
using ServiceLibrary;

namespace iqchampion_design
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        private const int pingPeriod = 1000;
        private IQServiceClient client = null;
        private string user = null;


        Thread APIpingThread;

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            APIpingThread.Abort();
        }
        public Menu(string user)
        {
            InitializeComponent();
            this.user = user;
            client = new IQServiceClient();
            Thread pingThread = new Thread(new ThreadStart(ping));
            pingThread.IsBackground = true;
            pingThread.Start();

            APIpingThread = new Thread(new ThreadStart(APIping));
            APIpingThread.IsBackground = true;
            APIpingThread.Start();
        }
        private void ping()
        {
            while (true)
            {
                client.PingAsync(user);
                Thread.Sleep(pingPeriod);
            }
        }

        private void APIping()
        {
            while (true)
            {
               usingAPI(client.APIping("asd", "asd"));
               Thread.Sleep(pingPeriod);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://index.hu");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Lobby lobby = new Lobby();
            lobby.Show();
            this.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            GameTable game = new GameTable();
            game.Show();
        }

        public void usingAPI(APIenum api)
        {

            switch (api)
            {
                case APIenum.QUEUE_STANDBY:
                    break;

                case APIenum.ROOM_FOUND:
                    break;

                case APIenum.GAME_STARTED:
                    break;

                default:
                    MessageBox.Show("" + api.ToString());
                    break;
            }
        }
    }
}
