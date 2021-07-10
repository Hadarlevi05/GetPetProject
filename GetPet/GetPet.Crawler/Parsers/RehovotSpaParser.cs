using GetPet.BusinessLogic.Model;
using GetPet.Data.Entities;
using GetPet.Data.Enums;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GetPet.Crawler.Parsers
{
    public class RehovotSpaParser : ParserBase
    {
        public override HtmlNodeCollection GetNodes()
        {
            try
            {
                var items = Document.DocumentNode.SelectNodes("//div[starts-with(@class, 'av-masonry-container')]/a");

                return items;
            }
            catch (Exception ex)
            {
                // throw ex;
            }

            return null;
        }

        public override PetDto ParseSingleNode(HtmlNode node, List<Trait> allTraits = null)
        {
            int animalTypeId = 3; // Todo: Get Dog or cat from API or other way
            var allTraitsByAnimalType = allTraits.Where(x => x.AnimalTypeId == animalTypeId).ToList();

            string name = ParseName(node);
            var birthday = ParseAgeInYear(node);
            var gender = ParseGender(node, "title");
            var description = ParseDescription(node, "title");
            var traits = ParseTraits(node, name, allTraitsByAnimalType);
            var imageStyle = node.SelectSingleNode(".//div[@class='av-masonry-image-container']").Attributes["style"].Value;
            var image = new Regex(@"url\((.*)\)").Match(imageStyle).Groups[1].Value;
            var sourceLink = "http://rehovotspa.org.il/our-dogs/";

            var pet = new PetDto
            {
                Name = name,
                Gender = gender,
                Birthday = birthday,
                Description = description,
                Images = new List<string> {
                    image
                },
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
            var result = node.SelectNodes(".").FirstOrDefault().InnerText;
            return result;
        }

        public override DateTime ParseAgeInYear(HtmlNode node) => ParseAgeInYear(node.GetAttributeValue("title", "0"));
    }
}