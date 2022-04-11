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
    public partial class ConfirmDialog : UserControl
    {
        public static DependencyProperty TextProperty;
        public static DependencyProperty DialogBackgroundProperty;

        public static RoutedEvent ConfirmEvent = EventManager.RegisterRoutedEvent("Confirm", RoutingStrategy.Direct, typeof(EventHandler), typeof(ConfirmDialog));

        public static RoutedEvent RejectEvent = EventManager.RegisterRoutedEvent("Reject", RoutingStrategy.Direct, typeof(EventHandler), typeof(ConfirmDialog));

        public event EventHandler Confirm
        {
            add => AddHandler(ConfirmEvent, value);
            remove => RemoveHandler(ConfirmEvent, value);
        }
        public event EventHandler Reject
        {
            add => AddHandler(RejectEvent, value);
            remove => RemoveHandler(RejectEvent, value);
        }


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

            DependencyPropertyDescriptor dpd = DependencyPropertyDescriptor
                .FromProperty(ConfirmDialog.OpacityProperty, typeof(ConfirmDialog));
            if (dpd != null)
            {
                dpd.AddValueChanged(this, HandleOpacityChange);
            }
        }

        private void HandleOpacityChange(object sender, EventArgs e)
        {
            if(this.Opacity < 0.001)
            {
                Visibility = Visibility.Hidden;
            }
            else
            {
                Visibility = Visibility.Visible;
            }
        }

        private void NoClick(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(RejectEvent));
        }

        private void YesClick(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(ConfirmEvent));
        }
    }
}
