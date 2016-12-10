using System.Threading.Tasks;
using WebCrawlerModel;
using WebCrawlerModel.Types;

namespace WebCrawlerDesktop.Model
{
    internal class ModelClass
    {

        internal async Task<CrawlerResultType> Crawl(string nodeUrl, uint deepLevel, Logger logger)
        {
            var crawlerClass = new WebCrawlerClass(deepLevel, logger);
            return await crawlerClass.PerformCrawlingAsync(nodeUrl);
        }

    }
}