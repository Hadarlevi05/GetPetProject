using GetPet.Data.Entities;
using GetPet.Data.Enums;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GetPet.Crawler.Parsers.Abstractions
{
    public interface IParser
    {
        PetSource Source { get; }
        HtmlDocument Document { get; set; }
        HtmlDocument Document2 { get; set; }

        Task<IList<Pet>> Parse(List<Trait> allTraits, User user, List<Data.Entities.AnimalType> animalTypes, HtmlDocument document, DocumentType type);
        string ParseName(HtmlNode node, DocumentType docType);
        DateTime? ParseAgeInYear(HtmlNode node, DocumentType docType);
        Gender ParseGender(HtmlNode node, string name);
        AnimalType ParseAnimalType(HtmlNode node, string name, List<AnimalType> animalTypes, DocumentType docType);
        string ParseDescription(HtmlNode node, string name);
        Dictionary<Trait, TraitOption> ParseTraits(HtmlNode node, string name, List<Trait> allTraits = null);
    }
}
