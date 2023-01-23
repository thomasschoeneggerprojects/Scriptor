using ScriptExecutorLib.Model.Execution;
using ScriptExecutorPrime.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TsSolution.WpfCommon;
using TsSolutions.WpfCommon.Controls.Input;
using TsSolutions.WpfCommon.TestViews;

namespace ScriptExecutorPrime
{
    internal class MainWindowModel : ViewModelBase
    {
        public MainWindowModel()
        {
        }

        protected override Task OnLoadingAfterInitializeAsync()
        {
            return Task.CompletedTask;
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

        internal void SetTestUIControlsContent()
        {
            Content = new TestRtbEditorView();
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