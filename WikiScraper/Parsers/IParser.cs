namespace WikiScraper.Parsers
{
    public interface IParser
    {
        AstroWikiContent Parse(string content);
    }
}