using ScriptExecutorLib.Model.Execution;
using System.Windows;
using System.Windows.Controls;
using TsSolutions.Serialization.PropertySet;

namespace ScriptExecutorLib.View
{
    /// <summary>
    /// Interaction logic for ScriptEditUserControl.xaml
    /// </summary>
    public partial class ScriptEditUserControl : UserControl
    {
        private IExecutionItemManager _executionItemManger;
        private ExecutionItem _executionItem;
        public ScriptEditUserControl(ExecutionItemId itemId)
        {
            InitializeComponent();
            _executionItemManger = ServiceProvider.Get<IExecutionItemManager>();
            Init(itemId);
        }

        private async void Init(ExecutionItemId itemId)
        {
            _executionItem = await _executionItemManger.GetById(itemId);
            TextBoxNameExecutionItem.Text = _executionItem.Name;
            TextBoxScript.Text = _executionItem.Content;
            TextBoxDescriptionExecutionItem.Text = _executionItem.Description;
        }

        private async void ButtonSaveScript_Click(object sender, RoutedEventArgs e)
        {
            ExecutionItem itemToSave = new()
            {
                Id = _executionItem.Id,
                Name = TextBoxNameExecutionItem.Text,
                Content = TextBoxScript.Text,
                Description = TextBoxDescriptionExecutionItem.Text,
                Arguments = new(),
                ItemType = ExecutionItemType.Powershell,
                Properties = new DefaultPropertySet(),
            };
            await _executionItemManger.Update(itemToSave);
        }

        private void ButtonAddArgument_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
