using System;
using System.Windows;
using R6Downloader.setupWizard.wizardPages;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

namespace R6Downloader.setupWizard
{
    public partial class setupWizard : Window
    {
        private int currentPage = 0;
        public setupWizard()
        {
            InitializeComponent();
            
            wizard_content.Child = new page1();
        }

        private void SetupWizard_OnDeactivated(object sender, EventArgs e)
        {
            Window window = (Window)sender;
            window.Topmost = true;
        }

        private void Btn_next_OnClick(object sender, RoutedEventArgs e)
        {
            var pages = getPages();
            if (currentPage < (pages.Count - 1))
            {
                currentPage++;
                wizard_content.Child = (UIElement)pages[currentPage];
            }
        }

        private void Btn_back_OnClick(object sender, RoutedEventArgs e)
        {
            var pages = getPages();
            if (currentPage > 0)
            {
                currentPage--;
                wizard_content.Child = (UIElement)pages[currentPage];
            }
        }

        private List<object> getPages()
        {
            return new List<object>
            {
                new page1(),
                new page2(),
                new page3(),
                new page4(),
                new final()
            };
        }
    }
}