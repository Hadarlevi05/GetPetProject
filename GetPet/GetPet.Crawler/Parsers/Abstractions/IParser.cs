using GetPet.BusinessLogic.Model;
using GetPet.Data.Enums;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetPet.Crawler.Parsers.Abstractions
{
    public interface IParser
    {
        HtmlDocument Document { get; set; }

        IList<PetDto> Parse();

        string ParseName(HtmlNode node);
        string ParseAgeInYear(HtmlNode node);
        Gender ParseGender(HtmlNode node);
        Gender ParseGender(HtmlNode node, string name);
        AnimalType ParseAnimalType(HtmlNode node);
        AnimalType ParseAnimalType(HtmlNode node, string name);
    }
}
