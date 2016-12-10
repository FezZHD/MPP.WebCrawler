using System;
using System.Collections.Generic;
using System.Text;

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
            var nextStringSymbol = isMultiString ? "\n" : String.Empty;
            var stringBuilder = new StringBuilder();
            foreach (var exception in exceptionList)
            {
                stringBuilder.Append($"{exception.Message}{nextStringSymbol}");
            }
            return stringBuilder.ToString();
        }
    }
}