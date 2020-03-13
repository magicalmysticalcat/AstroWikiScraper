using System.IO;
using Autofac;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using WikiScraper.ScrapingServices;

namespace WikiScraper
{
    public class Configuration
    {
        public static IContainer CompositionRoot()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>();
            builder.RegisterType<ScrappyService>().As<IScrapingService>();
            return builder.Build();
        }

        public static IMapper Mapper { get; private set; }
        public static void CreateMapper()
        {
            var config = new MapperConfiguration(cfg => {
                
            });
            Mapper = config.CreateMapper();
        }

        public static IConfigurationRoot ConfigurationRoot;
        public static void LoadAppSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json");

            ConfigurationRoot = builder.Build();
        }
    }
}