using System;
using Autofac;
using WikiScraper.ScrapingServices;

namespace WikiScraper
{
    public class Program
    {
        static void Main(string[] args)
        {
            Configuration.CreateMapper();
            Configuration.CompositionRoot().Resolve<Application>().Run();
        }
    }
}