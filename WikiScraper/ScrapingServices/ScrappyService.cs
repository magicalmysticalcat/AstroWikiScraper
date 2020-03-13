using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WikiClientLibrary.Client;
using WikiClientLibrary.Generators;
using WikiClientLibrary.Pages;
using WikiClientLibrary.Pages.Queries;
using WikiClientLibrary.Sites;

namespace WikiScraper.ScrapingServices
{
    public class ScrappyService : IScrapingService
    {
        private const string _astroWikiUrl = "https://www.astro.com/wiki/astro-databank/api.php";

        public async Task ProcessPages(string dumpingFilePath, int amountOfPages)
        {
            var items = await FetchItems(amountOfPages);
            DumpData(dumpingFilePath, items);
        } 
        
        public async Task<IEnumerable<AstroWikiContent>> FetchItems(int amountOfItems)
        {
            try
            {
                var normalizedPages = new List<AstroWikiContent>();
                var client = new WikiClient();
                var wikiSite = new WikiSite(client,_astroWikiUrl);
                
                await wikiSite.Initialization;

                var allPages = new AllPagesGenerator(wikiSite);

                var provider = WikiPageQueryProvider.FromOptions(PageQueryOptions.FetchContent);
                var pages = await allPages.EnumPagesAsync(provider).Take(amountOfItems).ToList();
                
                foreach (var page in pages)
                {
                    var normalizedContent = ParseContent(page.Content);
                    normalizedPages.Add(normalizedContent);
                }

                return normalizedPages;
            }
            catch (Exception ex)
            {
                throw new Exception("Something when wrong while fetching!", ex);
            }
        }

        public void DumpData(string filePath, IEnumerable<AstroWikiContent> content) => 
            File.WriteAllText(filePath, JsonConvert.SerializeObject(content));

        public AstroWikiContent ParseContent(string content)
        {
            var normalizedContent = new AstroWikiContent();
            var normalizedContentProps = normalizedContent.GetType().GetProperties();
            var splitProperties = content.Split('|');
            if (splitProperties != null)
            {
                foreach (var propStringified in splitProperties)
                {
                    var prop = propStringified.Split('=');
                    if (prop != null)
                    {
                        var property =
                            normalizedContentProps.FirstOrDefault(p => p.Name.ToLower() == prop[0].ToLower());
                        if (property != null) 
                            property.SetValue(normalizedContent, prop[1]);
                    }
                }
            }
            return normalizedContent;
        }
    }
}