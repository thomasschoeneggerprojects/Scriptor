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

namespace TsSolutions.WpfCommon.Controls.Output
{
    /// <summary>
    /// Interaction logic for RtfTextViewer.xaml
    /// </summary>
    public partial class RtfTextViewer : UserControl
    {
        public RtfTextViewer()
        {
            InitializeComponent();
        }

        public void SetRtbText(string text, TextPointer start, TextPointer end)
        {
            using (MemoryStream stream = new MemoryStream(ASCIIEncoding.Default.GetBytes(text)))
            {
                TextRange range = new TextRange(start, end);
                range.Load(stream, DataFormats.Rtf);
            }
        }

        public void SetContent(TextEditorContent text)
        {
            if (text.Type.Equals(TextEditorContentType.Rtf))
                SetRtbText(text.Content, RtbView.Document.ContentStart, RtbView.Document.ContentEnd);
        }

        private void RtbView_HyperlinkClick(object sender, RoutedEventArgs e)
        {
            if (e.Source is Hyperlink hyperLink)
            {
                var linkText = hyperLink.NavigateUri.ToString();
                Process.Start("explorer", linkText);
            }
        }
    }
}