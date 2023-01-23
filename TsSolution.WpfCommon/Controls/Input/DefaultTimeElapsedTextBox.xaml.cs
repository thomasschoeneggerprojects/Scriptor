using System.Threading;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Threading;

using TsSolution.WpfCommon.Resources.Style;

namespace TsSolutions.WpfCommon.Controls.Input
{
    /// <summary>
    /// Interaction logic for DefaultTimeElapsedTextBox.xaml
    /// </summary>
    public partial class DefaultTimeElapsedTextBox : TextBox
    {
        private System.Timers.Timer countDown = new System.Timers.Timer(1700);

        public event System.EventHandler<string> InputChanged;

        public DefaultTimeElapsedTextBox()
        {
            Foreground = WpfLibColor.ColorText;
            VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            TextChanged += TextBox_TextDidChange;
            countDown.Elapsed += CountDown_Elapsed;
        }

        private void CountDown_Elapsed(object sender, ElapsedEventArgs e)
        {
            countDown.Stop();

            Dispatcher.BeginInvoke(
                new ThreadStart(() =>
                {
                    if (InputChanged != null)
                    {
                        InputChanged(this, Text);
                    }
                }));
        }

        private void TextBox_TextDidChange(object sender, TextChangedEventArgs e)
        {
            countDown.Stop();
            countDown.Start();
        }
    }
}