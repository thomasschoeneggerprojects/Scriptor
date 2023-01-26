using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using TsSolutions.WpfCommon.Controls.Input;
using TsSolutions.WpfCommon.Controls.Output;

namespace TsSolutions.WpfCommon.TestViews
{
    /// <summary>
    /// Interaction logic for TestRtbEditorView.xaml
    /// </summary>
    public partial class TestRtbEditorView : UserControl
    {
        public TestRtbEditorView()
        {
            InitializeComponent();
        }

        private void buttonGetText_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var rtfContent = editor.GetContent().Content;

            txtOutput.Text = rtfContent;
            sw.Stop();

            labelInfo.Content = $"Cnt Chars:{rtfContent.Length}; Duration [ms]: {sw.Elapsed.TotalMilliseconds}";
        }

        private void buttonSetText_Click(object sender, RoutedEventArgs e)
        {
            string rtf = txtOutput.Text;
            editor.SetContent(TextEditorContent.Create(TextEditorContentType.Rtf, rtf));
        }

        private void buttonShowInViewer_Click(object sender, RoutedEventArgs e)
        {
            var rtfContent = editor.GetContent();
            RtfTextViewer viewer = new RtfTextViewer();
            viewer.SetContent(rtfContent);

            Window container = new Window();
            ContentControl contentControl = new ContentControl();
            contentControl.Content = viewer;
            container.Content = contentControl;
            container.Show();
        }
    }
}