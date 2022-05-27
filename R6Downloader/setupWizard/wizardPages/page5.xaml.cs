using System;
using System.Windows;
using System.Windows.Controls;
using Nocksoft.IO.ConfigFiles;

namespace R6Downloader.setupWizard.wizardPages
{
    public partial class page5 : UserControl
    {
        public page5()
        {
            InitializeComponent();
        }

        private void Btn_passwdapply_OnClick(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(input_steampasswd.Text))
            {
                //MessageBox.Show("valid input");
                globals.steampasswd = input_steampasswd.Text;
                INIFile settings = new INIFile(globals.appdataPath + "\\settings.ini", true);
                settings.SetValue("Settings", "steamPasswd", globals.steampasswd);
            }
        }
    }
}