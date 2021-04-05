using GetPet.BusinessLogic.Model;
using GetPet.Crawler.Parsers.Abstractions;
using GetPet.Crawler.Utils;
using GetPet.Data.Enums;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace GetPet.Crawler.Parsers
{
    public abstract class ParserBase : IParser
    {
        public HtmlDocument Document { get; set; }

        public abstract IList<PetDto> Parse();

        public abstract string ParseName(HtmlNode node);
        public abstract string ParseAgeInYear(HtmlNode node);
        public virtual Gender ParseGender(HtmlNode node)
        {
            throw new NotImplementedException();
        }

        public Gender ParseGender(HtmlNode node, string name)
        {
            var gender = node.GetAttributeValue(name, "unknown");

            return ParserUtils.ConvertGender(gender);
        }

        public virtual AnimalType ParseAnimalType(HtmlNode node)
        {
            throw new NotImplementedException();
        }

        public AnimalType ParseAnimalType(HtmlNode node, string name)
        {
            var gender = node.GetAttributeValue(name, "unknown");

            return ParserUtils.ConvertAnimalType(gender);
        }
    }
}