using System.Windows.Input;

namespace WebCrawlerDesktop.ViewModel
{
    internal class ViewModel:BaseViewModel
    {

        private uint deepLevel;

        internal ViewModel()
        {
            deepLevel = 3;
        }
    }
}