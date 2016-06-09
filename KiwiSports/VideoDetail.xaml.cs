using KiwiSports.Common;
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
using KiwiSports.YouTube;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Popups;

namespace KiwiSports
{
    /// <summary>
    /// A page that displays each selected video information in detail
    /// </summary>
    public sealed partial class VideoDetail : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

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

        public VideoDetail()
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
            progressBar.Visibility = Visibility.Visible;
            try {
                string navparam = "" + e.NavigationParameter;
                string[] rnavparam = navparam.Split('&');
                List<GetYouTubeData> youtubeData = new List<GetYouTubeData>();
                List<VideoDetail> videoDetails = new List<VideoDetail>();
                youtubeData = await GetYouTubeData.GetYouTubeVideos(rnavparam[1].ToString());
                if (youtubeData != null)
                {
                    progressBar.Visibility = Visibility.Collapsed;
                    foreach (var videoObject in youtubeData)
                    {
                        if (videoObject.videoID == rnavparam[0].ToString())
                        {
                            pageTitle.Text = videoObject.videoTitle;
                            videoTitle.Text = videoObject.videoTitle;
                            videoDescription.Text = videoObject.videoDesc + "\nPosted by @"+ videoObject.channelId;
                            watchVideo.NavigateUri = videoObject.redirectURL;
                            videoImage.Source = new BitmapImage(new Uri("" + videoObject.videoThumbnail480));
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    progressBar.Visibility = Visibility.Collapsed;
                    generateErrorHandler("Well, this is embarrassing", "We happened to encounter a minor error while we were working. Apologies!");
                }
            }
            catch (Exception){
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
