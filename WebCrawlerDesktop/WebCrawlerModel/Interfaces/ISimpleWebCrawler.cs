using System.Threading.Tasks;

namespace WebCrawlerModel.Interfaces
{
    public interface ISimpleWebCrawler
    {
        Task PerformCrawlingAsync(string url, uint currentDeeplevel);
    }
}