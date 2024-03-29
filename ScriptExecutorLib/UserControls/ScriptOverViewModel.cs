﻿using ScriptExecutorLib.Model.Execution;
using ScriptExecutorLib.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TsSolution.WpfCommon;

namespace ScriptExecutorLib.UserControls
{
    internal class ScriptOverViewModel : ViewModelBase
    {
        private IExecutionItemProcessor _executionItemProcessor;

        public ScriptOverViewModel(IExecutionItemProcessor executionItemProcessor)
        {
            this._executionItemProcessor = executionItemProcessor;
        }

        private ExecutionItem _executionItem;

        protected override Task OnLoadingAfterInitializeAsync()
        {
            return Task.CompletedTask;
        }

        internal void SetItem(ExecutionItem executionItem)
        {
            this._executionItem = executionItem;
            RunOnUIThread(() =>
            {
                ScriptName = _executionItem.Name;
                ScriptDescription = _executionItem.Description;
            });
        }

        private string _scriptName;

        public string ScriptName
        {
            get
            {
                return _scriptName;
            }
            set
            {
                _scriptName = value;
                NotifyPropertyChanged();
            }
        }

        private string _scriptDescription;

        public string ScriptDescription
        {
            get
            {
                return _scriptDescription;
            }
            set
            {
                _scriptDescription = value;
                NotifyPropertyChanged();
            }
        }

        #region Execute Script

        internal void ExecuteScript()
        {
            _executionItemProcessor.Run(_executionItem);
        }

        internal void OpenEditScript()
        {
            ScriptEditUserControl content = new(_executionItem.Id);
            var containerWindow = new ContainerWindow(content);
            var result = containerWindow.ShowDialog();
        }

        #endregion Execute Script
    }
}