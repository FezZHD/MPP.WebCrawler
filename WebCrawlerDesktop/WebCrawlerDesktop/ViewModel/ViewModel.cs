using System;
using System.IO;
using Newtonsoft.Json;
using WebCrawlerDesktop.Commands;
using WebCrawlerDesktop.Model;
using WebCrawlerModel;
using WebCrawlerModel.Types;


namespace WebCrawlerDesktop.ViewModel
{
    internal class ViewModel : BaseViewModel
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


        private string errorMessages;

        public string ErrorMessages
        {
            get { return errorMessages; }
            set
            {
                errorMessages = value;
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


        private string Url { get; set; }
        private uint DeepLevel { get; set; }
        private bool isProgressBarEnabled = false;

        public bool IsProgressBarEnabled
        {
            get { return isProgressBarEnabled; }
            set
            {
                isProgressBarEnabled = value;
                OnPropertyChanged();
            }
        }


        internal ViewModel()
        {
            CrawlingCommand = new CommandClass(async () =>
            {
                ErrorMessages = String.Empty;
                try
                {
                    ReadJson();
                }
                catch (Exception ex)
                {
                    ErrorMessages = ex.Message;
                    return;
                }
                var model = new ModelClass();
                IsEnabled = false;
                IsProgressBarEnabled = true;
                CrawlerResult = await model.Crawl(Url, DeepLevel);
                ErrorMessages = model.BuildLogString();
                IsProgressBarEnabled = false;
                IsEnabled = true;
            });
        }


        private ConfigLoader.ConfigLoader ReadConfig()
        {
            var configReader = new ConfigLoader.ConfigLoader();
            configReader.ReadToken();
            return configReader;
        }



        private void ReadJson()
        {
            ConfigLoader.ConfigLoader loader;
            try
            {
                loader = ReadConfig();
            }
            catch (JsonReaderException)
            {
                throw new JsonException("Error reading json\n");
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("File not found\n");
            }
            try
            {
                DeepLevel = loader.ReadDeepLevel();
            }
            catch (AggregateException)
            {
                throw new AggregateException("Error casting config value, please, be sure, that deep level value is higher than - 1\n");
            }
            Url = loader.ReadUrl();
        }             
    }
}