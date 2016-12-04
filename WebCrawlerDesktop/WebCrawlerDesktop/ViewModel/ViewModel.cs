using System.Diagnostics;
using System.Windows.Input;
using WebCrawlerModel;


namespace WebCrawlerDesktop.ViewModel
{
    internal class ViewModel:BaseViewModel
    {


        public CommandClass CrawlingCommand { get; }
        private bool isEnabled = true;
        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
                OnPropertyChanged();
            }
            
        }


        private CrawlerResultType result;

        public CrawlerResultType CrawlerResult
        {
            get { return result; }
            set
            {
                if (value != null)
                {
                    result = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool isProggressBarEnabled = false;

        public bool IsProggressBarEnabled
        {
            get { return isProggressBarEnabled; }
            set
            {
                isProggressBarEnabled = value;
                OnPropertyChanged();
            }
        }


        

        internal ViewModel()
        {
            CrawlingCommand = new CommandClass(async () =>
            {
                WebCrawlerClass crawlerClass = new WebCrawlerClass(2);
                IsEnabled = false;
                IsProggressBarEnabled = true;
                CrawlerResult = await crawlerClass.PerformCrawlingAsync("https://twitter.com");
                IsProggressBarEnabled = false;
                IsEnabled = true;
            });
        }                
    }
}