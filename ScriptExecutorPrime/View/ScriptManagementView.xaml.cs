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
        private ScriptManagementViewModel VM;

        public ScriptManagementView()
        {
            InitializeComponent();

            IExecutionItemManager manager = new DefaultExecutionItemManager();

            VM = new ScriptManagementViewModel(manager);
            this.DataContext = VM;
        }

        private void buttonCreateTestScripts_Click(object sender, RoutedEventArgs e)
        {
            VM.CreateTestScripts();
        }
    }
}