using KiwiSports.Common;
using KiwiSports.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace KiwiSports
{
    /// <summary>
    /// A page that displays wiki data onto the page for the respective sport selected
    /// </summary>
    public sealed partial class Wiki : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private static Uri wikiUri = new Uri("https://45e1532c55ac016eb9af76b404750cd2a94b1b48-www.googledrive.com/host/0B3iPDNpwGU-tMC1SLWdIT1JuLTQ/WikiData.json");

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public Wiki()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="Common.NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            try
            {
                progressBar.Visibility = Visibility.Visible;
                HttpClient client = new HttpClient();
                pageTitle.Text = "About team " + e.NavigationParameter.ToString();
                string wikiResult = await client.GetStringAsync(wikiUri);
                if (wikiResult != null)
                {
                    progressBar.Visibility = Visibility.Collapsed;
                    JsonObject jsonObject = JsonObject.Parse(wikiResult);
                    JsonArray jsonArray = jsonObject["Groups"].GetArray();
                    foreach (JsonValue groupValue in jsonArray)
                    {
                        JsonObject groupObject = groupValue.GetObject();
                        if (groupObject["Title"].GetString() == e.NavigationParameter.ToString())
                        {
                            wikiImage.Source = new BitmapImage(new Uri("" + groupObject["ImagePath"].GetString()));                            
                            wikiDescription.Text = groupObject["Description"].GetString();
                            WikiReadMore.NavigateUri = new Uri(groupObject["Subtitle"].GetString());
                        }
                    }
                }
                else
                {
                    progressBar.Visibility = Visibility.Collapsed;
                    generateErrorHandler("Well, this is embarrassing", "We happened to encounter a minor error while we were working. Apologies!");
                }
            }
            catch(Exception)
            {
                generateErrorHandler("Well, this is embarrassing", "We happened to encounter a minor error while we were working. Apologies!");
            }

        }
        /// <summary>
        /// Displays an error message with title and content accpeted from calling method
        /// </summary>
        /// <param name="_content">
        /// Contnent to be displayed in the error message box
        /// </param>
        /// <param name="_title">
        /// Title of the error message box
        /// </param>
        private async void generateErrorHandler(string _title, string _content)
        {
            MessageDialog errorMessage = new MessageDialog(_content, _title);
            errorMessage.Commands.Add(new UICommand("Back", new UICommandInvokedHandler(commandhandler)));
            await errorMessage.ShowAsync();
        }
        /// <summary>
        /// Handles the event fired by the error message dialog box buttons
        /// </summary>
        /// <param name="command">
        /// Passes information about the trigger that generated this event
        /// </param>
        private void commandhandler(IUICommand command)
        {
            NavigationHelper.GoBack();
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="Common.NavigationHelper.LoadState"/>
        /// and <see cref="Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

    }
}
