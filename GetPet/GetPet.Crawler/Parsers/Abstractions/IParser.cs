﻿using GetPet.BusinessLogic.Model;
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
        IList<Pet> Parse(List<Trait> allTraits, User user, List<Data.Entities.AnimalType> animalTypes);
        string ParseName(HtmlNode node);
        DateTime ParseAgeInYear(HtmlNode node);
        Gender ParseGender(HtmlNode node, string name);
        AnimalType ParseAnimalType(HtmlNode node, string name, List<AnimalType> animalTypes);
        string ParseDescription(HtmlNode node, string name);
        Dictionary<Trait, TraitOption> ParseTraits(HtmlNode node, string name, List<Trait> allTraits = null);
    }
}
