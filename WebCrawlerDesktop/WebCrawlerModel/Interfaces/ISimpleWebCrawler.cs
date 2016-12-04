using System.Threading.Tasks;
using WebCrawlerModel.Types;

namespace WebCrawlerModel.Interfaces
{
    public interface ISimpleWebCrawler
    {
        Task<CrawlerResultType> PerformCrawlingAsync(string url, uint currentDeeplevel, string fromWhich);
    }
}