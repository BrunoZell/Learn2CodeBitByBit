using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
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

        private void PinTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return)
                return;

            Phone.Unlocked = false;
            LockScreen.UnlockPhone(PinTextBox.Text);

            if (Phone.Unlocked) {
                LockIcon.Kind = PackIconKind.LockOpen;
            } else {
                var shakeStoryboard = (Storyboard)LockIcon.Resources["ShakeStoryboard"];
                Storyboard.SetTarget(shakeStoryboard.Children.ElementAt(0) as DoubleAnimationUsingKeyFrames, LockIcon);
                shakeStoryboard.Begin();
            }

            PinTextBox.Text = String.Empty;
        }

        private void SetPinTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return)
                return;

            LockScreen.SetPassword(PinTextBox.Text, SetPinTextBox.Text);
            PinTextBox.Text = String.Empty;
            SetPinTextBox.Text = String.Empty;
        }

        private void PinTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void LockIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LockIcon.Kind = PackIconKind.Lock;
        }
    }
}
