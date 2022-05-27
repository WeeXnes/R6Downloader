using System.Windows;
using System.Windows.Controls;

namespace R6Downloader.setupWizard.wizardPages
{
    public partial class page6 : UserControl
    {
        public page6()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            string strCmdText= "/C dotnet DepotDownloader.dll -app 359550 -depot 34532623 -username " + globals.steamUsername +" -password " + globals.steampasswd + " && pause";
            System.Diagnostics.Process.Start("CMD.exe",strCmdText);
        }
    }
}