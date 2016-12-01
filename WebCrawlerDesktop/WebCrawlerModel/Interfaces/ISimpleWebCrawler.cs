using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebCrawlerModel.Interfaces
{
    public interface ISimpleWebCrawler
    {
        Task<CrawlerResultType> PerformCrawlingAsync(string url, uint currentDeeplevel, string fromWhich);
    }
}