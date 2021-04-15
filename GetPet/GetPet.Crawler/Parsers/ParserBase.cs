﻿using GetPet.BusinessLogic.Model;
using GetPet.Crawler.Parsers.Abstractions;
using GetPet.Crawler.Utils;
using GetPet.Data.Enums;
using HtmlAgilityPack;
using System.Collections.Generic;

namespace GetPet.Crawler.Parsers
{
    public abstract class ParserBase : IParser
    {
        public HtmlDocument Document { get; set; }

        public virtual IList<PetDto> Parse()
        {
            var results = new List<PetDto>();

            var nodes = GetNodes();

            foreach (var node in nodes)
            {
                var pet = ParseSingleNode(node);
                pet.UserId = 1; // TODO: Find a better way to assign the default user
                pet.AnimalTypeId = 2;

                results.Add(pet);
            }

            return results;
        }

        public abstract HtmlNodeCollection GetNodes();
        public abstract PetDto ParseSingleNode(HtmlNode node);

        public abstract string ParseName(HtmlNode node);
        public abstract string ParseAgeInYear(HtmlNode node);

        public Gender ParseGender(HtmlNode node, string name)
        {
            var gender = node.GetAttributeValue(name, "unknown");

            return ParserUtils.ConvertGender(gender);
        }

        public AnimalType ParseAnimalType(HtmlNode node, string name)
        {
            var gender = node.GetAttributeValue(name, "unknown");

            return ParserUtils.ConvertAnimalType(gender);
        }

        public virtual string ParseDescription(HtmlNode node, string name)
        {
            return node.GetAttributeValue("title", "");
        }
    }
}