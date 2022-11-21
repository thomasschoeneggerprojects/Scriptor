using ScriptExecutorLib.Model.Execution;
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

namespace ScriptExecutorLib.UserControls
{
    /// <summary>
    /// Interaction logic for ScriptOverView.xaml
    /// </summary>
    public partial class ScriptOverView : UserControl
    {
        private ScriptOverViewModel VM;

        public ScriptOverView(ExecutionItem executionItem)
        {
            InitializeComponent();
            VM = new ScriptOverViewModel(ServiceProvider.Get<IExecutionItemProcessor>());
            this.DataContext = VM;

            VM.SetItem(executionItem);
        }

        private void buttonExecuteScript_Click(object sender, RoutedEventArgs e)
        {
            VM.ExecuteScript();
        }
    }
}