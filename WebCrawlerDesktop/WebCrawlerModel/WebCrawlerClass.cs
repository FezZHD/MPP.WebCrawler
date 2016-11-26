using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
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


        public async Task PerformCrawlingAsync(string url, uint currentDeepLevel = 0)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response;
                try
                {
                    response = await client.GetAsync(url);
                }
                catch (WebException webException)
                {
                    return;
                }

                if (response.IsSuccessStatusCode)
                {
                }
            }
        }
    }
}
