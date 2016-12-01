using System.Diagnostics;
using System.Windows.Input;
using WebCrawlerModel;


namespace WebCrawlerDesktop.ViewModel
{
    internal class ViewModel:BaseViewModel
    {

        private WebCrawlerClass crawlerClass = new WebCrawlerClass(3);

        public CommandClass CrawlingCommand { get; }

        internal ViewModel()
        { 
            CrawlingCommand = new CommandClass(async () =>
            {
                var watch = new Stopwatch();
                watch.Start();
                var result = await crawlerClass.PerformCrawlingAsync("https://twitter.com");
                watch.Stop();
                Debug.WriteLine(watch.Elapsed.Seconds);
            });
        }
        
        
                
    }
}