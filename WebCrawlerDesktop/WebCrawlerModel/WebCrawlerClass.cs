using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using WebCrawlerModel.Interfaces;

namespace WebCrawlerModel
{
    public class WebCrawlerClass : ISimpleWebCrawler
    {

        private uint deepLevel;

        private uint DeepLevel
        {
            get { return deepLevel; }
            set
            {
                if (value > 6)
                {
                    value = 6;
                }
                deepLevel = value;
            }
        }


        public WebCrawlerClass(uint deepLevel)
        {
            DeepLevel = deepLevel;
        }


        public async Task<CrawlerResultType> PerformCrawlingAsync(string url, uint currentDeepLevel = 0)
        {
            var result = new CrawlerResultType(url);

            using (var client = new HttpClient())
            {
                HttpResponseMessage response;
                try
                {
                    response = await client.GetAsync(url);
                }
                catch (WebException webException)
                {
                    return result;
                }
                if (response.IsSuccessStatusCode)
                {
                    if (currentDeepLevel <= DeepLevel)
                    {
                        var nodeUrls = GetUrls(await response.Content.ReadAsStringAsync());
                        foreach (var link in nodeUrls)
                        {
                            result.NodeList.Add(await PerformCrawlingAsync(link, currentDeepLevel + 1));
                        }
                    }
                }
            }
            return result;
        }


        private IEnumerable<string> GetUrls(string content)
        {
            HtmlDocument currentDocument = new HtmlDocument();
            currentDocument.LoadHtml(content);
            List<string> nodeUrls = new List<string>();

            foreach (var link in currentDocument.DocumentNode.SelectNodes("//a[@href]"))
            {
                var htmlAttribute = link.Attributes["href"];
                nodeUrls.Add(htmlAttribute.Value);
            }
            return nodeUrls;
        }
    }
}
