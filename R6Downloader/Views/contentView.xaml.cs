using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using System.Windows.Controls;
using DiscordRPC;

namespace R6Downloader
{
    public partial class contentView : UserControl
    {
        public version selectedVersion { get; set; }
        public contentView(version _version)
        {
            InitializeComponent();
            #region Load Parameters into UI
            this.selectedVersion = _version;
            label_name.Text = _version.Name;
            string[] sortedOperatorArray = _version.Operators;
            Array.Sort(sortedOperatorArray);
            int i = 0;
            string OperatorText = "";
            foreach (string s in sortedOperatorArray)
            {
                if (i == 0)
                {
                    OperatorText = OperatorText + s;
                    i++;
                }
                else
                {
                    OperatorText = OperatorText + ", " + s;
                }
            }
            label_operator.Text = OperatorText;
            label_map.Text = _version.Map;
            label_special.Text = _version.Special;
            label_size.Text = _version.Size + "GB";


            #endregion

            if (!globals.currentlyPlaying)
            {
                this.selectedVersion.rpcViewPage();
            }

            this.selectedVersion.CMD.ProgressChanged += (sender, args) =>
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    this.downloadProgressBar1.Value = Convert.ToInt32(this.selectedVersion.CMD.progress);
                }));
            };
            this.selectedVersion.CMD.Output += (sender, args) =>
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (!String.IsNullOrEmpty(this.selectedVersion.CMD.outputStream))
                    {
                        if (this.selectedVersion.CMD.outputStream == "download f1nished")
                        {
                            this.downloadProgressBar1.Visibility = Visibility.Hidden;
                            globals.currentlyPlaying = false;
                            this.selectedVersion.rpcViewPage();
                        }
                        this.outputLog.AppendText(this.selectedVersion.CMD.outputStream + "\n");
                        this.outputLog.ScrollToEnd();
                    }
                }));
            };
        }

        private void Btn_install_OnClick(object sender, RoutedEventArgs e)
        {
            globals.currentlyPlaying = true;
            this.selectedVersion.attemptDownload();
            downloadProgressBar1.Visibility = Visibility.Visible;
        }

        private void Btn_uninstall_OnClick(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show();
            this.selectedVersion.uninstall();
        }

        private void Btn_start_OnClick(object sender, RoutedEventArgs e)
        {
            this.selectedVersion.launch();
        }
    }
}