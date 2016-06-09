using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KiwiSports.Instagram
{
    class GetInstagramData
    {
        public Uri image320 { get; set; }
        public string imageCaption { get; set; }
        public string imageUsername { get; set; }
        public int imageLikeCount { get; set; }
        public Uri imageWebUrl { get; set; }
        public string imageId { get; set; }
        public static List<String> allBlackTagNames = new List<String>();
        public static List<String> blackCapsTagNames = new List<String>();
        public static List<String> silverFernsTagNames = new List<String>();
        public static string nextid;
        public async static Task<List<GetInstagramData>> GetInstagramImages(string teamName)
        {
            HttpClient client = new HttpClient();
            string imagesApiKey = "API KEY";
            allBlackTagNames.Add("teamallblacks");
            allBlackTagNames.Add("backingblack");
            blackCapsTagNames.Add("blackcapsnz");
            blackCapsTagNames.Add("backtheblackcaps");
            silverFernsTagNames.Add("silverfans");
            silverFernsTagNames.Add("silverfernsnz");
            List<GetInstagramData> imagesData = new List<GetInstagramData>();
            string baseUrl;
            for (int i = 0; i < 2; i++)
            {
                if (teamName == "All Blacks")
                {
                    baseUrl = "https://api.instagram.com/v1/tags/"+allBlackTagNames[i]+"/media/recent?client_id="+imagesApiKey;
                }
                else if (teamName == "Silver Ferns")
                {
                    baseUrl = "https://api.instagram.com/v1/tags/" + silverFernsTagNames[i] + "/media/recent?client_id=" + imagesApiKey;
                }
                else
                {
                    baseUrl = "https://api.instagram.com/v1/tags/"+blackCapsTagNames[i]+"/media/recent?client_id="+imagesApiKey;
                }              
                string imagesResult = await client.GetStringAsync(baseUrl);
                RootObject apiData = JsonConvert.DeserializeObject<RootObject>(imagesResult);
                if (apiData.meta.code == 200)
                {
                    nextid = apiData.pagination.next_url;
                    foreach (var imageObject in apiData.data)
                    {
                        GetInstagramData container = new GetInstagramData();
                        container.image320 = new Uri("" + imageObject.images.standard_resolution.url);
                        container.imageCaption = imageObject.caption.text;
                        container.imageWebUrl = new Uri("" + imageObject.link);
                        container.imageLikeCount = imageObject.likes.count;
                        container.imageUsername = imageObject.user.username;
                        container.imageId = imageObject.id;
                        imagesData.Add(container);
                    }
                }
                // End of for loop
            }   
            return imagesData;
            // End of method
        }
        // End of class
    }
}
