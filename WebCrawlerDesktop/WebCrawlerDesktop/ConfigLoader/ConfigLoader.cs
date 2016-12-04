using System;
using System.Configuration;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebCrawlerDesktop.ConfigLoader
{
    internal class ConfigLoader
    {

        private readonly string jsonPath;

        private JToken token;

        internal ConfigLoader()
        {
            jsonPath = ConfigurationManager.AppSettings["jsonPath"];
        }


        internal void ReadToken()
        {
            if (!File.Exists(jsonPath))
            {
               throw new FileNotFoundException();
            }
            try
            {
                var json = ReadJsonFromFile();
                token = JObject.Parse(json);
            }
            catch (JsonReaderException)
            {
                throw new JsonReaderException();
            }
        }


        internal string ReadUrl()
        {
            if (token == null)
            {
                throw new NullReferenceException();
            }

            return token["url"].ToObject<string>();
        }


        internal uint ReadDeepLevel()
        {
            if (token == null)
            {
                throw new NullReferenceException();
            }

            return token["deepLevel"].ToObject<uint>();
        }

        private string ReadJsonFromFile()
        {
            string resultString;
            var fileStream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                resultString = streamReader.ReadToEnd();
            }
            return resultString;
        }
    }
}
