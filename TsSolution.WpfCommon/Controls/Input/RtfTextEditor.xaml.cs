using System;
using System.Collections.Generic;
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

namespace TsSolutions.WpfCommon.Controls.Input
{
    /// <summary>
    /// Interaction logic for TextEditor.xaml
    /// </summary>
    public partial class RtfTextEditor : UserControl
    {
        public RtfTextEditor()
        {
            InitializeComponent();
            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            cmbFontSize.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
        }

        public void SetRtbText(string text, TextPointer start, TextPointer end)
        {
            MemoryStream stream = new MemoryStream(ASCIIEncoding.Default.GetBytes(text));
            TextRange range = new TextRange(RtbEditor.Document.ContentStart, RtbEditor.Document.ContentEnd);
            range.Load(stream, DataFormats.Rtf);
        }

        public void SetContent(TextEditorContent text)
        {
            if (text.Type.Equals(TextEditorContentType.Rtf))
                SetRtbText(text.Content, RtbEditor.Document.ContentStart, RtbEditor.Document.ContentEnd);
        }

        public TextEditorContent GetContent()
        {
            string rtfFromRtb = string.Empty;
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    TextRange range2 = new TextRange(RtbEditor.Document.ContentStart, RtbEditor.Document.ContentEnd);
                    range2.Save(ms, DataFormats.Rtf);
                    ms.Seek(0, SeekOrigin.Begin);
                    using (StreamReader sr = new StreamReader(ms))
                    {
                        rtfFromRtb = sr.ReadToEnd();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return TextEditorContent.Create(TextEditorContentType.Rtf, rtfFromRtb);
        }

        private void cmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!updateSelectionsByRtbEditor)
            {
                if (cmbFontFamily.SelectedItem != null)
                    RtbEditor.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cmbFontFamily.SelectedItem);
            }
        }

        private void cmbFontSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            RtbEditor.Selection.ApplyPropertyValue(Inline.FontSizeProperty, cmbFontSize.Text);
        }

        private bool updateSelectionsByRtbEditor = false;

        private void RtbEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            updateSelectionsByRtbEditor = true;
            object temp = RtbEditor.Selection.GetPropertyValue(Inline.FontWeightProperty);
            btnBold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));
            temp = RtbEditor.Selection.GetPropertyValue(Inline.FontStyleProperty);
            btnItalic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));
            temp = RtbEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            btnUnderline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));

            temp = RtbEditor.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            cmbFontFamily.SelectedItem = temp;
            temp = RtbEditor.Selection.GetPropertyValue(Inline.FontSizeProperty);
            if (temp != DependencyProperty.UnsetValue)
            {
                cmbFontSize.Text = temp.ToString();
            }
            updateSelectionsByRtbEditor = false;
        }

        internal void InsertAtSelection(string textToInsert)
        {
            if (RtbEditor == null)
            {
                return;
            }

            var caretPosition = RtbEditor.CaretPosition.GetPositionAtOffset(0,
                                  LogicalDirection.Forward);

            RtbEditor.CaretPosition.InsertTextInRun(textToInsert);
            RtbEditor.CaretPosition = caretPosition;
        }

        private void RtbEditor_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {
                if (e.Key == Key.V)
                {
                    if (Clipboard.ContainsText(TextDataFormat.Html)
                        || Clipboard.ContainsText(TextDataFormat.Rtf))
                    {
                        string plainText = Clipboard.GetText();
                        InsertAtSelection(plainText);
                        e.Handled = true;
                        return;
                    }
                }
            }

            if (e.Key == Key.Tab)
            {
                InsertAtSelection("\t");

                e.Handled = true;
                return;
            }
        }

        private void TestButtonInsertTable_Click(object sender, RoutedEventArgs e)
        {
            var table = CreateTable();

            RtbEditor.Document.Blocks.Add(table);

            RtbEditor.AppendText(" ");
        }

        private Table CreateTable()
        {
            var tab = new Table();
            var gridLenghtConvertor = new GridLengthConverter();

            tab.Columns.Add(new TableColumn() { Name = "Column1", Width = (GridLength)gridLenghtConvertor.ConvertFromString("*") });
            tab.Columns.Add(new TableColumn() { Name = "Column2", Width = (GridLength)gridLenghtConvertor.ConvertFromString("*") });

            tab.RowGroups.Add(new TableRowGroup());

            for (int i = 0; i < 10; i++)
            {
                tab.RowGroups[0].Rows.Add(new TableRow());
                var tabRow = tab.RowGroups[0].Rows[i];

                tabRow.Cells.Add(new TableCell(new Paragraph(new Run("Row" + (i + 1).ToString() + " Column1"))) { TextAlignment = TextAlignment.Center });
                tabRow.Cells.Add(new TableCell(new Paragraph(new Run("Row" + (i + 1).ToString() + " Column2"))));
            }
            tab.BorderBrush = Brushes.Black;
            tab.BorderThickness = new Thickness(1);

            return tab;
        }

        private static String CreateTable(int rows, int cols, int width)
        {
            //Create StringBuilder Instance
            StringBuilder sringTableRtf = new StringBuilder();

            //beginning of rich text format
            sringTableRtf.Append(@"{\rtf1 ");

            //Variable for cell width
            int cellWidth;

            //Start row
            sringTableRtf.Append(@"\trowd");

            //Loop to create table string
            for (int i = 0; i < rows; i++)
            {
                sringTableRtf.Append(@"\trowd");

                for (int j = 0; j < cols; j++)
                {
                    //Calculate cell end point for each cell
                    cellWidth = (j + 1) * width;

                    //A cell with width 1000 in each iteration.
                    sringTableRtf.Append(@"\cellx" + cellWidth.ToString());
                }

                //Append the row in StringBuilder
                sringTableRtf.Append(@"\intbl \cell \row");
            }
            sringTableRtf.Append(@"\pard");
            sringTableRtf.Append(@"}");

            return sringTableRtf.ToString();
        }

        private void btnBold_Click(object sender, RoutedEventArgs e)
        {
            object fontweight = RtbEditor.Selection.GetPropertyValue(Inline.FontWeightProperty);
            btnBold.IsChecked = (fontweight != DependencyProperty.UnsetValue) && (fontweight.Equals(FontWeights.Bold));

            if (btnBold.IsChecked.Value)
            {
                RtbEditor.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);
            }
            else
            {
                RtbEditor.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
            }
        }
    }
}