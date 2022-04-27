using System;
using System.Windows;
using System.Windows.Controls;
using Nocksoft.IO.ConfigFiles;

namespace R6Downloader.setupWizard.wizardPages
{
    public partial class page4 : UserControl
    {
        public page4()
        {
            InitializeComponent();
        }

        private void Btn_nameapply_OnClick(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(input_steamname.Text))
            {
                //MessageBox.Show("valid input");
                globals.steamUsername = input_steamname.Text;
                INIFile settings = new INIFile(globals.appdataPath + "\\settings.ini", true);
                settings.SetValue("Settings", "steamUsername", globals.steamUsername);
            }
        }
    }
}