using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TsSolution.WpfCommon;
using TsSolutions.WpfCommon.Controls;

namespace ScriptExecutorLib.ViewModel
{
    internal class ScriptEditorViewModel : ViewModelBase
    {
        private UserControl _executionItems;

        public UserControl ExecutionItems
        {
            get
            {
                if (_executionItems == null)
                {
                    var executionItems = new UserControlList();
                    _executionItems = executionItems;
                }
                return _executionItems;
            }
            set
            {
                _executionItems = value;
                NotifyPropertyChanged();
            }
        }

        protected override Task OnLoadingAfterInitializeAsync()
        {
            return Task.CompletedTask;
        }

        internal void SetItems(List<UserControl> items)
        {
            ((UserControlList)ExecutionItems).ListItems = items;
        }
    }
}