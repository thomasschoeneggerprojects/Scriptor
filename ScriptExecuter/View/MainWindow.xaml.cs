using ScriptExecutor.View;
using ScriptExecutor.ViewModel;
using ScriptExecutorLib.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using TsSolutions.Service.PerformanceTest;
using TsSolutions.Service;

namespace ScriptExecuter.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel VM;

        public MainWindow()
        {
            InitializeComponent();
            VM = new MainWindowViewModel();
            this.DataContext = VM;

            VM.OpenScriptExecution();
        }

        private void menueBtnExecute_Click(object sender, RoutedEventArgs e)
        {
            VM.OpenScriptExecution();
        }

        private void menueBtnEdit_Click(object sender, RoutedEventArgs e)
        {
            VM.OpenScriptEditor();
        }

        private void menueBtnCreateTestScripts_Click(object sender, RoutedEventArgs e)
        {
        }

        private void gridTitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}