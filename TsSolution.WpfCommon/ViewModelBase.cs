using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TsSolution.WpfCommon
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
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
            Application.Current.Dispatcher.Invoke(async () =>
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
    }
}