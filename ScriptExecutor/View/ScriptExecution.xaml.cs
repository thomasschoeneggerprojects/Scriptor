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
using System.IO;
using Path = System.IO.Path;
using ScriptExecutorLib.ViewModel;
using ScriptExecutorLib;
using ScriptExecutorLib.Model.Execution;

namespace ScriptExecutorLib.View
{
    /// <summary>
    /// Interaction logic for ScriptExecutor.xaml
    /// </summary>
    public partial class ScriptExecution : UserControl
    {
        private Process _CurrentRunningProcess;

        private ScriptExecutionViewModel VM;

        public ScriptExecution()
        {
            InitializeComponent();
            VM = new ScriptExecutionViewModel(ServiceProvider.Get<IExecutionItemManager>(),
                ServiceProvider.Get<IExecutionItemProcessor>());
            this.DataContext = VM;
        }

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void LogErrorInformation(string message)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
            }));
        }

        private void ClearErrorInformation()
        {
            LogErrorInformation(string.Empty);
        }

        #region Button Actions

        private void buttonAddExecutionTemplate_Click(object sender, RoutedEventArgs e)
        {
            VM.AddNewExecutionItem();
        }

        #endregion Button Actions

        private void buttonDeleteExecutionTemplate_Click(object sender, RoutedEventArgs e)
        {
            _ = VM.DeleteSelectedExecutionItem();
        }

        private void searchBoxItems_InputChanged(object sender, string text)
        {
            VM.SearchTextChanged(text);
        }
    }
}