using System.Collections;
using System.Collections.Generic;
using WikiScraper.Models;

namespace WikiScraper.Repositories
{
    public interface IRepository
    {
        void Save(Event item);
        void Save(IEnumerable<Event> items);
    }
}