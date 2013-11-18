using iqchampion_design.ServiceReference;
using IQChampionsServiceLibrary;
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
using System.Windows.Shapes;
using System.ComponentModel;

namespace iqchampion_design
{
    public partial class GameTable : Window
    {
        private Menu parent = null;
        private BackgroundWorker refreshworker = null;

        private string User
        {
            get { return Login.User; }
        }
        private IQServiceClient Client
        {
            get { return Login.Client; }
        }
        private int PingPeriod
        {
            get { return Login.PingPeriod; }
        }

        public GameTable(Menu parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.Title = parent.Title;

            refreshworker = new BackgroundWorker();
            refreshworker.WorkerSupportsCancellation = true;
            refreshworker.WorkerReportsProgress = true;
            refreshworker.DoWork += refresh;
            refreshworker.ProgressChanged += setCellColor;

        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            refreshworker.RunWorkerAsync();


            /*
            APIenum ret = Client.APIping(User, null);
            while (ret != APIenum.YOU_CAN_MOVE || ret != APIenum.WAITING_FOR_MOVE)
            {
                // nem te jösz, sötét, de látsz mindent
                ret = Client.APIping(User, null);
                Thread.Sleep(Login.PingPeriod);
            }
            */
            // itt te jösz
            // lépsz, stb


            /*
            APIenum.PLAYER_CAN_MOVE;    nem te jösz
            APIenum.YOU_CAN_MOVE        te jösz
            */
        }

        private void refresh(object sender, DoWorkEventArgs e)
        {
            while (!(sender as BackgroundWorker).CancellationPending)
            {
                ServiceReference.GameTable table = Client.getGameTable(User);
                foreach (Cell c in table.Table)
                {
                    (sender as BackgroundWorker).ReportProgress(0, c);
                }
                Thread.Sleep(PingPeriod);
            }
        }

        private void setCellColor(object sender, ProgressChangedEventArgs e)
        {
            Cell c = e.UserState as Cell;
            ((Rectangle)GridGameTable.FindName("cell" + c.Row + c.Col)).Fill =
                           new SolidColorBrush(Color.FromRgb(c.Owner.Color[0], c.Owner.Color[1], c.Owner.Color[2]));

        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parent.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextEntered(object sender, KeyEventArgs e)
        {

        }


        public void usingAPI(APIenum api)
        {

            switch (api)
            {
                case APIenum.STANDBY:

                    break;

                case APIenum.GAME_STANDBY:

                    break;

                case APIenum.GAME_REFRESH:

                    break;

                case APIenum.GAME_ENDED:

                    break;

                case APIenum.PLAYER_CAN_MOVE:

                    break;

                case APIenum.YOU_CAN_MOVE:

                    break;

                case APIenum.WAITING_FOR_MOVE:

                    break;

                case APIenum.PLAYER_MOVED:

                    break;

                case APIenum.YOU_CAN_ANSWER:

                    break;

                case APIenum.WAITING_FOR_ANSWER:

                    break;

                case APIenum.PLAYERS_ANSWERED:

                    break;

                default:

                    break;
            }
        }





    }
}
