using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace KiwiSports.NYTimes
{
    class GetNewsData
    {
        public Uri newsThumbnail320 { get; set; }
        public string newsLeadParragraph { get; set; }
        public string newsHeadline { get; set; }
        public Uri newsUrl { get; set; }
        public string newsId { get; set; }
        public string newsSource { get; set; }

        public async static Task<List<GetNewsData>> GetNewsFeeds(string teamName)
        {
            HttpClient client = new HttpClient();
            List<GetNewsData> images = new List<GetNewsData>();
            string newsApiKey = "API KEY";
            string baseUrl;
            for(int i = 1; i <= 2; i++)
            {            
                if (teamName == "All Blacks")
                {
                   baseUrl = "http://api.nytimes.com/svc/search/v2/articlesearch.json?q=%22all+blacks%22&page="+i+"&sort=newest&api-key="+newsApiKey;                   
                }
                else if (teamName == "Silver Ferns")
                {
                    baseUrl = "http://api.nytimes.com/svc/search/v2/articlesearch.json?q=%22all+blacks%22&sort=newest&api-key="+newsApiKey;
                }
                else
                {
                    if(i==1)
                    {
                        baseUrl = "http://api.nytimes.com/svc/search/v2/articlesearch.json?q=%22Black%2BCaps%22&fq=Sports&sort=newest&api-key="+newsApiKey;
                    }
                    else
                    {
                        break;
                    }                
                }

                string newsResult = await client.GetStringAsync(baseUrl);
                RootObject apiData = JsonConvert.DeserializeObject<RootObject>(newsResult);

                if (apiData.status == "OK" && apiData.response.docs.Count > 0)
                {
                    foreach (var newsObject in apiData.response.docs)
                    {
                        GetNewsData container = new GetNewsData();
                        container.newsLeadParragraph = newsObject.lead_paragraph;
                        container.newsHeadline = newsObject.headline.main;
                        container.newsUrl = new Uri(""+ newsObject.web_url);
                        container.newsSource = newsObject.source;
                        container.newsId = newsObject._id;
                        if (newsObject.multimedia.Count != 0)
                        {
                            container.newsThumbnail320 = new Uri("http://www.nytimes.com/" + newsObject.multimedia[0].url);
                        }
                        else
                        {
                                if(teamName=="All Blacks")
                                    container.newsThumbnail320 = new Uri("ms-appx:///Assets/AllBlacksTile.jpg");
                                else
                                   container.newsThumbnail320 = new Uri("ms-appx:///Assets/BlackCapsTile.jpg");
                        }                    
                        images.Add(container);
                    }                
                }
                // End of for loop
            }
            return images;
            // End of method.
        }
        // End of class
    }
}
