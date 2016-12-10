﻿using System;
using System.IO;
using Newtonsoft.Json;
using WebCrawlerDesktop.Commands;
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
                ConfigLoader.ConfigLoader loader;
                uint deepLevel;
                try
                {
                    loader = ReadConfig();
                }
                catch (JsonReaderException)
                {
                    ErrorMessages = "Error to read Json config file\n";
                    return;
                }
                catch (FileNotFoundException)
                {
                    ErrorMessages = "Cannot fine config file\n";
                    return;
                }
                try
                {
                    deepLevel = loader.ReadDeepLevel();
                }
                catch (AggregateException)
                {
                    ErrorMessages = "Error casting config value, please, be sure , that deep level value is higher than -1\n";
                    return;
                }
                var logger = new Logger();
                var crawlerClass = new WebCrawlerClass(deepLevel, logger);
                IsEnabled = false;
                IsProgressBarEnabled = true;
                CrawlerResult = await crawlerClass.PerformCrawlingAsync(loader.ReadUrl());
                IsProgressBarEnabled = false;
                ErrorMessages = logger.PrintExceptions(true);
                IsEnabled = true;
            });
        }


        private ConfigLoader.ConfigLoader ReadConfig()
        {
            var configReader = new ConfigLoader.ConfigLoader();
            configReader.ReadToken();
            return configReader;
        }                
    }
}