using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.Gpio;
using Windows.Devices.SerialCommunication;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace App4
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class Dispatcher : Page
    {
        string deviceId;
        SerialDevice SerialPort;
        DispatcherTimer timer = new DispatcherTimer();
        HttpClient client = new HttpClient();
        Frame Tf = Window.Current.Content as Frame;
        public Dispatcher()
        {
            this.InitializeComponent();

          

            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))   //Back Button
            {
                Windows.Phone.UI.Input.HardwareButtons.BackPressed += OnBackPressed;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
           
            if (Tf.CanGoBack)
            {
                var currentView = SystemNavigationManager.GetForCurrentView();
                currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                currentView.BackRequested += CurrentView_BackRequested;
            }
           
        }

        private void CurrentView_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (Tf.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        public void OnBackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
           
            if (Tf != null)
            {
                if (Tf.CanGoBack)
                {
                    e.Handled = true;
                   Tf.GoBack();
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Start();

            StartButton.IsEnabled = false;
            StopButton.IsEnabled = true;
        }

        private async void Timer_Tick(object sender, object e)
        {
            SerialPort = await SerialDevice.FromIdAsync(deviceId);


            if (SerialPort != null)
            {
                SerialPort.ReadTimeout = TimeSpan.FromMilliseconds(100);
                SerialPort.BaudRate = 9600;
            
                //SerialPort.Parity = SerialParity.None;
                //SerialPort.StopBits = SerialStopBitCount.One;
                //SerialPort.DataBits = 8;
                //SerialPort.Handshake = SerialHandshake.None;

                try
                {
                    using (DataReader spdata = new DataReader(SerialPort.InputStream))
                    {

                        Task<UInt32> loadAsyncTask;

                        uint ReadBufferLength = 64;

                        spdata.InputStreamOptions = InputStreamOptions.Partial;

                        loadAsyncTask = spdata.LoadAsync(ReadBufferLength).AsTask();

                        uint bytesRead = await loadAsyncTask;

                        TextBlock3.Text = spdata.ReadString(bytesRead).ToString();
                       
                        int serialdata = Convert.ToInt16(TextBlock3.Text);

                        switch (serialdata)
                        {
                            case 10:

                                Status.Text = "Full";
                                TextBlock3.Text = "...";
                                TextBlock4.Text = "...";
                                Send("#ff0000");
                                break;

                            case 113:
                                Status.Text = "#Error_Sensor_No_Power";
                                TextBlock3.Text = "...";
                                TextBlock4.Text = "...";
                                Send("#000000");
                                break;

                            default:

                                int percentresult = (serialdata * 100) / 160;

                                TextBlock4.Text = percentresult.ToString();

                                Status.Text = "Free";
                                Send("#3caa3c");
                                break;

                        }

                    }

                }

                catch 
                {
                    //TextBlock3.Text = ex.ToString();
                }

            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            timer.Stop();

            StartButton.IsEnabled = true;
            StopButton.IsEnabled = false;
        }

        private async void Ports_Loaded(object sender, RoutedEventArgs e)
        {

            var aqs = SerialDevice.GetDeviceSelector();
            var dis = await DeviceInformation.FindAllAsync(aqs);

            if (dis.Any())
            {
                deviceId = dis.First().Id;
            }

            for (int nt = 0; nt < dis.Count; nt++)
            {
                Ports.Items.Add(dis[nt].Name);

                PortID.Text = dis[nt].Id;
            }

            for (int list = 1; list < 13; list++)
            {
                ContainerList.Items.Add(list);

            }

            StartButton.IsEnabled = false;
            StopButton.IsEnabled = false;

        }

        private void ContainerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StartButton.IsEnabled = true;

        }


        private async void Send(string indicator)
        {
            try
            {
                //pos = await geolocator.GetGeopositionAsync();
                //Latitude_Text.Text = pos.Coordinate.Point.Position.Latitude.ToString();
                //Longtitude_Text.Text = pos.Coordinate.Point.Position.Longitude.ToString();

                DataModel dm = new DataModel
                {
                    IdentName = "Container" + ContainerList.SelectedItem.ToString(),
                    CoordinateX = "76.913581",
                    CoordinateY = "43.251351",
                    VolumeRemain = Convert.ToInt32(TextBlock4.Text),
                    Status = Status.Text,
                    Indicator = indicator
                };
                var clientsJson = JsonConvert.SerializeObject(dm);

                var HttpContent = new StringContent(clientsJson);
                HttpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                await client.PutAsync("http://10.2.10.1/api/container/" + ContainerList.SelectedItem.ToString(), HttpContent);
            }

            catch
            {

            }

        }

      
    }
}
