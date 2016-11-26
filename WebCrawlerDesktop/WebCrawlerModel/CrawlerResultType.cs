using System.Collections.Generic;

namespace WebCrawlerModel
{
    public class CrawlerResultType
    {
        public string Url { get; private set; }

        public List<CrawlerResultType> NodeList { get; private set; }

        public CrawlerResultType(string url)
        {
            Url = url;
            NodeList = new List<CrawlerResultType>();
        }
    }
}