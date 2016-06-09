using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiwiSports.NYTimes
{
    class NYTimes
    {
    }
    public class Meta
    {
        public int hits { get; set; }
        public int time { get; set; }
        public int offset { get; set; }
    }

    public class Headline
    {
        public string main { get; set; }
        public string print_headline { get; set; }
    }

    public class Byline
    {
        public List<object> person { get; set; }
        public string original { get; set; }
        public string organization { get; set; }
    }
    public class Legacy
    {
        public string wide { get; set; }
        public string wideheight { get; set; }
        public string widewidth { get; set; }
    }
    public class Multimedia
    {
        public int width { get; set; }
        public int height { get; set; }
        public string url { get; set; }
        public string subtype { get; set; }
        public string type { get; set; }
        public Legacy legacy { get; set; }
    }

    public class Doc
    {
        public string web_url { get; set; }
        public string snippet { get; set; }
        public string lead_paragraph { get; set; }
        public object @abstract { get; set; }
        public object print_page { get; set; }
        public List<object> blog { get; set; }
        public string source { get; set; }
        // public List<object> multimedia { get; set; }
        public List<Multimedia> multimedia { get; set; }
        public Headline headline { get; set; }
        public List<object> keywords { get; set; }
        public string pub_date { get; set; }
        public string document_type { get; set; }
        public string news_desk { get; set; }
        public string section_name { get; set; }
        public string subsection_name { get; set; }
        public Byline byline { get; set; }
        public string type_of_material { get; set; }
        public string _id { get; set; }
        public string word_count { get; set; }
    }

    public class Response
    {
        public Meta meta { get; set; }
        public List<Doc> docs { get; set; }
    }

    public class RootObject
    {
        public Response response { get; set; }
        public string status { get; set; }
        public string copyright { get; set; }
    }
}
