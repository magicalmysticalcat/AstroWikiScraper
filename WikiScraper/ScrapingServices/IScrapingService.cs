using System.Collections.Generic;
using System.Threading.Tasks;

namespace WikiScraper.ScrapingServices
{
    public interface IScrapingService
    {
        Task<IEnumerable<AstroWikiContent>> FetchItems(int amountOfItems);
        void DumpData(string filePath, IEnumerable<AstroWikiContent> content);
        AstroWikiContent ParseContent(string content);
        Task ProcessPages(string dumpingFilePath, int amountOfPages);
    }
}