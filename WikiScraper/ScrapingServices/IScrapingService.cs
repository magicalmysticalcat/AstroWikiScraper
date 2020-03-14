using System.Collections.Generic;
using System.Threading.Tasks;
using WikiScraper.DTOs;

namespace WikiScraper.ScrapingServices
{
    public interface IScrapingService
    {
        Task<IEnumerable<NormalisedAstroWikiContentDto>> FetchItems(int amountOfItems);
        Task ProcessPages(int amountOfPages);
    }
}