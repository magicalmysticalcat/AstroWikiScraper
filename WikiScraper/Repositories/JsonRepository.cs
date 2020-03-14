using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using WikiScraper.Models;

namespace WikiScraper.Repositories
{
    public class JsonRepository:IRepository
    {
        private readonly string _filePath;
        public JsonRepository(string filePath) => _filePath = filePath;
        
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