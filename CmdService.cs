using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace ConsoleApp2
{
    class CmdService:IDisposable
    {
        Process cmdProcess;
        StreamWriter streamwriter;
        AutoResetEvent outputWaitHandle;
        string cmdoutput;
        public CmdService(string cmdpath)
        {
            cmdProcess = new Process();
            outputWaitHandle = new AutoResetEvent(false);
            cmdoutput = string.Empty;

            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = cmdpath;
            info.UseShellExecute = false;
            info.RedirectStandardOutput = true;
            info.RedirectStandardInput = true;
            info.CreateNoWindow = true;

            cmdProcess.OutputDataReceived += cmdoutputdate_rec;
            cmdProcess.StartInfo = info;
            cmdProcess.Start();

            streamwriter = cmdProcess.StandardInput;
            cmdProcess.BeginOutputReadLine();
        }

        public void Dispose()
        {
            cmdProcess.Close();
            cmdProcess.Dispose();
            streamwriter.Close();
            streamwriter.Dispose();
        }

        public string ExcuteCommon(string command)
        {
            cmdoutput = string.Empty;
            streamwriter.WriteLine(command);
            streamwriter.WriteLine("echo end");
            outputWaitHandle.WaitOne();
            return cmdoutput;
        }
        private void cmdoutputdate_rec(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null || e.Data == "end")
                outputWaitHandle.Set();
            else
                cmdoutput += e.Data + Environment.NewLine;
        }
    }
}
