using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WikiScraper.Models;

namespace WikiScraper.Repositories
{
    public class JsonRepository:IRepository
    {
        private readonly string _filePath;
        private ILogger _logger;

        public JsonRepository(ILogger logger, string filePath)
        {
            _logger = logger;
            _filePath = filePath;
        }

        public void Save(Event item)
        {
            throw new System.NotImplementedException();
        }

        public void Save(IEnumerable<Event> items)
        {
            if (_filePath != null) 
                File.WriteAllText(_filePath, JsonConvert.SerializeObject(items));
        }
    }
}