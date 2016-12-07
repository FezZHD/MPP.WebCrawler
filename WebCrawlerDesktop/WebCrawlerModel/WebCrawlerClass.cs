using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Parser.Html;
using WebCrawlerModel.Interfaces;
using WebCrawlerModel.Types;

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


        public WebCrawlerClass(uint deepLevel, Logger logger)
        {
            DeepLevel = deepLevel;
            crawlerLogger = logger;
        }


        private readonly Logger crawlerLogger;

        public async Task<CrawlerResultType> PerformCrawlingAsync(string url, uint currentDeepLevel = 0, string fromWhich = null)
        {
            var result = new CrawlerResultType(url);
            Debug.WriteLine($"{url} with {currentDeepLevel} from {fromWhich}");
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(5);
                HttpResponseMessage response;
                try
                {
                    response = await client.GetAsync(url);
                }
                catch (WebException)
                {
                    crawlerLogger.AddException(new WebException($"Cannot crawl {url}, because there is internet connection problems"));
                    return result;
                }
                catch (TaskCanceledException)
                {
                    crawlerLogger.AddException(new WebException($"Http timeout for {url}"));
                    return result;
                }
                catch (HttpRequestException)
                {
                    crawlerLogger.AddException(new HttpRequestException($"Error to send http request for {url}"));
                    return result;
                }
                if (response.IsSuccessStatusCode)
                {
                    if (currentDeepLevel < DeepLevel)
                    {
                        var nodeUrls = GetUrls(await response.Content.ReadAsStringAsync());
                        foreach (var link in nodeUrls)
                        {
                            result.NodeList.Add(await PerformCrawlingAsync(link, currentDeepLevel + 1, url));
                        }
                    }
                }
                else
                {
                    {
                        crawlerLogger.AddException(new HttpRequestException($"{(int)response.StatusCode} code answer for {url}"));
                    }
                }
            }
            return result;
        }


        private IEnumerable<string> GetUrls(string content)
        {
            IEnumerable<string> nodeUrls = new List<string>();

            var parser = new HtmlParser();

            var document = parser.Parse(content);
            nodeUrls = document.QuerySelectorAll("a").Select(x => x.GetAttribute("href")).Where((x) =>
            {
                return (!string.IsNullOrEmpty(x)) && (x.StartsWith("http") || x.StartsWith("https"));
            });
            return nodeUrls;
        }
    }
}
