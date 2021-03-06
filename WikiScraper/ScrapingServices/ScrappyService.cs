using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WikiClientLibrary.Client;
using WikiClientLibrary.Generators;
using WikiClientLibrary.Pages;
using WikiClientLibrary.Pages.Queries;
using WikiClientLibrary.Sites;
using WikiScraper.Models;
using WikiScraper.Parsers;
using WikiScraper.Repositories;

namespace WikiScraper.ScrapingServices
{
    public class ScrappyService : IScrapingService
    {
        private readonly IRepository _repository;
        private readonly IParser _parser;
        private readonly ILogger _logger;
        private readonly string _astroWikiUrl;

        public ScrappyService(IRepository repository,
            IParser parser,
            ILogger logger,
            string astroWikiUrl)
        {
            _repository = repository;
            _parser = parser;
            _logger = logger;
            _astroWikiUrl = astroWikiUrl;
        }

        public async Task ProcessPages(int amountOfPages)
        {
            var items = await FetchItems(amountOfPages);
            _repository.Save(items);
        } 
        
        public async Task<IEnumerable<Event>> FetchItems(int amountOfItems)
        {
            try
            {
                var client = new WikiClient();
                var wikiSite = new WikiSite(client,_astroWikiUrl);
                
                await wikiSite.Initialization;

                var allPages = new AllPagesGenerator(wikiSite);
                
                var provider = WikiPageQueryProvider.FromOptions(PageQueryOptions.FetchContent);
                var pages = await allPages.EnumPagesAsync(provider).Take(amountOfItems).ToList();

                return pages.Select(page => 
                    Configuration.Mapper.Map<Event>(_parser.Parse(page.Content))).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"something went wrong while fetching!");
                throw;
            }
        }
    }
}