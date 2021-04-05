using GetPet.BusinessLogic.Model;
using GetPet.Crawler.Parsers.Abstractions;
using GetPet.Data.Enums;
using HtmlAgilityPack;
using System.Collections.Generic;

namespace GetPet.Crawler.Parsers
{
    public abstract class ParserBase : IParser
    {
        public HtmlDocument Document { get; set; }

        public abstract IList<PetDto> Parse();

        public abstract string ParseName(HtmlNode node);
        public abstract string ParseAgeInYear(HtmlNode node);
        public abstract Gender ParseGender(HtmlNode node);        
    }
}