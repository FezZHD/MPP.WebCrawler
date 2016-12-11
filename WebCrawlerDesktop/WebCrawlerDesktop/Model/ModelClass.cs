using System.Threading.Tasks;
using WebCrawlerModel;
using WebCrawlerModel.Types;

namespace WebCrawlerDesktop.Model
{
    internal class ModelClass
    {

        private Logger Log { get; set; }

        internal ModelClass()
        {
            Log = new Logger();
        }

        internal async Task<CrawlerResultType> Crawl(string nodeUrl, uint deepLevel)
        {
            var crawlerClass = new WebCrawlerClass(deepLevel, Log);
            return await crawlerClass.PerformCrawlingAsync(nodeUrl);
        }


        internal string BuildLogString()
        {
            return Log.PrintExceptions(true);
        }

    }
}