using ScriptExecutorLib;
using ScriptExecutorLib.Model.Execution;
using ScriptExecutorPrime.ViewModel;
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

namespace ScriptExecutorPrime.View
{
    /// <summary>
    /// Interaction logic for ScriptManagementView.xaml
    /// </summary>
    public partial class ScriptManagementView : UserControl
    {
        private readonly IExecutionItemManager _executionItemManager;
        private ScriptManagementViewModel VM;

        public ScriptManagementView()
        {
            InitializeComponent();

            this._executionItemManager = ServiceProvider.Get<IExecutionItemManager>();
            VM = new ScriptManagementViewModel(_executionItemManager);
            this.DataContext = VM;
        }

        private void buttonCreateTestScripts_Click(object sender, RoutedEventArgs e)
        {
            VM.CreateTestScripts();
        }

        private void buttonDeleteAllTestScripts_Click(object sender, RoutedEventArgs e)
        {
            VM.DeleteAllTestScripts();
        }
    }
}