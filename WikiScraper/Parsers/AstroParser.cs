using System.Linq;
using Microsoft.Extensions.Logging;

namespace WikiScraper.Parsers
{
    public class AstroParser: IParser
    {
        private ILogger _logger;

        public AstroParser(ILogger logger)
        {
            _logger = logger;
        }
        public AstroWikiContent Parse(string content)
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
                            property.SetValue(normalizedContent, SanitizeContent(prop[1]));
                    }
                }
            }
            return normalizedContent;
        }

        private string SanitizeContent(string content) => 
            content.Replace("\n", " ")
                .Replace("}}", "")
                .Trim();
    }
}