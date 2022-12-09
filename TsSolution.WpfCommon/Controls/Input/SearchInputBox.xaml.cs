using System;
using System.Collections.Generic;
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
    /// Interaction logic for SearchInputBox.xaml
    /// </summary>
    public partial class SearchInputBox : UserControl
    {
        public SearchInputBox()
        {
            InitializeComponent();
            textBoxSearch.InputChanged += (o, e) => OnSearchEvent(e);
        }

        private void OnSearchEvent(string text)
        {
        }
    }
}