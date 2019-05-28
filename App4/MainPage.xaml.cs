using Windows.UI.Xaml.Controls;
using Windows.Devices.Geolocation;
using System;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using System.Net.Http;
using App4;
using System.Text;
using Windows.UI.Core;


// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace App4
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DispatcherTimer disptimer = new DispatcherTimer();
        Geoposition pos;
        Geolocator geolocator = new Geolocator();
        HttpClient client = new HttpClient();
        Frame Tf = Window.Current.Content as Frame;
        public MainPage()
        {
            this.InitializeComponent();
            //disptimer.Interval = new TimeSpan(00, 00, 00, 00, 00);
            //disptimer.Tick += Disptimer_Tick;
           

        }

        private void CurrentView_BackRequested(object sender, BackRequestedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private async void Disptimer_Tick(object Sender, object e)
        {

            //geolocator.DesiredAccuracy = PositionAccuracy.High;
            ////geolocator.DesiredAccuracyInMeters = 12;
            //try
            //{
            //    pos = await geolocator.GetGeopositionAsync();
            //    Latitude_Text.Text = pos.Coordinate.Point.Position.Latitude.ToString();
            //    Longtitude_Text.Text = pos.Coordinate.Point.Position.Longitude.ToString();
               
            //        DataModel dm = new DataModel
            //        {
            //            Name = "Client1",
            //            CoordinateX = Latitude_Text.Text.Replace(",","."),
            //            CoordinateY = Longtitude_Text.Text.Replace(",",".")
            //        };
            //        var clientsJson = JsonConvert.SerializeObject(dm);

            //        var HttpContent = new StringContent(clientsJson);
            //        HttpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            //        await client.PutAsync("http://178.88.161.71/api/values/1", HttpContent);          
            //}

            //catch
            //{

            //}
        }
      private async void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //geolocator.DesiredAccuracy = PositionAccuracy.High;
            //geolocator.DesiredAccuracyInMeters = 12;
            try
            {
                //pos = await geolocator.GetGeopositionAsync();
                //Latitude_Text.Text = pos.Coordinate.Point.Position.Latitude.ToString();
                //Longtitude_Text.Text = pos.Coordinate.Point.Position.Longitude.ToString();

                DataModel dm = new DataModel
                {
                    IdentName = "Container1",
                    CoordinateX = "777",
                    CoordinateY = "888"
                };
                var clientsJson = JsonConvert.SerializeObject(dm);

                var HttpContent = new StringContent(clientsJson);
                HttpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                await client.PutAsync("http://178.88.161.71/api/values/1", HttpContent);
            }

            catch
            {

            }

        }

        private void ButtonClear_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Latitude_Text.Text = "";
            Longtitude_Text.Text = "";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //disptimer.Start();
        }

        private void Webapi_Page_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(WebApi));
        }

        private void Map_Page_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Map_Page));
        }

        private void Dispatcher_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Dispatcher));
        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            var currentView = SystemNavigationManager.GetForCurrentView();
            if (Tf.CanGoBack)
            {
                
                currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                currentView.BackRequested += CurrentView_BackRequested;
            }

            else
            {

                currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            }
        }
    }
}
