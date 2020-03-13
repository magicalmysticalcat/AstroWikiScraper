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
            var extractionDetails = Configuration.ConfigurationRoot.GetSection("ExtractionDetails");
            var extractionFilePath = extractionDetails["ExtractionFilePath"];
            var amountOfPages = Convert.ToInt32(extractionDetails["AmountOfPagesToExtract"]);
            
            _scrapingService.ProcessPages(extractionFilePath,amountOfPages);
            Console.ReadLine();
        }
    }
}