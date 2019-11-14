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
            if (PinTextBox.Text.Length < 8)
                return;

            if (!TryGetDigit(PinTextBox.Text.AsSpan().Slice(0, 1), out char digit1) ||
                !TryGetDigit(PinTextBox.Text.AsSpan().Slice(1, 1), out char digit2) ||
                !TryGetDigit(PinTextBox.Text.AsSpan().Slice(2, 1), out char digit3) ||
                !TryGetDigit(PinTextBox.Text.AsSpan().Slice(3, 1), out char digit4) ||
                !TryGetDigit(PinTextBox.Text.AsSpan().Slice(4, 1), out char digit5) ||
                !TryGetDigit(PinTextBox.Text.AsSpan().Slice(5, 1), out char digit6) ||
                !TryGetDigit(PinTextBox.Text.AsSpan().Slice(6, 1), out char digit7) ||
                !TryGetDigit(PinTextBox.Text.AsSpan().Slice(7, 1), out char digit8)) {
                PinTextBox.Text = String.Empty;
                return;
            }

            Phone.Unlocked = false;
            LockScreen.UnlockPhone(new[] { digit1, digit2, digit3, digit4, digit5, digit6, digit7, digit8 });

            if (Phone.Unlocked) {
                LockIcon.Kind = PackIconKind.LockOpen;
            } else {
                var shakeStoryboard = (Storyboard)LockIcon.Resources["ShakeStoryboard"];
                Storyboard.SetTarget(shakeStoryboard.Children.ElementAt(0) as DoubleAnimationUsingKeyFrames, LockIcon);
                shakeStoryboard.Begin();
            }

            PinTextBox.Text = String.Empty;

            static bool TryGetDigit(ReadOnlySpan<char> chars, out char digit)
            {
                digit = chars[0];
                return true;
            }
        }

        private void PinTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void LockIcon_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LockIcon.Kind = PackIconKind.Lock;
        }
    }
}
