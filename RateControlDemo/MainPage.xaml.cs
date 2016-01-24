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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RateControlDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            rate.CancelCommand = new RelayCommand(CancelClicked);
            rate.OkCommand = new RelayCommand(OkClicked);
        }

        public void CancelClicked(object x)
        {
            rate.IsOpen = false;
        }

        public async void OkClicked(object x)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store:reviewapp"));
            rate.IsOpen = false;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            rate.IsOpen = !rate.IsOpen;
            //rate.OkCommand = OkCommand;
        }
    }
}
