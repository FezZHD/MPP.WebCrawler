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


        private WebCrawlerClass crawlerClass = new WebCrawlerClass(2);

        internal ViewModel()
        {
            CrawlingCommand = new CommandClass(async () =>
            { 
                IsEnabled = false;
                IsProggressBarEnabled = true;
                var result = await crawlerClass.PerformCrawlingAsync("https://twitter.com");
                IsProggressBarEnabled = false;
                IsEnabled = true;
            });
        }
        
        
                
    }
}