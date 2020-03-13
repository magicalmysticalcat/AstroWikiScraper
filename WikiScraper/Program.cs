using System;
using Autofac;
using WikiScraper.ScrapingServices;

namespace WikiScraper
{
    static class Program
    {
        static void Main(string[] args)
        {
            Configuration.LoadAppSettings();
            Configuration.CompositionRoot().Resolve<Application>().Run();
            Configuration.CreateMapper();
        }
    }
}