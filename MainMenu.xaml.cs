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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TicTacToe5.components;

namespace TicTacToe5
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("Down");
        }

        private void button_MouseLeave(object sender, MouseEventArgs e)
        {
            Console.WriteLine("Leave");
        }

        private void button_MouseEnter(object sender, MouseEventArgs e)
        {
            Console.WriteLine("Enter");
        }

        private void quitButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
            
        }

        private void button1Player_Click(object sender, RoutedEventArgs e)
        {
            IGameProcessor gp = new SinglePlayerGameProcessor(10);
            Hide();
            MainWindow mw = new MainWindow(gp);
            mw.ShowDialog();
            Show();
        }

        private void button2Players_Click(object sender, RoutedEventArgs e)
        {
            
            PlayerNameSelectorDialog dialog = new PlayerNameSelectorDialog();
            dialog.ShowDialog();
            if(String.IsNullOrEmpty(dialog.Player1Name) || String.IsNullOrEmpty(dialog.Player2Name))
            {
                return;
            }
            Hide();
            IGameProcessor gp = new HotSeatGameProcessor(10, 5, dialog.Player1Name, dialog.Player2Name);
            MainWindow mw = new MainWindow(gp);
            mw.ShowDialog();
            Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ConfirmDialog cf = new ConfirmDialog()
            {
                Text = "Выйти?"
            };
            e.Cancel = cf.ShowDialog() == false;
        }
    }
}
