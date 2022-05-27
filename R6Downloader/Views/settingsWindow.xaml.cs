using System;
using System.Windows;
using Nocksoft.IO.ConfigFiles;
using wizard = R6Downloader.setupWizard.setupWizard;

namespace R6Downloader
{
    public partial class settingsWindow : Window
    {
        public settingsWindow()
        {
            InitializeComponent();
            label_path.Text = globals.installPath;
            label_steamname.Text = globals.steamUsername;
            label_steampasswd.Text = globals.steampasswd;
            if (globals.disableImages)
            {
                disableImagescheckbox.IsChecked = true;
            }
        }

        private void Btn_selectPath_OnClick(object sender, RoutedEventArgs e)
        {
            
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (!String.IsNullOrEmpty(dialog.SelectedPath))
                {
                    //MessageBox.Show("Pfad okay");
                    globals.installPath = dialog.SelectedPath;
                    INIFile settings = new INIFile(globals.appdataPath + "\\settings.ini", true);
                    settings.SetValue("Settings", "installPath", globals.installPath);
                }
            }
            label_path.Text = globals.installPath;
            
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
            
            label_steamname.Text = globals.steamUsername;
        }

        private void Btn_openwizard_OnClick(object sender, RoutedEventArgs e)
        {
            
            wizard setupWizard = new wizard();
            setupWizard.Show();
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
            
            label_steampasswd.Text = globals.steampasswd;
        }

        private void DisableImagescheckbox_OnChecked(object sender, RoutedEventArgs e)
        {
            globals.disableImages = true;
            INIFile settings = new INIFile(globals.appdataPath + "\\settings.ini", true);
            settings.SetValue("Settings", "disableImages", globals.disableImages.ToString());
        }

        private void DisableImagescheckbox_OnUnchecked(object sender, RoutedEventArgs e)
        {
            globals.disableImages = false;
            INIFile settings = new INIFile(globals.appdataPath + "\\settings.ini", true);
            settings.SetValue("Settings", "disableImages", globals.disableImages.ToString());
        }
    }
}