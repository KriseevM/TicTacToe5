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

namespace TicTacToe5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IGameProcessor mainProcessor;
        public MainWindow(IGameProcessor processor)
        {
            InitializeComponent();
            mainProcessor = processor;
            mainProcessor.GameTick += MainProcessor_GameTick;
            mainProcessor.GameFinished += MainProcessor_GameFinished;
        }

        private void MainProcessor_GameFinished(object sender, GameFinishedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MainProcessor_GameTick(object sender, EventArgs e)
        {
            
        }
    }
}
