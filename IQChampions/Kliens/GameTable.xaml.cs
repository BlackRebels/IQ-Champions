using ServiceLibrary;
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
using System.Windows.Shapes;

namespace iqchampion_design
{
    public partial class GameTable : Window
    {
        Menu parent = null;

        private string user
        {
            get { return parent.User; }
        }

        public GameTable(Menu parent)
        {
            InitializeComponent();
            this.parent = parent;
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
