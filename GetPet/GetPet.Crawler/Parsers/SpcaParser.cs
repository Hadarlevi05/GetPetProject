﻿using GetPet.BusinessLogic.Model;
using GetPet.Data.Entities;
using GetPet.Data.Enums;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GetPet.Crawler.Parsers
{  
     public class SpcaParser : ParserBase
     {
        public override HtmlNodeCollection GetNodes()
        {
            var items = Document.DocumentNode.SelectNodes("//li[starts-with(@class, 'grid-item')]");
            return items;
        }

        public override PetDto ParseSingleNode(HtmlNode node, List<Trait> allTraits = null)
        {
            int animalTypeId = 3; // Todo: Get Dog or cat from API or other way
            var allTraitsByAnimalType = allTraits.Where(x => x.AnimalTypeId == animalTypeId).ToList();

            string name = ParseName(node);
            var birthday = ParseAgeInYear(node);
            var gender = ParseGender(node, "data-tag");
            var description = ParseDescription(node);
            var traits = ParseTraits(node, name, allTraitsByAnimalType);
            string sourceLink = node.SelectSingleNode("./a").Attributes["href"].Value;

            var pet = new PetDto
            {
                Name = name,
                Gender = gender,
                Birthday = birthday,
                Description = description,
                Traits = traits.ToDictionary(k => k.Name, v => v.Name),
                TraitDTOs = traits,
                Source = PetSource.External,
                SourceLink = sourceLink,
                AnimalTypeId = animalTypeId,
            };

            return pet;
        }

        public override string ParseName(HtmlNode node)
        {
            return node.SelectNodes("./a/h2/b").FirstOrDefault().InnerText;
        }

        public override DateTime ParseAgeInYear(HtmlNode node) => ParseAgeInYear(node.GetAttributeValue("data-type", "none"));

        public override string ParseDescription(HtmlNode node, string name = "")
        {
            return node.InnerText;
        }
    }
}