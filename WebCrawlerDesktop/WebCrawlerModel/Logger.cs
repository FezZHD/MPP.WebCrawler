using System;
using System.Collections.Generic;

namespace WebCrawlerModel
{
    public class Logger:ILogger
    {

        public Logger()
        {
            exceptionList = new List<Exception>();
        }


        private readonly List<Exception> exceptionList;

        public void AddException(Exception exception)
        {
            exceptionList.Add(exception);
        }

        public string PrintExceptions(bool isMultiString)
        {
            string returnString = string.Empty;
            var nextStringSymbol = isMultiString ? "\n" : String.Empty;
            foreach (var exception in exceptionList)
            {
                returnString += $"{exception.Message}{nextStringSymbol}";
            }
            return returnString;
        }
    }
}