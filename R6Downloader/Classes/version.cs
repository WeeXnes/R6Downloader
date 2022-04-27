using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;
using Newtonsoft.Json;
using cmdInterface;

namespace R6Downloader
{
    public class version
    {
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
            string path = globals.installPath + "\\" + this.generateFilePath();
            CMD.execute(new []
            {
                "title Downloading " + this.Name,
                "dotnet DepotDownloader.dll -app 359550 -depot 377237 -manifest " + res.binary + " -username " + globals.steamUsername + " -remember-password -dir \"" + path + "\" -validate -max-servers 15 -max-downloads 10",
                "dotnet DepotDownloader.dll -app 359550 -depot 359551 -manifest " + res.content + " -username " + globals.steamUsername + " -remember-password -dir \"" + path + "\" -validate -max-servers 15 -max-downloads 10"
            });
        }

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