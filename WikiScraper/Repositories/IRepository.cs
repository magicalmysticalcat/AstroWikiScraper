using System.Collections;
using System.Collections.Generic;
using WikiScraper.DTOs;

namespace WikiScraper.Repositories
{
    public interface IRepository
    {
        void Save(NormalisedAstroWikiContentDto item);
        void Save(IEnumerable<NormalisedAstroWikiContentDto> items);
    }
}