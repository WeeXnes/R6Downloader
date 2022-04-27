using System.Windows;
using System.Windows.Controls;

namespace R6Downloader.setupWizard.wizardPages
{
    public partial class final : UserControl
    {
        public final()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Window wizardWindow = Window.GetWindow(this);
            wizardWindow.Close();
        }
    }
}