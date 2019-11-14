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
            if (PinTextBox.Text.Length < 6)
                return;

            if (!TryGetDigit(PinTextBox.Text.AsSpan().Slice(0, 1), out ushort digit1) ||
                !TryGetDigit(PinTextBox.Text.AsSpan().Slice(1, 1), out ushort digit2) ||
                !TryGetDigit(PinTextBox.Text.AsSpan().Slice(2, 1), out ushort digit3) ||
                !TryGetDigit(PinTextBox.Text.AsSpan().Slice(3, 1), out ushort digit4) ||
                !TryGetDigit(PinTextBox.Text.AsSpan().Slice(4, 1), out ushort digit5) ||
                !TryGetDigit(PinTextBox.Text.AsSpan().Slice(5, 1), out ushort digit6)) {
                PinTextBox.Text = String.Empty;
                return;
            }

            Phone.Unlocked = false;
            LockScreen.UnlockPhone(digit1, digit2, digit3, digit4, digit5, digit6);

            if (Phone.Unlocked) {
                LockIcon.Kind = PackIconKind.LockOpen;
            } else {
                var shakeStoryboard = (Storyboard)LockIcon.Resources["ShakeStoryboard"];
                Storyboard.SetTarget(shakeStoryboard.Children.ElementAt(0) as DoubleAnimationUsingKeyFrames, LockIcon);
                shakeStoryboard.Begin();
            }

            PinTextBox.Text = String.Empty;

            static bool TryGetDigit(ReadOnlySpan<char> chars, out ushort digit)
            {
                if (UInt16.TryParse(chars, out digit))
                    return true;

                ushort? proposedDigit = chars[0] switch
                {
                    'a' => 10,
                    'b' => 11,
                    'c' => 12,
                    'd' => 13,
                    'e' => 14,
                    'f' => 15,
                    _ => default(ushort?)
                };

                digit = proposedDigit.GetValueOrDefault();
                return proposedDigit.HasValue;
            }
        }

        private void PinTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9a-f]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void LockIcon_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LockIcon.Kind = PackIconKind.Lock;
        }
    }
}
