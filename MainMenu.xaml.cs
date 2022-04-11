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
            this.BeginStoryboard((Storyboard)Resources["ExitDialogShowUp"]);
        }

        private void exitDialog_Confirm(object sender, EventArgs e)
        {
            Close();
        }

        private void exitDialog_Reject(object sender, EventArgs e)
        {
            this.BeginStoryboard((Storyboard)Resources["ExitDialogHide"]);
        }

        private void button1Player_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button2Players_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            HotSeatGameProcessor gp = new HotSeatGameProcessor(9);
            MainWindow mw = new MainWindow(gp);
            mw.ShowDialog();
            Show();
        }
    }
}
