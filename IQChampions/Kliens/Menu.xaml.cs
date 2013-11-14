using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
    public partial class Menu : Window
    {
        private Login parent = null;
        private BackgroundWorker pingworker = null;
        private Thread APIpingThread = null;

        public string User
        {
            get { return parent.User; }
        }
        public IQServiceClient Client
        {
            get { return Login.Client; }
        }

        public Menu(Login parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            pingworker = new BackgroundWorker();
            pingworker.WorkerSupportsCancellation = true;
            pingworker.DoWork += ping;
            pingworker.RunWorkerCompleted += timeout;
            pingworker.RunWorkerAsync();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            //APIpingThread.Abort();
            parent.Show();
            Cursor = Cursors.Arrow;
        }

        private void ping(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                int sleepdur = 250;
                for (int i = 0; i < Login.PingPeriod / sleepdur; i++)
                {
                    if ((sender as BackgroundWorker).CancellationPending)
                    {
                        Client.Logout(User);
                        return;
                    }
                    Thread.Sleep(sleepdur);
                }
                if (!stop && !Login.Client.Ping(User))
                {
                    MessageBox.Show("Időtúllépés!");
                    return;
                }
            }
        }

        private void timeout(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        bool stop = false;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            stop = !stop;
            debugbutton.Content = "Pinging is " + (stop ? "off" : "on");
        }
        private void ButtonClickLogout(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            parent.ClearFields();
            pingworker.CancelAsync();
        }

        private void ButtonClickProfil(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://index.hu"); // Link to user profile
        }

        private void ButtonClickGameWithFriends(object sender, RoutedEventArgs e)
        {
            Lobby lobby = new Lobby(this);
            lobby.Show();
            this.Hide();
        }

        private void ButtonClickGameRandom(object sender, RoutedEventArgs e)
        {
            Client.joinQueue(User);

            // befejezni
            APIenum ret = Client.APIping(User, null);
            while (ret != APIenum.ROOM_FOUND)
            {
                ret = Client.APIping(User, null);
                Client.getQueuePosition(User); //kiiratni csicsa picsa
                Thread.Sleep(Login.PingPeriod);
            }
            Client.joinFoundRoom(User,null);

            ret = Client.APIping(User, null);
            while (ret != APIenum.ROOM_STANDBY)
            {
                ret = Client.APIping(User, null);
                Client.getQueuePosition(User); //kiiratni csicsa picsa
                Thread.Sleep(Login.PingPeriod);
            }

            GameTable game = new GameTable(this);
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
