using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace TicTacToe5.components
{
    /// <summary>
    /// Логика взаимодействия для ConfirmDialog.xaml
    /// </summary>
    public partial class ConfirmDialog : Window
    {
        public static DependencyProperty TextProperty;
        public static DependencyProperty DialogBackgroundProperty;

        static ConfirmDialog()
        {
            TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(ConfirmDialog));
            DialogBackgroundProperty = DependencyProperty.Register("DialogBackground", typeof(Brush), typeof(ConfirmDialog));
        }
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        public Brush DialogBackground
        {
            get => (Brush)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        public ConfirmDialog()
        {
            InitializeComponent();
        }

        private void NoClick(object sender, RoutedEventArgs e)
        {
            
            Close();
        }

        private void YesClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Close();
        }
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }

        private void root_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
