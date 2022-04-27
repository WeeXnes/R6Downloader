using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace R6Downloader.setupWizard.wizardPages
{
    public partial class page3 : UserControl
    {
        public page3()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start(
                "https://download.visualstudio.microsoft.com/download/pr/1ac0b57e-cf64-487f-aecf-d7df0111fd56/2484cbe1ffacceacaa41eab92a6de998/dotnet-runtime-6.0.3-win-x64.exe");
        }
    }
}