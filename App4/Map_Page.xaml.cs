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
using App4;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace App4
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class Map_Page : Page
    {
       
        public Map_Page()
        {
            this.InitializeComponent();
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                Windows.Phone.UI.Input.HardwareButtons.BackPressed += OnBackPressed;
            }
        
        }

        public void OnBackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            if (Window.Current.Content is Frame rootFrame)
            {
                if (rootFrame.CanGoBack)
                {
                    e.Handled = true;
                    rootFrame.GoBack();
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
          string  CoordinateX = "76.913581";
           string CoordinateY = "43.251351";
            MainPage mp = new MainPage();
            string tr = "https://yandex.kz/maps/162/almaty/?ll=" + CoordinateX + "%2C" + CoordinateY + "&z=17&mode=whatshere&whatshere%5Bpoint%5D=76.913630%2C43.251421&whatshere%5Bzoom%5D=17";
            Uri target = new Uri(tr);
            this.WebWiew1.Navigate(target);
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }
}
