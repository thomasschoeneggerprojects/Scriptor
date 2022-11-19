using ScriptExecutorLib.Model.Execution;
using ScriptExecutorLib.UserControls;
using ScriptExecutorLib.ViewModel;
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

namespace ScriptExecutor.View
{
    /// <summary>
    /// Interaction logic for <see cref="ScriptEditor"/>.xaml
    /// </summary>
    public partial class ScriptEditor : UserControl
    {
        private ScriptEditorViewModel VM;

        public ScriptEditor()
        {
            InitializeComponent();

            VM = new ScriptEditorViewModel();
            this.DataContext = VM;

            FillTestItems(10);
        }

        internal void FillTestItems(int countItems)
        {
            List<UserControl> items = new List<UserControl>();

            for (int i = 0; i < countItems; i++)
            {
                var item = ExecutionItemFactory.CreateExecutionItemDefault($"test Item Name {i}");
                var cntrl = new ScriptControl(item);

                items.Add(cntrl);
            }

            VM.SetItems(items);
        }
    }
}