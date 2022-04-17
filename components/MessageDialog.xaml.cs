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
    /// Логика взаимодействия для MessageDialog.xaml
    /// </summary>
    public partial class MessageDialog : Window
    {
        public static DependencyProperty TextProperty;
        public static DependencyProperty HeadingProperty;


        static MessageDialog()
        {
            TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(MessageDialog));
            HeadingProperty = DependencyProperty.Register("Heading", typeof(string), typeof(MessageDialog));
        }
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        public string Heading
        {
            get => (string)GetValue(HeadingProperty);
            set => SetValue(HeadingProperty, value);
        }
        public Brush DialogBackground
        {
            get => (Brush)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        public MessageDialog()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void root_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
