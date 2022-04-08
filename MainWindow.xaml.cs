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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private IGameProcessor mainProcessor;
        private bool isGameFinished = false;
        private Button[,] fieldButtons;

        public MainWindow(IGameProcessor processor)
        {
            InitializeComponent();
            mainProcessor = processor;
            mainProcessor.GameTick += MainProcessor_GameTick;
            mainProcessor.GameFinished += MainProcessor_GameFinished;
            for (int i = 0; i < mainProcessor.GameField.Size; ++i)
            {
                mainField.RowDefinitions.Add(new RowDefinition() {
                    Height = new GridLength((double) Application.Current.Resources["cellWidth"], GridUnitType.Pixel)
                });
                mainField.ColumnDefinitions.Add(new ColumnDefinition() {
                    Width = new GridLength((double)Application.Current.Resources["cellWidth"], GridUnitType.Pixel)
                });
            }
            
            fieldButtons = new Button[mainProcessor.GameField.Size, mainProcessor.GameField.Size];
            for (int i = 0; i < mainProcessor.GameField.Size; ++i)
            {
                for (int j = 0; j < mainProcessor.GameField.Size; ++j)
                {
                    Button currentButton;
                    fieldButtons[i, j] = currentButton = new Button()
                    {
                        Margin = new Thickness(0,0,0,0),
                    };
                    currentButton.Click += GridButton_Click;
                    mainField.Children.Add(fieldButtons[i, j]);
                    Grid.SetRow(fieldButtons[i, j], j);
                    Grid.SetColumn(fieldButtons[i, j], i);
                    var currentCellState = mainProcessor.GameField[i, j];
                    UpdateButtonState(currentButton, currentCellState);
                }
            }
        }

        private void GridButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int x = Grid.GetColumn(btn);
            int y = Grid.GetRow(btn);
            mainProcessor.SetCell(x, y);
        }

        private static void UpdateButtonState(Button currentButton, CellState currentCellState)
        {
            switch (currentCellState)
            {
                case CellState.Cross:
                    currentButton.IsEnabled = false;
                    currentButton.Content = new Image() { Source = (BitmapImage)Application.Current.Resources["CrossIcon"] };
                    break;
                case CellState.Nought:
                    currentButton.IsEnabled = false;
                    currentButton.Content= new Image() { Source = (BitmapImage)Application.Current.Resources["NoughtIcon"] };
                    break;
                case CellState.Empty:
                    currentButton.Content = "";
                    break;
            }
        }

        private void MainProcessor_GameFinished(object sender, GameFinishedEventArgs e)
        {
            if (e.WinnerSide == CellState.Empty)
            {
                MessageBox.Show("Ничья!", "Игра окончена");
            }
            else
            {
                MessageBox.Show("Выиграли " + e.WinnerName, "Игра окончена");
            }
            isGameFinished = true;
            Close();
        }

        private void MainProcessor_GameTick(object sender, EventArgs e)
        {
            var proc = (IGameProcessor)sender;
            for (int i = 0; i < proc.GameField.Size; ++i)
            {
                for (int j = 0; j < proc.GameField.Size; ++j)
                {
                    Dispatcher.Invoke(() => UpdateButtonState(fieldButtons[i, j], proc.GameField[i, j]));
                }
            }
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            Width = (double)Application.Current.Resources["cellWidth"] * mainProcessor.GameField.Size + 30;
            Height= (double)Application.Current.Resources["cellHeight"] * mainProcessor.GameField.Size + 160;
            mainProcessor.SetReady();
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(isGameFinished)
            {
                return;
            }
            e.Cancel = MessageBox.Show("Вы действительно хотите выйти?", "Выйти?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No;
            if (!e.Cancel)
            {
                mainProcessor.Interrupt();
                mainProcessor.GameTick -= MainProcessor_GameTick;
            }
        }
    }
}
