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
    public partial class MessageDialog : UserControl
    {
        public static DependencyProperty TextProperty;
        public static DependencyProperty HeadingProperty;
        public static DependencyProperty DialogBackgroundProperty;

        public static RoutedEvent ConfirmEvent = EventManager.RegisterRoutedEvent("Confirm", RoutingStrategy.Direct, typeof(EventHandler), typeof(MessageDialog));

        public static RoutedEvent RejectEvent = EventManager.RegisterRoutedEvent("Reject", RoutingStrategy.Direct, typeof(EventHandler), typeof(MessageDialog));

        public event EventHandler Confirm
        {
            add => AddHandler(ConfirmEvent, value);
            remove => RemoveHandler(ConfirmEvent, value);
        }


        static MessageDialog()
        {
            TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(MessageDialog));
            HeadingProperty = DependencyProperty.Register("Heading", typeof(string), typeof(MessageDialog));
            DialogBackgroundProperty = DependencyProperty.Register("DialogBackground", typeof(Brush), typeof(MessageDialog));
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

            DependencyPropertyDescriptor dpd = DependencyPropertyDescriptor
                .FromProperty(MessageDialog.OpacityProperty, typeof(MessageDialog));
            if (dpd != null)
            {
                dpd.AddValueChanged(this, HandleOpacityChange);
            }
        }

        private void HandleOpacityChange(object sender, EventArgs e)
        {
            if (this.Opacity < 0.001)
            {
                Visibility = Visibility.Hidden;
            }
            else
            {
                Visibility = Visibility.Visible;
            }
        }

        private void YesClick(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(ConfirmEvent));
        }
    }
}
