﻿using Microsoft.Maui;
using Microsoft.UI.Xaml;
using Windows.ApplicationModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace VirtualListViewSample
{
    public partial class Application : MauiWinUIApplication
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public Application()
        {
            this.InitializeComponent();
        }

        protected override MauiApp CreateMauiApp()
            => MauiProgram.Create();
    }

}
