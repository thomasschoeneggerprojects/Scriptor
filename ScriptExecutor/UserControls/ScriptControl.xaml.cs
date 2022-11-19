using ScriptExecutorLib.Model.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
    /// Interaction logic for ScriptControl.xaml
    /// </summary>
    public partial class ScriptControl : UserControl
    {
        private ExecutionItem Item { get; }

        public ScriptControl(ExecutionItem executionItem)
        {
            Item = executionItem;
            InitializeComponent();
            lblName.Content = executionItem.Name;
            lblDescription.Content = executionItem.Description;
        }
    }
}