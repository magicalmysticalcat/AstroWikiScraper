using System;
using WikiScraper.ScrapingServices;

namespace WikiScraper
{
    public class Application
    {
        private readonly IScrapingService _scrapingService;

        public Application(IScrapingService scrapingService)
        {
            _scrapingService = scrapingService;
        }

        public void Run()
        {
            var numberOfItemsToExtract = Convert.ToInt32(Configuration
                .ConfigurationRoot
                .GetSection("ExtractionDetails")["AmountOfItemsToExtract"]);
            _scrapingService.ProcessPages(numberOfItemsToExtract);
            Console.ReadLine();
        }
    }
}