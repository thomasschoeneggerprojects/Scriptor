using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptExecutorLib.Model.Execution
{
    internal class DefaultExecutionItemProcessor : IExecutionItemProcessor
    {
        private Process _CurrentRunningProcess;
        private ExecutionInformation _currentExecutionInformation = ExecutionInformation.Nothing();
        private DateTimeOffset _startTime;

        public DefaultExecutionItemProcessor()
        {
        }

        public async Task<ExecutionInformation> Run(ExecutionItem executionItem)
        {
            _startTime = DateTimeOffset.Now;
            if (_CurrentRunningProcess != null && !_CurrentRunningProcess.HasExited)
            {
                return ExecutionInformation.Aborted(_startTime);
            }

            const string pathSource = "C:\\Program Files (x86)\\Microsoft SDKs\\NuGetPackages\\";
            const string nugetPackageName = "TS.Solutions.ScriptExecutor.1.0.0.nupkg";
            const string testBatDirectory = @"C:\Users\Thomas\source\repos\ScriptExecuter\ScriptExecuter\bin\Debug\net6.0-windows";
            const string testBatFileName = "createDir.bat";
            const string fullFilePath = @"D:\Test\createDir.bat";

            await Task.Run(() =>
            {
                try
                {
                    int ExitCode;
                    ProcessStartInfo ProcessInfo;

                    _CurrentRunningProcess = new Process();

                    _CurrentRunningProcess.StartInfo.WorkingDirectory = Path.GetDirectoryName(executionItem.ItemFilePath);
                    _CurrentRunningProcess.StartInfo.FileName = executionItem.ItemFilePath;
                    _CurrentRunningProcess.StartInfo.UseShellExecute = true;
                    _CurrentRunningProcess.StartInfo.CreateNoWindow = false;
                    _CurrentRunningProcess.Start();
                    _CurrentRunningProcess.WaitForExit();

                    //ClearErrorInformation();

                    _CurrentRunningProcess.Close();
                }
                catch (Exception)
                {
                    _currentExecutionInformation = ExecutionInformation.Aborted(_startTime);
                }
                finally
                {
                    _CurrentRunningProcess = null;
                }
            });

            return _currentExecutionInformation;
        }
    }
}