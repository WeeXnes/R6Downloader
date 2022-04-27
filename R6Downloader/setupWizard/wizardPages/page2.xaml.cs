using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.IO.Compression;

namespace R6Downloader.setupWizard.wizardPages
{
    public partial class page2 : UserControl
    {
        public page2()
        {
            InitializeComponent();
        }

        private void Btn_downloaddepotdownloader_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/SteamRE/DepotDownloader/releases");
        }

        private void UIElement_OnDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[]) e.Data.GetData(DataFormats.FileDrop);
                //MessageBox.Show(Path.GetFileName(files[0]));
                //MessageBox.Show(files[0]);
                
                ZipFile.ExtractToDirectory(
                    files[0],
                    AppDomain.CurrentDomain.BaseDirectory
                    );
                infolabel.Text = "Action successful";
            }
        }
    }
}