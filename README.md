# Astro Wiki Scraper

AstroWikiScraper is a .NET Core console project for scraping data from the astro.com wiki.
The final product could be a json file or a sqlite database, both with normalised fields for the events.

## Configuration
This is a basic appsettings.json. There are two Repository types: JSON or Sqlite. At the moment there is no strategy coded, so you will need to uncomment the JSON configuration (Configuration.cs) and comment the SQLite one.

```json
{
    "ExtractionDetails": {
        "ExtractionFilePath": "./extractedData.json",
        "AstroWikiAPIUrl": "https://www.astro.com/wiki/astro-databank/api.php",
        "AmountOfItemsToExtract": 100
    },
    "ConnectionStrings": {
        "astroEventsDb": "Data Source=./AstroEventsDb.db;"
    }
}
```

## License
[MIT](https://choosealicense.com/licenses/mit/)
