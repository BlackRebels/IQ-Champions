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
        private BackgroundWorker queueworker = null;
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
            this.Title = "Bejelentkezve mint " + User;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            pingworker = new BackgroundWorker();
            pingworker.WorkerSupportsCancellation = true;
            pingworker.DoWork += ping;
            pingworker.RunWorkerCompleted += timeout;
            pingworker.RunWorkerAsync();

            queueworker = new BackgroundWorker();
            queueworker.WorkerSupportsCancellation = true;
            queueworker.WorkerReportsProgress = true;
            queueworker.DoWork += queueCheck;
            queueworker.ProgressChanged += queueProgressChanged;
            queueworker.RunWorkerCompleted += startGame;
        }
            
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (queueworker.IsBusy)
            {
                queueworker.CancelAsync();
                ButtonJatek.Content = "Játék";
            }
            if (pingworker.IsBusy)
            {
                ButtonClickLogout(sender, null);
            }
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

        private void queueCheck(object sender, DoWorkEventArgs e)
        {
            while (!Client.roomFound(User))
            {
                if ((sender as BackgroundWorker).CancellationPending)
                {
                    return;
                }
                (sender as BackgroundWorker).ReportProgress(0, Client.getQueuePosition(User));
                Thread.Sleep(Login.PingPeriod);
            }
        }

        void queueProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //kiiratni csicsa picsa

            ButtonJatek.Content = ((int)e.UserState < 1 ? "Még " + ((int)e.UserState * -1) + " játékos kell" : e.UserState + ". vagy a sorban");
        }

        void startGame(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                GameTable game = new GameTable(this);
                this.Hide();
                game.Show();
            }
        }

        bool stop = false;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            stop = !stop;
            debugbutton.Content = "Pinging is " + (stop ? "off" : "on");
        }

        private void ButtonClickGameRandom(object sender, RoutedEventArgs e)
        {
            if (queueworker.IsBusy)
            {
                Client.leaveQueue(User);
                queueworker.CancelAsync();
                ButtonJatek.Content = "Játék";
            }
            else
            {
                Client.joinQueue(User);
                queueworker.RunWorkerAsync();
            }
        }

        private void ButtonClickGameWithFriends(object sender, RoutedEventArgs e)
        {
            Lobby lobby = new Lobby(this);
            lobby.Show();
            this.Hide();
        }

        private void ButtonClickProfil(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://index.hu"); // Link to user profile
        }

        private void ButtonClickLogout(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            parent.ClearFields();
            pingworker.CancelAsync();
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
