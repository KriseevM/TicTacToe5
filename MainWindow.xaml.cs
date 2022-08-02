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
using TicTacToe5.components;

namespace TicTacToe5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private IGameProcessor mainProcessor;
        private bool isGameFinished = false;
        private Border[,] fieldRectangles;
        private CellState[,] displayedCellsStates;
        public MainWindow(IGameProcessor processor)
        {
            InitializeComponent();
            mainProcessor = processor;
            mainProcessor.GameTick += MainProcessor_GameTick;
            mainProcessor.GameFinished += MainProcessor_GameFinished;
            for (int i = 0; i < mainProcessor.FieldSize; ++i)
            {
                mainField.RowDefinitions.Add(new RowDefinition()
                {
                    Height = new GridLength((double)Application.Current.Resources["cellWidth"], GridUnitType.Pixel)
                });
                mainField.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    Width = new GridLength((double)Application.Current.Resources["cellWidth"], GridUnitType.Pixel)
                });
            }

            fieldRectangles = new Border[mainProcessor.FieldSize, mainProcessor.FieldSize];
            displayedCellsStates = new CellState[mainProcessor.FieldSize, mainProcessor.FieldSize];
            for (int i = 0; i < mainProcessor.FieldSize; ++i)
            {
                for (int j = 0; j < mainProcessor.FieldSize; ++j)
                {
                    Border currentBorder;
                    fieldRectangles[i, j] = currentBorder = new Border()
                    {
                        Margin = new Thickness(0, 0, 0, 0),
                    };
                    currentBorder.MouseLeftButtonUp += GridRectangle_Click;
                    currentBorder.BorderBrush = new SolidColorBrush(Colors.Black);
                    currentBorder.BorderThickness = new Thickness(2,2,2,2);
                    currentBorder.Background = new SolidColorBrush(Colors.Transparent);
                    Rectangle rect = new Rectangle() {
                        Margin = new Thickness(0, 0, 0, 0)
                    };
                    currentBorder.Child = rect;
                    mainField.Children.Add(fieldRectangles[i, j]);
                    Grid.SetRow(fieldRectangles[i, j], j);
                    Grid.SetColumn(fieldRectangles[i, j], i);
                    var currentCellState = mainProcessor.GameField[i, j];
                    displayedCellsStates[i, j] = currentCellState;
                    UpdateRectangleState((Rectangle)currentBorder.Child, currentCellState, false);
                }
            }
        }

        private void GridRectangle_Click(object sender, RoutedEventArgs e)
        {
            Border btn = (Border)sender;
            int x = Grid.GetColumn(btn);
            int y = Grid.GetRow(btn);
            mainProcessor.SetCell(x, y);
        }

        private static void UpdateRectangleState(Rectangle currentRectangle, CellState currentCellState, bool doAnimation = true)
        {
            if (!doAnimation)
            {
                RedrawRectangle(currentRectangle, currentCellState);
                return;
            }
            else
            {
                DoubleAnimation fadeOut = new DoubleAnimation()
                {
                    Duration = new Duration(TimeSpan.FromMilliseconds(50)),
                    To = 0.0
                };
                ColorAnimation backColorAnim = new ColorAnimation()
                {
                    Duration = new Duration(TimeSpan.FromMilliseconds(100)),
                    EasingFunction = new ElasticEase()
                    {
                        EasingMode = EasingMode.EaseOut
                    },
                    To = currentCellState == CellState.Empty ? Color.FromRgb(255, 0, 0) :
                        currentCellState == CellState.Cross ? Color.FromRgb(0, 38, 255) : Color.FromRgb(76, 255, 0),
                    AutoReverse = true
                };
                ((SolidColorBrush)((Border)currentRectangle.Parent).Background).BeginAnimation(SolidColorBrush.ColorProperty, backColorAnim);
                fadeOut.Completed += (object sender, EventArgs e) =>
                {
                    RedrawRectangle(currentRectangle, currentCellState);
                    DoubleAnimation fadeIn = new DoubleAnimation()
                    {
                        Duration = new Duration(TimeSpan.FromMilliseconds(100)),
                        To = 1.0
                    };
                    currentRectangle.BeginAnimation(Rectangle.OpacityProperty, fadeIn);
                };
                currentRectangle.BeginAnimation(Rectangle.OpacityProperty, fadeOut, HandoffBehavior.Compose);
            }
        }

        private static void RedrawRectangle(Rectangle currentRectangle, CellState currentCellState)
        {
            if (currentCellState == CellState.Empty)
            {
                ((Border)currentRectangle.Parent).IsEnabled = true;
                currentRectangle.Fill = new SolidColorBrush(Colors.Transparent);
            }
            else
            {
                ((Border)currentRectangle.Parent).IsEnabled = false;
                currentRectangle.SetResourceReference(Rectangle.FillProperty, currentCellState == CellState.Cross ? "CrossIconBrush" : "NoughtIconBrush");
            }
        }

        private void MainProcessor_GameFinished(object sender, GameFinishedEventArgs e)
        {
            string winText;
            if (e.WinnerSide == CellState.Empty)
            {
                winText = "Ничья!";
            }
            else
            {
                winText = "Выиграл " + e.WinnerName;
            }
            MessageDialog finishDialog = new MessageDialog()
            {
                Heading = "Игра окончена",
                Text = winText
            };
            //finishDialog.Text = winText;
            //this.BeginStoryboard((Storyboard)Resources["FinishDialogShowUp"]);
            //MessageBox.Show(winText, "Игра окончена", MessageBoxButton.OK);
            finishDialog.ShowDialog();
            isGameFinished = true;
            Close();
        }

        private void MainProcessor_GameTick(object sender, EventArgs e)
        {
            var proc = (IGameProcessor)sender;
            Dispatcher.Invoke(() => {
                timeLabel.Content = Math.Ceiling(proc.Time) + "с";
                currentTurnLabel.Content = proc.CurrentPlayer;
            });
            for (int i = 0; i < proc.FieldSize; ++i)
            {
                for (int j = 0; j < proc.FieldSize; ++j)
                {

                    Dispatcher.Invoke(() => 
                    {
                        if(proc.GetCellState(i, j) != displayedCellsStates[i, j])
                        {
                            displayedCellsStates[i, j] = proc.GetCellState(i, j);
                            UpdateRectangleState((Rectangle)fieldRectangles[i, j].Child, displayedCellsStates[i, j]);
                        }                        
                    });
                }
            }
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            Width = (double)Application.Current.Resources["cellWidth"] * mainProcessor.FieldSize + 30;
            Height = (double)Application.Current.Resources["cellHeight"] * mainProcessor.FieldSize + 160;
            mainProcessor.SetReady();
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isGameFinished)
            {
                return;
            }
            ConfirmDialog cf = new ConfirmDialog()
            {
                Text = "Выйти?"
            };
            e.Cancel = cf.ShowDialog() == false;
            if (!e.Cancel)
            {
                mainProcessor.Interrupt();
                mainProcessor.GameTick -= MainProcessor_GameTick;
            }
        }

        private void MessageDialog_Confirm(object sender, EventArgs e)
        {
            Close();
        }
    }
}
