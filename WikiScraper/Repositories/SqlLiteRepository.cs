using System.Collections.Generic;
using WikiScraper.DTOs;

namespace WikiScraper.Repositories
{
    public class SqlLiteRepository:IRepository
    {
        private readonly string _connectionString;
        public SqlLiteRepository(string connectionString) => _connectionString = connectionString;
        
        public void Save(NormalisedAstroWikiContentDto item)
        {
            throw new System.NotImplementedException();
        }

        public void Save(IEnumerable<NormalisedAstroWikiContentDto> items)
        {
            throw new System.NotImplementedException();
        }
    }
}