using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;


namespace KiwiSports.YouTube
{
    /// <summary>
    /// This consists of the actual logic to fetch data from youtube
    /// </summary>
    class GetYouTubeData
    {
        public Uri videoThumbnail320 { get; set; }
        public Uri videoThumbnail480 { get; set; }
        public Uri redirectURL { get; set; }
        public string videoTitle { get; set; }
        public string videoID { get; set; }
        public string videoDesc { get; set; }
        public string channelId { get; set; }
        /// <summary>
        /// Fetches data from youtube and returns a list object containing granular youtube data
        /// </summary>
        /// <param name="teamName">
        /// The team name i.e All Blacks Silver Ferns or Black Caps is fed as an argument here
        /// </param>
        public async static Task<List<GetYouTubeData>> GetYouTubeVideos(string teamName)
        {
            HttpClient client = new HttpClient();
            string youtubeApiKey = "API KEY";
            string baseUrl = "";
            if (teamName == "All Blacks")
            {
                baseUrl = "https://www.googleapis.com/youtube/v3/search?key="+youtubeApiKey+"&channelId=UCsAPiUMyBjtKamxYGbSUnLA&part=snippet,id&order=date&maxResults=30";
            }
            else if (teamName == "Silver Ferns")
            {
                baseUrl = "https://www.googleapis.com/youtube/v3/search?key="+youtubeApiKey+"&channelId=UCBfq7h4L1kKY6yHp8-e1ZRA&part=snippet,id&order=date&maxResults=30";
            }
            else
            {
                baseUrl = "https://www.googleapis.com/youtube/v3/search?key="+youtubeApiKey+"&channelId=UCuKowzPg2-A0FcmyPa3yWug&part=snippet,id&order=date&maxResults=30";
            }
            string videoResult = await client.GetStringAsync(baseUrl);
            RootObject apiData =JsonConvert.DeserializeObject<RootObject>(videoResult);
            List<GetYouTubeData> videos = new List<GetYouTubeData>();
            List<Item> itemObject = new List<Item>();
            try
            {
                if (apiData.pageInfo.totalResults > 0)
                {
                    foreach (var videoObject in apiData.items)
                    {
                        GetYouTubeData container = new GetYouTubeData();
                        container.videoThumbnail320 = new Uri("" + videoObject.snippet.thumbnails.medium.url);
                        container.videoThumbnail480 = new Uri("" + videoObject.snippet.thumbnails.high.url);
                        container.videoTitle = "" + videoObject.snippet.title;
                        container.videoID = "" + videoObject.id.videoId;
                        container.channelId = "" + videoObject.snippet.channelTitle;
                        container.videoDesc = "" + videoObject.snippet.description;
                        container.redirectURL = new Uri("https://www.youtube.com/watch?v=" + videoObject.id.videoId);
                        videos.Add(container);
                    }
                }
            }
            catch (Exception e)
            {
            }
            return videos;
            // End of GetYouTubeVideos method.
        }
    }
}
