using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using R6Downloader;

namespace CMDHandler
{
    public class CommandHandler
    {
        public event EventHandler Output;
        public event EventHandler ProgressChanged;
        public StreamWriter ProcessInput;
        public string outputStream { get; set; }
        public double progress { get; set; }
        public CommandHandler()
        {
            
        }

        private void onOutput(string txt)
        {
            if (!String.IsNullOrEmpty(txt))
            {
                this.outputStream = txt;
                checkIfOutputIsProgressData(txt);
                EventHandler handler = Output;
                if(null != handler) handler(this, EventArgs.Empty);
            }
        }

        public void checkIfOutputIsProgressData(string txt)
        {
            try
            {
                string chopped2 = txt.Substring(0, 6);
                try
                {
                    double tmp = Convert.ToDouble(chopped2);
                    changeProgress(tmp);
                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception exception)
            {
                
            }
        }
        public void changeProgress(double value)
        {
            this.progress = value;
            EventHandler handler = ProgressChanged;
            if(null != handler) handler(this, EventArgs.Empty);
        }

        public void RunCommand(string command)
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo("cmd")
                {
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    Arguments = String.Format("/c \"{0}\"", command),
                }
            };
            process.OutputDataReceived += (sender, args) => onOutput(args.Data);
            process.Start();
            ProcessInput = process.StandardInput;
            
            process.BeginOutputReadLine();
            
        }
        
    }
}