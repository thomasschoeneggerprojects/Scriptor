using ScriptExecutorPrime.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TsSolution.WpfCommon;

namespace ScriptExecutorPrime
{
    internal class MainWindowModel : ViewModelBase
    {
        protected override async Task OnLoadingAfterInitializeAsync()
        {
        }

        internal void SetDefaultContent()
        {
            SetCreateTestScriptsContent();
        }

        internal void SetCreateTestScriptsContent()
        {
            if (Content is ScriptManagementView)
            {
                return;
            }
            Content = new ScriptManagementView();
        }

        private UserControl content;

        public UserControl Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
                NotifyPropertyChanged();
            }
        }
    }
}