using GetPet.BusinessLogic.Model;
using GetPet.Data.Entities;
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
        IList<PetDto> Parse(List<Trait> allTraits = null);
        string ParseName(HtmlNode node);
        string ParseAgeInYear(HtmlNode node);
        Gender ParseGender(HtmlNode node, string name);
        Data.Enums.AnimalType ParseAnimalType(HtmlNode node, string name);
        string ParseDescription(HtmlNode node, string name);
        List<Trait> ParseTraits(HtmlNode node, string name, List<Trait> allTraits = null);
    }
}
