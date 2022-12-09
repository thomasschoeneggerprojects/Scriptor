using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TsSolutions.Service;

namespace TsSolution.WpfCommon
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region Initialization View Model

        internal bool IsInitialized => _initDone;
        protected bool _initDone = false;

        protected ViewModelBase()
        {
            //InitViewModelBase(5000);
        }

        protected ViewModelBase(int minLoadingTimeMs)
        {
            //InitViewModelBase(minLoadingTimeMs);
        }

        protected void InitViewModel()
        {
            InitViewModelBase(5000);
        }

        private void InitViewModelBase(int minLoadingTimeMs)
        {
            _initDone = false;

            var task = Task<bool>.Run(async () =>
            {
                OnInitialize();
                await OnLoadingAfterInitializeAsync();

                _initDone = true;

                return true;
            });
        }

        protected abstract System.Threading.Tasks.Task OnLoadingAfterInitializeAsync();

        protected virtual void OnInitialize()
        {
            // Default Method to override
        }

        #endregion Initialization View Model

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region UI thread execution

        public async Task RunOnUIThread(Func<System.Threading.Tasks.Task> action, [CallerMemberName] string callerName = "")
        {
            await Application.Current.Dispatcher.InvokeAsync(async () =>
            {
                try
                {
                    await action.Invoke().ConfigureAwait(true);
                }
                catch (Exception ex)
                {
                    // TODO Logging
                }
            });
        }

        public void RunOnUIThread(Action action, [CallerMemberName] string callerName = "")
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                try
                {
                    action.Invoke();
                }
                catch (Exception ex)
                {
                    // TODO Logging
                }
            });
        }

        #endregion UI thread execution

        #region States

        private bool _isWorking = false;

        public bool IsWorking
        {
            get
            {
                return _isWorking;
            }
            protected set
            {
                _isWorking = value;
                NotifyPropertyChanged();
            }
        }

        #endregion States
    }
}