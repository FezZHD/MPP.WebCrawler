using System;

namespace WebCrawlerModel
{
    public interface ILogger
    {
        void AddException(Exception exception);
        string PrintExceptions(bool isMultiString);
    }
}