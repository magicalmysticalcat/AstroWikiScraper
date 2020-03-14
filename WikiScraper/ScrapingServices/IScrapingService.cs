using System.Collections.Generic;
using System.Threading.Tasks;
using WikiScraper.Models;

namespace WikiScraper.ScrapingServices
{
    public interface IScrapingService
    {
        Task<IEnumerable<Event>> FetchItems(int amountOfItems);
        Task ProcessPages(int amountOfPages);
    }
}