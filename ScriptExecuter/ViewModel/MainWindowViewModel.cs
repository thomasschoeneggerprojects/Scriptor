using ScriptExecutor.View;
using ScriptExecutorLib.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TsSolution.WpfCommon;

namespace ScriptExecutor.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private UserControl _content;

        public UserControl Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
                NotifyPropertyChanged();
            }
        }

        protected override async Task OnLoadingAfterInitializeAsync()
        {
            // Not needed at the moment!?1ß
        }

        internal void OpenScriptEditor()
        {
            if (Content is ScriptEditor)
            {
                return;
            }

            Content = new ScriptEditor();
        }

        internal void OpenScriptExecution()
        {
            if (Content is ScriptExecution)
            {
                return;
            }

            Content = new ScriptExecution();
        }
    }
}