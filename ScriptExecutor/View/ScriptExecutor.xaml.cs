using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.IO;
using Path = System.IO.Path;
using ScriptExecutorLib.ViewModel;
using ScriptExecutorLib;
using ScriptExecutorLib.Model.Execution;

namespace ScriptExecutor.View
{
    /// <summary>
    /// Interaction logic for ScriptExecutor.xaml
    /// </summary>
    public partial class ScriptExecutor : UserControl
    {
        private Process _CurrentRunningProcess;

        private ScriptExecutorViewModel VM;

        public ScriptExecutor()
        {
            InitializeComponent();
            VM = new ScriptExecutorViewModel(ServiceProvider.Get<IExecutionItemManager>());
            this.DataContext = VM;
        }

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_CurrentRunningProcess != null && !_CurrentRunningProcess.HasExited)
            {
                LogErrorInformation("Process currently running");
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    labelErrorInformation.Content = "Process currently running";
                }));

                return;
            }

            const string pathSource = "C:\\Program Files (x86)\\Microsoft SDKs\\NuGetPackages\\";
            const string nugetPackageName = "TS.Solutions.ScriptExecutor.1.0.0.nupkg";
            const string testBatDirectory = @"C:\Users\Thomas\source\repos\ScriptExecuter\ScriptExecuter\bin\Debug\net6.0-windows";
            const string testBatFileName = "createDir.bat";
            const string fullFilePath = @"D:\Test\createDir.bat";

            Task.Run(() =>
            {
                int ExitCode;
                ProcessStartInfo ProcessInfo;

                _CurrentRunningProcess = new Process();
                _CurrentRunningProcess.StartInfo.WorkingDirectory = testBatDirectory;
                _CurrentRunningProcess.StartInfo.FileName = testBatFileName;
                _CurrentRunningProcess.StartInfo.UseShellExecute = true;
                _CurrentRunningProcess.StartInfo.CreateNoWindow = false;
                _CurrentRunningProcess.Start();
                _CurrentRunningProcess.WaitForExit();

                ClearErrorInformation();

                _CurrentRunningProcess.Close();
                _CurrentRunningProcess = null;
            });
        }

        private void LogErrorInformation(string message)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                labelErrorInformation.Content = message;
            }));
        }

        private void ClearErrorInformation()
        {
            LogErrorInformation(string.Empty);
        }

        #region Button Actions

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            VM.DeleteTest();
        }

        private void btnCreateDef_Click(object sender, RoutedEventArgs e)
        {
            VM.CreateTest();
        }

        #endregion Button Actions
    }
}