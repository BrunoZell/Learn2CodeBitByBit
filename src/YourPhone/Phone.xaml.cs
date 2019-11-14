using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
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
                var shakeStoryboard = (Storyboard)LockIcon.Resources["ShakeStoryboard"];
                Storyboard.SetTarget(shakeStoryboard.Children.ElementAt(0) as DoubleAnimationUsingKeyFrames, LockIcon);
                shakeStoryboard.Begin();
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
