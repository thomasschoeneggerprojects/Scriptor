using ScriptExecutorPrime.View;
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

namespace ScriptExecutorPrime
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowModel VM;

        public MainWindow()
        {
            InitializeComponent();
            VM = new MainWindowModel();
            this.DataContext = VM;

            VM.SetDefaultContent();
        }

        private void buttonCreateTestScripts_Click(object sender, RoutedEventArgs e)
        {
            VM.SetCreateTestScriptsContent();
        }
    }
}