using GetPet.BusinessLogic.Model;
using GetPet.Crawler.Parsers.Abstractions;
using HtmlAgilityPack;
using System.Collections.Generic;

namespace GetPet.Crawler.Parsers
{
    public abstract class ParserBase : IParser
    {
        public HtmlDocument Document { get; set; }

        public abstract IList<PetDto> Parse();       
    }
}