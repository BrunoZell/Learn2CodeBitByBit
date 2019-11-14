using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;

namespace YourPhone
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PinTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PinTextBox.Text.Length < 4)
                return;

            if (!UInt16.TryParse(PinTextBox.Text, out ushort pin)) {
                PinTextBox.Text = String.Empty;
                return;
            }

            Phone.Unlocked = false;
            LockScreen.UnlockPhone(pin);

            if (Phone.Unlocked) {
                LockIcon.Kind = PackIconKind.LockOpen;
            } else {
                // Todo: Show lock animation
            }

            PinTextBox.Text = String.Empty;
        }

        private void PinTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void LockIcon_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LockIcon.Kind = PackIconKind.Lock;
        }
    }
}
