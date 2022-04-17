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

namespace TicTacToe5.components
{
    /// <summary>
    /// Логика взаимодействия для PlayerNameSelectorDialog.xaml
    /// </summary>
    public partial class PlayerNameSelectorDialog : Window
    {
        public string Player1Name { get; private set; }
        public string Player2Name { get; private set; }
        public PlayerNameSelectorDialog()
        {
            InitializeComponent();
        }

        private void dialogMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if(tb.Text.Length > 30)
            {
                tb.Text = tb.Text.Substring(0, 30);
                tb.CaretIndex = 30;
            }
            Player1Name = player1Tb.Text.Trim();
            Player2Name = player2Tb.Text.Trim();
            btnAccept.IsEnabled = !(String.IsNullOrEmpty(Player1Name) || String.IsNullOrEmpty(Player2Name) || Player1Name == Player2Name);
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (!(String.IsNullOrEmpty(Player1Name) || String.IsNullOrEmpty(Player2Name) || Player1Name == Player2Name))
            {
                Close();
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Player1Name = "";
            Player2Name = "";
            Close();
        }
    }
}
