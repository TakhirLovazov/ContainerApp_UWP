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
using Windows.UI.Popups;
using Newtonsoft.Json;
using System.Net.Http;
using App4;
using Windows.UI.Core;
using System.Text;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace App4
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class WebApi : Page
    {
        MessageDialog msgbox = new MessageDialog("Id is empty");
        Frame Tf = Window.Current.Content as Frame;
        public WebApi()
        {
            this.InitializeComponent();
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
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
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null)
            {
                if (rootFrame.CanGoBack)
                {
                    e.Handled = true;
                    rootFrame.GoBack();
                }
            }
        }

        private async void Getapi_Click(object sender, RoutedEventArgs e)
        {
            if (Webapi_Id.Text == "")
            {

                //await msgbox.ShowAsync();
                ContentDialog noWifiDialog = new ContentDialog
                {
                    Title = "Id is empty",
                    Content = "Id is empty",
                    PrimaryButtonText = "Ok"


                };

                ContentDialogResult result = await noWifiDialog.ShowAsync();

            }
            else
            {
                try
                {
                    HttpClient client = new HttpClient();
                    StringBuilder dv = new StringBuilder("http://10.2.10.1/api/container/");
                    dv.Append(Convert.ToInt16(Webapi_Id.Text));
                    dv.Append("/");
                    dv.Append("91b6a3b39b8e31d69c5a2e14dd244708");
                    var JsonResponse = await client.GetStringAsync(dv.ToString());
                    DataModel dm = JsonConvert.DeserializeObject<DataModel>(JsonResponse);
                  
                    Name.Text = dm.IdentName;
                    CoordinateX.Text = dm.CoordinateX;
                    CoordinateY.Text = dm.CoordinateY;
                    Volume.Text = dm.VolumeRemain.ToString();
                    isFull.Text = dm.Status;
                }

                catch
                {
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = "Warning",
                        Content = "Id is not existing",
                        PrimaryButtonText = "Ok"


                    };

                    ContentDialogResult result = await noWifiDialog.ShowAsync();
                }

            }


            //HttpClient client = new HttpClient();
            //HttpResponseMessage JsonResponse = await client.GetAsync("http://178.88.161.71/api/values/");
            //HttpContent content = JsonResponse.Content;
            //string mycontent = await content.ReadAsStringAsync();
            //Webapi_Text.Text = mycontent;
        }

        private async void Postapi_Click(object sender, RoutedEventArgs e)
        {
            //if (Webapi_Id.Text == "")
            //{

            //    //await msgbox.ShowAsync();
            //    ContentDialog noWifiDialog = new ContentDialog
            //    {
            //        Title = "Id is empty",
            //        Content = "Id is empty",
            //        PrimaryButtonText = "Ok"


            //    };

            //    ContentDialogResult result = await noWifiDialog.ShowAsync();

            //}
            //else
            //{
                try
                {
                    var clients = new DataModel()
                    {
                        // Id = Int16.Parse(Id.Text),
                        IdentName = Name.Text,
                        CoordinateX = CoordinateX.Text,
                        CoordinateY = CoordinateY.Text
                    };
                    var clientsJson = JsonConvert.SerializeObject(clients);
                    HttpClient client = new HttpClient();
                    var HttpContent = new StringContent(clientsJson);
                    HttpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    await client.PostAsync("http://10.2.10.1/api/container/", HttpContent);
                }

                catch
                {
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = "Warning",
                        Content = "Id is not existing",
                        PrimaryButtonText = "Ok"


                    };

                    ContentDialogResult result = await noWifiDialog.ShowAsync();
                }
            //}
        }

        private async void Putapi_Click(object sender, RoutedEventArgs e)
        {
            if (Webapi_Id.Text == "")
            {

                //await msgbox.ShowAsync();
                ContentDialog noWifiDialog = new ContentDialog
                {
                    Title = "Id is empty",
                    Content = "Id is empty",
                    PrimaryButtonText = "Ok"


                };

                ContentDialogResult result = await noWifiDialog.ShowAsync();

            }
            else
            {
                try
                {
                    HttpClient client = new HttpClient();
                    DataModel dm = new DataModel
                    {
                        IdentName = Name.Text,
                        CoordinateX = CoordinateX.Text,
                        CoordinateY = CoordinateY.Text,
                        VolumeRemain = Convert.ToInt16(Volume.Text),
                        Status = isFull.Text
                        
                    };
                    var clientsJson = JsonConvert.SerializeObject(dm);

                    var HttpContent = new StringContent(clientsJson);
                    HttpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    await client.PutAsync("http://10.2.10.1/api/container/" + Webapi_Id.Text, HttpContent);
                }

                catch
                {
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = "Warning",
                        Content = "Id is not existing",
                        PrimaryButtonText = "Ok"


                    };

                    ContentDialogResult result = await noWifiDialog.ShowAsync();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }
}
