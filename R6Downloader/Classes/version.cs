using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using CMDHandler;
using R6Downloader.Views;

namespace R6Downloader
{
    public class version
    {
        public CommandHandler CMD = new CommandHandler();
        
        public event EventHandler ProgresssChanged;
        public event EventHandler OutputRecieved;
        public string Name { get; set; }
        public string Image { get; set; }
        public string ApiName { get; set; }
        public string VersionStr { get; set; }
        public string[] Operators { get; set; }
        public string Map { get; set; }
        public string Special { get; set; }
        public string Size { get; set; }
        public string binaryFileName { get; set; }

        public version(string name, string image, string apiname,string versionStr, string[] operators, string map, string special, string size, string _binaryFileName = "RainbowSix.exe")
        {
            this.Name = name;
            this.Image = image;
            this.ApiName = apiname;
            this.Operators = operators;
            this.Map = map;
            this.Special = special;
            this.Size = size;
            this.VersionStr = versionStr;
            this.binaryFileName = _binaryFileName;
            CMD.ProgressChanged += CMDOnProgressChanged;
            CMD.Output += CMDOnOutput;
        }
        
        public void attemptDownload()
        {
            WebClient client = new WebClient();
            string downloadString = client.DownloadString("http://weexnes.eu:23420/" + this.ApiName);
            apiResponse downloadVersion = JsonConvert.DeserializeObject<apiResponse>(downloadString);
            if (downloadVersion != null)
            {
                if (downloadVersion.isValid())
                {
                    startDownload(downloadVersion);
                }
                else
                {
                    MessageBox.Show("Couldn't start Download");
                }
            }
        }

        public void launch()
        {
            Process p = new Process();
            p.StartInfo.FileName = globals.installPath + "\\" + this.generateFilePath() + "\\" + this.binaryFileName;
            p.Exited += new EventHandler(gameExit); 
            p.EnableRaisingEvents = true;
            try
            {
                p.Start();
                this.rpcPlaying();
                globals.currentlyPlaying = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Cant start " + this.Name + " when its not installed", e.GetType().ToString());
            }
            
        }
        void gameExit(object sender, EventArgs e)
        {
            this.rpcViewPage();
            globals.currentlyPlaying = false;
        }

        public void startDownload(apiResponse res)
        {
            rpcDownload();
            string path = globals.installPath + "\\" + this.generateFilePath();
            
            string command1 = "dotnet DepotDownloader.dll -app 359550 -depot 377237 -manifest " + res.binary +
                              " -username " + globals.steamUsername + " -remember-password -dir \"" + path +
                              "\" -validate -max-servers 15 -max-downloads 10 -password " + globals.steampasswd;
            string command2 = "dotnet DepotDownloader.dll -app 359550 -depot 359551 -manifest " + res.content +
                              " -username " + globals.steamUsername + " -remember-password -dir \"" + path +
                              "\" -validate -max-servers 15 -max-downloads 10 -password " + globals.steampasswd;
            string commandEnd = "echo download f1nished";

            //string command = command1 + " && " + command2 + " && " + commandEnd;
            string command = command1 + " && " + commandEnd;
            CMD.RunCommand(command);
            

        }


        private void CMDOnOutput(object sender, EventArgs e)
        {
            EventHandler handler = OutputRecieved;
            if(null != handler) handler(this, EventArgs.Empty);
        }

        private void CMDOnProgressChanged(object sender, EventArgs e)
        {
            EventHandler handler = ProgresssChanged;
            if(null != handler) handler(this, EventArgs.Empty);
        }


        /*
        private string F()
        {
            string uin = Ask();
            CMD.ProcessInput.WriteLine(uin);
            MessageBox.Show(uin);
            return "monke";
        }
        public static string Ask()
        {
            var form = new userInput();
            form.ShowDialog();
            return form.userinput.Text;
        }

        private Task<string> RunOnUiAsync(Func<string> f)
        {
            var dispatcherOperation = Application.Current.Dispatcher.InvokeAsync(f);
            return dispatcherOperation.Task;
        }
*/
        public void uninstall()
        {
            string path = globals.installPath + "\\" + this.generateFilePath();
            try
            {
                Directory.Delete(path, true);
                MessageBox.Show(this.Name + " was uninstalled");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Cant uninstall " + this.Name + " when its not installed", exception.GetType().ToString());
            }
            
        }
        public string generateFilePath()
        {
            return this.VersionStr + "_" + this.ApiName;
        }

        public void rpcViewPage()
        {
            globals.rpcclient.UpdateDetails("Browsing...");
            globals.rpcclient.UpdateState(this.Name);
            globals.rpcclient.UpdateLargeAsset("logo", "R6Downloader by WeeXnes");
        }

        public void rpcDownload()
        {
            globals.rpcclient.UpdateDetails("Downloading...");
            globals.rpcclient.UpdateState(this.Name);
            globals.rpcclient.UpdateLargeAsset(this.ApiName, this.Name);
        }

        public void rpcPlaying()
        {
            globals.rpcclient.UpdateDetails("Playing...");
            globals.rpcclient.UpdateState(this.Name);
            globals.rpcclient.UpdateLargeAsset(this.ApiName, this.Name);
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
}