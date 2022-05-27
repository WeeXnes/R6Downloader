using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using CMDHandler;
using DiscordRPC;
using Newtonsoft.Json.Linq;
using Nocksoft.IO.ConfigFiles;
using Button = System.Windows.Controls.Button;
using wizard = R6Downloader.setupWizard.setupWizard;


namespace R6Downloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public List<version> versions = new List<version>();
        public MainWindow()
        {
            LoadVersions();
            InitializeComponent();
            LoadSettings();
            border_content.Child = new welcomeView();
            globals.rpcclient.Initialize();
            globals.rpcclient.SetPresence(new RichPresence()
            {
                Details = "Browsing...",
                State = "Welcome Page",
                Assets = new Assets()
                {
                    LargeImageKey = "logo",
                    LargeImageText = "R6Downloader by WeeXnes"
                }
            });
            

        }


        public void LoadSettings()
        {
            globals.appdataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "r6downloader");
            if (!Directory.Exists(globals.appdataPath))
            {
                Directory.CreateDirectory(globals.appdataPath);
                wizard setupWizard = new wizard();
                setupWizard.Show();
            }
            INIFile settings = new INIFile(globals.appdataPath + "\\settings.ini", true);
            
            string steamUsername = settings.GetValue("Settings", "steamUsername");
            if (String.IsNullOrEmpty(steamUsername))
            {
                settings.SetValue("Settings", "steamUsername", "empty");
                steamUsername = settings.GetValue("Settings", "steamUsername");
            }
            string steamPasswd = settings.GetValue("Settings", "steamPasswd");
            if (String.IsNullOrEmpty(steamPasswd))
            {
                settings.SetValue("Settings", "steamPasswd", "empty");
                steamUsername = settings.GetValue("Settings", "steamPasswd");
            }
            
            string installPath = settings.GetValue("Settings", "installPath");
            if (String.IsNullOrEmpty(installPath))
            {
                settings.SetValue("Settings", "installPath", globals.appdataPath + "\\downloads");
                installPath = settings.GetValue("Settings", "installPath");
            }

            globals.installPath = installPath;
            globals.steamUsername = steamUsername;
            globals.steampasswd = steamPasswd;
        }

        public void LoadVersions()
        {
            versions.AddRange(new List<version>
            {
                new version("Vanilla", 
                    "https://cdn.discordapp.com/attachments/804937376211533844/954755913292611614/vanilla.png", 
                    "vanilla", 
                    "y1s0",
                    new []{"None"},
                    "Default Maps",
                    "Nothing",
                    "14.3"
                ),
                new version("Black Ice", 
                    "https://cdn.discordapp.com/attachments/804937376211533844/954753092430204928/blackice.png", 
                    "blackice", 
                    "y1s1",
                    new []{"Buck", "Frost"},
                    "Yacht",
                    "Nothing",
                    "16.7"
                ),
                new version("Dust Line", 
                    "https://cdn.discordapp.com/attachments/804937376211533844/954753091817857144/dustline.png", 
                    "dustline", 
                    "y1s2",
                    new []{"Blackbeard", "Valkyrie"},
                    "Border",
                    "Nothing",
                    "20.9"
                ),
                new version("Skull Rain", 
                    "https://cdn.discordapp.com/attachments/804937376211533844/954753113921814578/skullrain.png", 
                    "skullrain", 
                    "y1s3",
                    new []{"Capitão", "Caveira"},
                    "Favela",
                    "Nothing",
                    "25.1",
                    "RainbowSixGame.exe"
                ),
                new version("Red Crow", 
                    "https://cdn.discordapp.com/attachments/804937376211533844/954753106107834498/redcrow.png", 
                    "redcrow", 
                    "y1s4",
                    new []{"Hibana", "Echo"},
                    "Skyscraper",
                    "Nothing",
                    "28.5",
                    "RainbowSixGame.exe"
                ),
                new version("Velvet Shell", 
                    "https://cdn.discordapp.com/attachments/804937376211533844/954753114475491388/velvetshell.png", 
                    "velvetshell", 
                    "y2s1",
                    new []{"Jackal", "Mira"},
                    "Coastline",
                    "Nothing",
                    "33.2",
                    "RainbowSixGame.exe"
                ),
                new version("Health", 
                    "https://cdn.discordapp.com/attachments/804937376211533844/954753105126363146/health.png", 
                    "health", 
                    "y2s2",
                    new []{"None"},
                    "",
                    "Fixing the game lmao",
                    "34"
                ),
                new version("Blood Orchid", 
                    "https://cdn.discordapp.com/attachments/804937376211533844/954753092711235604/bloodorchid.png", 
                    "bloodorchid", 
                    "y2s3",
                    new []{"Ying", "Lesion", "Ela"},
                    "Theme Park",
                    "Nothing",
                    "34.3"
                ),
                new version("White Noise", 
                    "https://cdn.discordapp.com/attachments/804937376211533844/954753115075256411/whitenoise.png", 
                    "whitenoise", 
                    "y2s4",
                    new []{"Dokkaebi", "Zofia", "Vigil"},
                    "Tower",
                    "Nothing",
                    "48.7"
                ),
                new version("Chimera", 
                    "https://cdn.discordapp.com/attachments/804937376211533844/954753090228191263/chimera_2.png", 
                    "chimera", 
                    "y3s1",
                    new []{"Lion", "Finka"},
                    "",
                    "Outbreak",
                    "58.8"
                ),
                new version("Para Bellum", 
                    "https://cdn.discordapp.com/attachments/804937376211533844/954753105629687828/parabellum.png", 
                    "parabellum", 
                    "y3s2",
                    new []{"Maestro", "Alibi"},
                    "Villa",
                    "Nothing",
                    "63.3"
                ),
                new version("Grim Sky", 
                    "https://cdn.discordapp.com/attachments/804937376211533844/954753104769851392/grimsky.png", 
                    "grimsky", 
                    "y3s3",
                    new []{"Maverick", "Clash"},
                    "Herford Rework",
                    "Mad House",
                    "72.6"
                ),
                new version("Wind Bastion", 
                    "https://cdn.discordapp.com/attachments/804937376211533844/954758007655378964/windbastion.png", 
                    "windbastion", 
                    "y3s4",
                    new []{"Nomad", "Kaid"},
                    "Fortress",
                    "Nothing",
                    "76.9"
                ),
                new version("Burnt Horizon", 
                    "https://cdn.discordapp.com/attachments/804937376211533844/954753089536147497/burnthorizon.png", 
                    "burnthorizon", 
                    "y4s1",
                    new []{"Gridlock", "Mozzie"},
                    "Outback",
                    "Rainbow is Magic",
                    "59.7"
                ),
                new version("Phantom Sight", 
                    "https://cdn.discordapp.com/attachments/804937376211533844/954753105872945192/phantomsight.png", 
                    "phantomsight", 
                    "y4s2",
                    new []{"Nøkk", "Warden"},
                    "Café Rework",
                    "Showdown",
                    "67.1"
                ),
                new version("Ember Rise", 
                    "https://cdn.discordapp.com/attachments/804937376211533844/954753092124045332/emberrise.png", 
                    "emberrise", 
                    "y4s3",
                    new []{"Amaru", "Goyo"},
                    "Kanal Rework",
                    "Doktor's Curse, Money Heist",
                    "69.6"
                ),
                new version("Shifting Tides", 
                    "https://cdn.discordapp.com/attachments/804937376211533844/954753106653110312/shiftingtides.png", 
                    "shiftingtides", 
                    "y4s4",
                    new []{"Kali", "Wamai"},
                    "Themepark Rework",
                    "S.I. 20",
                    "75.2"
                ),
                new version("Void Edge", 
                    "https://cdn.discordapp.com/attachments/804937376211533844/954753114756501524/voidedge.png", 
                    "voidedge", 
                    "y5s1",
                    new []{"Iana", "Oryx"},
                    "Oregon Rework",
                    "Grand Larceny",
                    "74.3"
                ),
                new version("Steel Wave", 
                    "https://cdn.discordapp.com/attachments/804937376211533844/954753114186059816/steelwave.png", 
                    "steelwave", 
                    "y5s2",
                    new []{"Ace", "Melusi"},
                    "House Rework",
                    "M.U.T.E.",
                    "81.3"
                ),
                new version("Shadow Legacy", 
                    "https://cdn.discordapp.com/attachments/804937376211533844/954753106435010680/shadowlegacy.png", 
                    "shadowlegacy", 
                    "y5s3",
                    new []{"Zero", "Tachanka Rework"},
                    "Chalet Rework",
                    "Sugar Fright",
                    "~80"
                ),
                new version("Neon Dawn", 
                    "https://cdn.discordapp.com/attachments/804937376211533844/954753105357062194/neondawn.png", 
                    "neondawn", 
                    "y5s4",
                    new []{"Aruni"},
                    "Skyscraper Rework",
                    "S.I. 21",
                    "~80"
                ),
                new version("Crimson Heist", 
                    "https://cdn.discordapp.com/attachments/804937376211533844/954753090614079508/crimsonheist.png", 
                    "crimsonheist", 
                    "y6s1",
                    new []{"Flores"},
                    "Border Rework",
                    "Apocalypse",
                    "~80"
                ),
                new version("North Star", 
                    "https://cdn.discordapp.com/attachments/804937376211533844/954757441785045004/northstar.png", 
                    "northstar", 
                    "y6s2",
                    new []{"Thunderbird"},
                    "Favela Rework",
                    "Containment",
                    "~85"
                ),
                new version("Crystal Gurad", 
                    "https://cdn.discordapp.com/attachments/804937376211533844/954757442103836772/crystalguard.png", 
                    "crystalguard", 
                    "y6s3",
                    new []{"Osa"},
                    "Coastline, Bank, Clubhaus",
                    "HP Rework",
                    "~80"
                ),
                new version("High Calibre", 
                    "https://cdn.discordapp.com/attachments/804937376211533844/954757442489708544/highcalibre.png", 
                    "highcalibre", 
                    "y6s4",
                    new []{"Thorn"},
                    "Outback Rework",
                    "Snow Brawl, S.I. 22",
                    "~80"
                ),
            });
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void Btn_minimize_OnClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        
        private void Btn_close_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void VersionList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {

            if (!globals.currentlyPlaying)
            {
                var buttonObj = ((Button) sender).CommandParameter;
                version versionFromButtonObj = (version) buttonObj;
                //MessageBox.Show(versionFromButtonObj.ToString());

                border_content.Child = new contentView(versionFromButtonObj);
            }
            else
            {
                MessageBox.Show("Close your game first");
            }
        }

        private void ListViewVersions_OnLoaded(object sender, RoutedEventArgs e)
        {
            ListViewVersions.ItemsSource = versions;
        }

        private void Btn_settings_OnClick(object sender, RoutedEventArgs e)
        {
            new settingsWindow().Show();
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("dotnet");
            foreach (var p in processes)
            {
                p.Kill();
            }
        }
    }
}