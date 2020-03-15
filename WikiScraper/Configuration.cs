using System;
using System.Collections.Generic;
using System.IO;
using Autofac;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WikiScraper.Models;
using WikiScraper.Parsers;
using WikiScraper.Repositories;
using WikiScraper.ScrapingServices;

namespace WikiScraper
{
    public static class Configuration
    {
        public static IContainer CompositionRoot()
        {
            LoadAppSettings();
            
            var builder = new ContainerBuilder();
            
            builder.RegisterType<Application>();
            /*
            var dumpToFilePath = ConfigurationRoot.GetSection("ExtractionDetails")["ExtractionFilePath"];
            builder.RegisterType<JsonRepository>()
                .As<IRepository>()
                .WithParameter(new NamedParameter("filePath", dumpToFilePath));*/
            var connectionString = ConfigurationRoot.GetConnectionString("astroEventsDb");
            builder.RegisterType<SqliteRepository>()
                .As<IRepository>()
                .WithParameter(new NamedParameter("connectionString", connectionString));

            var astroWikiUrl = ConfigurationRoot.GetSection("ExtractionDetails")["AstroWikiAPIUrl"];
            builder.RegisterType<ScrappyService>()
                .As<IScrapingService>()
                .WithParameter(new NamedParameter("astroWikiUrl", astroWikiUrl));
            
            var loggerFactory = LoggerFactory.Create(b =>
            {
                b.AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("WikiScraper.Program", LogLevel.Debug)
                    .AddConsole();
            });
            var logger = loggerFactory.CreateLogger<Program>();
            builder.RegisterInstance(logger).As<ILogger>();

            builder.RegisterType<AstroParser>().As<IParser>();
            
            return builder.Build();
        }

        public static IMapper Mapper { get; private set; }
        public static void CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AstroWikiContent, Event>()
                    .ForMember(dest => dest.AstroId, opt => opt.MapFrom(src => src.DatamainID))
                    .ForMember(dest => dest.FriendlyName, opt => opt.MapFrom(src => src.sflname))
                    .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.sbdate))
                    .ForMember(dest => dest.FriendlyDate, opt => opt.MapFrom(src => src.sbdate_dmy))
                    .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.sbtime))
                    .ForMember(dest => dest.TimeAMPM, opt => opt.MapFrom(src => src.sbtime_ampm))
                    .ForMember(dest => dest.Place, opt => opt.MapFrom(src => src.Place))
                    .ForMember(dest => dest.BirthCountry, opt => opt.MapFrom(src => src.BirthCountry))
                    .ForMember(dest => dest.PlaceAndCountryCode, opt => opt.MapFrom(src => src.sctr))
                    .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.slati))
                    .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.slong))
                    .ForMember(dest => dest.TimeZoneAbbreviation, opt => opt.MapFrom(src => src.TmZnAbbr))
                    .ForMember(dest => dest.TimeType, opt => opt.MapFrom(src => src.ctimetype))
                    .ForMember(dest => dest.Calendar, opt => opt.MapFrom(src => src.ccalendar))
                    .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.iyear))
                    .ForMember(dest => dest.Month, opt => opt.MapFrom(src => src.imonth))
                    .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.iday))
                    .ForMember(dest => dest.DataSourceCode, opt => opt.MapFrom(src => src.DataSourceCode))
                    .ForMember(dest => dest.FriendlyDataSource, opt => opt.MapFrom(src => src.sdatasource))
                    .ForMember(dest => dest.RoddenRatingCode, opt => opt.MapFrom(src => src.RoddenRatingCode))
                    .ForMember(dest => dest.FriendlyRoddenRating, opt => opt.MapFrom(src => src.sroddenrating))
                    .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                    .ForMember(dest => dest.FriendlyGender, opt => opt.MapFrom(src => src.csex))
                    .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                    .ForMember(dest => dest.BirthName, opt => opt.MapFrom(src => src.BirthName))
                    .ForMember(dest => dest.SunSign, opt => opt.MapFrom(src => src.sun_sign))
                    .ForMember(dest => dest.SunDegMin, opt => opt.MapFrom(src => src.sun_degmin))
                    .ForMember(dest => dest.MoonSign, opt => opt.MapFrom(src => src.moon_sign))
                    .ForMember(dest => dest.MoonDegMin, opt => opt.MapFrom(src => src.moon_degmin))
                    .ForMember(dest => dest.AscSign, opt => opt.MapFrom(src => src.asc_sign))
                    .ForMember(dest => dest.AscDegMin, opt => opt.MapFrom(src => src.asc_degmin));
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