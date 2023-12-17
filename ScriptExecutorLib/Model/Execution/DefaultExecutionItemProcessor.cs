using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace ScriptExecutorLib.Model.Execution
{
    internal class DefaultExecutionItemProcessor : IExecutionItemProcessor
    {
        private Process? _CurrentRunningProcess;
        private ExecutionInformation _currentExecutionInformation = ExecutionInformation.Nothing();
        private DateTimeOffset _startTime;

        public DefaultExecutionItemProcessor()
        {
        }

        public async Task<ExecutionInformation> Run(ExecutionItem executionItem)
        {
            if (executionItem.ItemType == ExecutionItemType.Powershell)
            {

                ExecutePowerShellScript(executionItem);
                return ExecutionInformation.Finished(DateTimeOffset.Now);
            }
            ExecutePowerShellScript(executionItem);
            return ExecutionInformation.Finished(DateTimeOffset.Now);
            //_startTime = DateTimeOffset.Now;
            //if (_CurrentRunningProcess != null && !_CurrentRunningProcess.HasExited)
            //{
            //    return ExecutionInformation.Aborted(_startTime);
            //}

            //const string pathSource = "C:\\Program Files (x86)\\Microsoft SDKs\\NuGetPackages\\";
            //const string nugetPackageName = "TS.Solutions.ScriptExecutor.1.0.0.nupkg";
            //const string testBatDirectory = @"C:\Users\Thomas\source\repos\ScriptExecuter\ScriptExecuter\bin\Debug\net6.0-windows";
            //const string testBatFileName = "createDir.bat";
            //const string fullFilePath = @"D:\Test\createDir.bat";



            //await Task.Run(() =>
            //{
            //    try
            //    {
            //        int ExitCode;
            //        ProcessStartInfo ProcessInfo;

            //        _CurrentRunningProcess = new Process();

            //        //_CurrentRunningProcess.StartInfo.WorkingDirectory = Path.GetDirectoryName(executionItem.ItemFilePath);
            //        //_CurrentRunningProcess.StartInfo.FileName = executionItem.ItemFilePath;
            //        _CurrentRunningProcess.StartInfo.UseShellExecute = true;
            //        _CurrentRunningProcess.StartInfo.CreateNoWindow = false;
            //        _CurrentRunningProcess.Start();
            //        _CurrentRunningProcess.WaitForExit();

            //        //ClearErrorInformation();

            //        _CurrentRunningProcess.Close();
            //    }
            //    catch (Exception)
            //    {
            //        _currentExecutionInformation = ExecutionInformation.Aborted(_startTime);
            //    }
            //    finally
            //    {
            //        _CurrentRunningProcess = null;
            //    }
            //});

            return _currentExecutionInformation;
        }

        private ExecutionInformation ExecutePowerShellScript(ExecutionItem executionItem)
        {
            //InitialSessionState iss = InitialSessionState.Create();
            //Runspace rs = RunspaceFactory.CreateRunspace(iss);
            //rs.Open();

            /* -- Test script --
             git --version
             git log
             
             
             
             */

            try
            {
                StringBuilder sb = new StringBuilder();
                using (PowerShell ps = PowerShell.Create())
                {
                    //ps.Runspace = rs;
                    //ps.AddScript(@"D:\MyDir\sc.ps1 | Out-String");
                    ps.AddScript(executionItem.Content);
                    Collection<PSObject> psOutput = ps.Invoke();

                    foreach (PSObject outputItem in psOutput)
                    {
                        if (outputItem != null)
                        {
                            string outputValue = outputItem.BaseObject.ToString();
                            sb.AppendLine(outputValue);
                        }
                    }
                }

                var result = sb.ToString();

                return ExecutionInformation.Finished(DateTimeOffset.Now);
            }
            catch (Exception ex)
            {
                return ExecutionInformation.Aborted(DateTimeOffset.Now);
                
            }
            

            //Collection<CommandInfo> result = ps.Invoke<CommandInfo>();
            //ps.AddScript(executionItem.Content + "| Out-String" );

            //ps.AddParameter("secret_key", key);
            //ps.AddParameter("message", message);
            //var results = ps.Invoke<string>();


            //var result = ps.Invoke();

            //rs.Close();
        }
    }
}