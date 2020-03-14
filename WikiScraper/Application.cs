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
            _scrapingService.ProcessPages(50);
            Console.ReadLine();
        }
    }
}