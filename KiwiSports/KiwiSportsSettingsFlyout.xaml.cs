using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace KiwiSports
{
    /// <summary>
    /// Displays the about section as a flyout from the right side
    /// </summary>
    public sealed partial class KiwiSportsSettingsFlyout : SettingsFlyout
    {
        public KiwiSportsSettingsFlyout()
        {
            this.InitializeComponent();
        }
    }
}
