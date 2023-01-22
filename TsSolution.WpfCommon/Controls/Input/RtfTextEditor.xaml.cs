﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            InitControl();
        }

        //private bool _isInitalized = false;
        // public bool IsControlInitialized => _isInitalized;

        private void InitControl()
        {
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
            RtbEditor.Focus();
        }

        private void btnUnderline_Checked(object sender, RoutedEventArgs e)
        {
            object fontStyle = RtbEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            btnUnderline.IsChecked = (fontStyle != DependencyProperty.UnsetValue) && (fontStyle.Equals(TextDecorations.Underline));

            if (btnUnderline.IsChecked.Value)
            {
                RtbEditor.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Baseline);
            }
            else
            {
                RtbEditor.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }
            RtbEditor.Focus();
        }

        private void btnItalic_Checked(object sender, RoutedEventArgs e)
        {
            object textDeco = RtbEditor.Selection.GetPropertyValue(Inline.FontStyleProperty);
            btnItalic.IsChecked = (textDeco != DependencyProperty.UnsetValue) && (textDeco.Equals(FontStyles.Italic));

            if (btnItalic.IsChecked.Value)
            {
                RtbEditor.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Normal);
            }
            else
            {
                RtbEditor.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
            }
            RtbEditor.Focus();
        }

        private void RtbEditor_GotFocus(object sender, RoutedEventArgs e)
        {
            CollapseAllContentsVisibilities();
        }

        #region Insert table

        private void ButtonOpenInsertTable_Click(object sender, RoutedEventArgs e)
        {
            if (contentInsertTable.Visibility == Visibility.Collapsed)
            {
                contentInsertTable.Visibility = Visibility.Visible;
            }
            else
            {
                CollapseAllContentsVisibilities();
            }
        }

        private void ButtonInsertTable_Click(object sender, RoutedEventArgs e)
        {
            var countRows = int.Parse(countRowsInsertTable.Text);
            var countColumns = int.Parse(countColumnsInsertTable.Text);

            var table = CreateTable(countRows, countColumns);

            RtbEditor.Document.Blocks.Add(table);

            RtbEditor.AppendText(" ");

            contentInsertTable.Visibility = Visibility.Collapsed;
        }

        private Table CreateTable(int rows, int columns)
        {
            string id = Guid.NewGuid().ToString();
            var tab = new Table() { Name = $"Table" };
            var gridLenghtConvertor = new GridLengthConverter();

            for (int colNo = 0; colNo < columns; colNo++)
            {
                tab.Columns.Add(new TableColumn() { Name = $"Column{colNo}", Width = (GridLength)gridLenghtConvertor.ConvertFromString("*") });
            }

            tab.RowGroups.Add(new TableRowGroup());

            for (int rowNo = 0; rowNo < rows; rowNo++)
            {
                tab.RowGroups[0].Rows.Add(new TableRow());
                var tabRow = tab.RowGroups[0].Rows[rowNo];

                for (int colNo = 0; colNo < columns; colNo++)
                {
                    tabRow.Cells.Add(new TableCell(new Paragraph(new Run(" ")))
                    { TextAlignment = TextAlignment.Center, BorderBrush = Brushes.Black, BorderThickness = new Thickness(1) });
                }
            }
            tab.CellSpacing = 0;

            return tab;
        }

        #endregion Insert table

        private void ButtonSetColor_Click(object sender, RoutedEventArgs e)
        {
            var colorAsText = SelectedColor.Text;
            var color = new BrushConverter().ConvertFromString(colorAsText) as SolidColorBrush;
            //consider Picture selection
            object textForeground = RtbEditor.Selection.GetPropertyValue(Inline.ForegroundProperty);
            var isred = (textForeground != DependencyProperty.UnsetValue) && (textForeground.Equals(Brushes.Red));
            RtbEditor.Selection.ApplyPropertyValue(Inline.ForegroundProperty, color);

            RtbEditor.Focus();
        }

        private void ButtonOpenSetColorMenue_Click(object sender, RoutedEventArgs e)
        {
            if (contentSetColor.Visibility == Visibility.Collapsed)
            {
                contentSetColor.Visibility = Visibility.Visible;
            }
            else
            {
                CollapseAllContentsVisibilities();
            }
        }

        private void CollapseAllContentsVisibilities()
        {
            contentSetColor.Visibility = Visibility.Collapsed;
            contentInsertTable.Visibility = Visibility.Collapsed;
        }

        private void SelectedColor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsInitialized)
                return;

            var textBox = sender as TextBox;

            if (textBox != null)
            {
                var inputText = textBox.Text;
                Regex myRegex = new Regex("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$");
                bool isValid = false;
                if (string.IsNullOrEmpty(inputText))
                {
                    isValid = false;
                }
                else
                {
                    isValid = myRegex.IsMatch(inputText);
                }

                if (isValid)
                {
                    var converter = new BrushConverter();

                    var color = converter.ConvertFromString(inputText) as SolidColorBrush;

                    ColorPreview.Fill = color;
                }
            }
        }
    }
}