using GetPet.BusinessLogic.Azure;
using GetPet.Data.Entities;
using GetPet.Data.Enums;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetPet.Crawler.Parsers
{
    public class SpcaParser : ParserBase
     {
        public SpcaParser(AzureBlobHelper azureBlobHelper) : base(azureBlobHelper)
        {
        }

        public override PetSource Source => PetSource.Spca;

        public override HtmlNodeCollection GetNodes()
        {
            var items = Document.DocumentNode.SelectNodes("//li[starts-with(@class, 'grid-item')]");
            return items;
        }

        public async override Task<Pet> ParseSingleNode(HtmlNode node, List<Trait> allTraits, List<AnimalType> animalTypes)
        {
            AnimalType animalType = ParseAnimalType(node, "class", animalTypes);
            int animalTypeId = animalType.Id; 

            var allTraitsByAnimalType = allTraits.Where(x => x.AnimalTypeId == animalType.Id).ToList();

            string name = ParseName(node);
            var birthday = ParseAgeInYear(node);
            var gender = ParseGender(node, "data-tag");
            var description = ParseDescription(node);
            var traits = ParseTraits(node, name, allTraitsByAnimalType);
            string sourceLink = node.SelectSingleNode("./a").Attributes["href"].Value;
            var image = node.SelectSingleNode(".//img").GetAttributeValue("src", "");

            var pet = new Pet
            {
                Name = name,
                Gender = gender,
                Birthday = birthday,
                Description = description,
                Source = PetSource.External,
                SourceLink = sourceLink,
                AnimalType = animalType,
                AnimalTypeId = animalType.Id,
            };
            
            var filePath = await _azureBlobHelper.Upload(image);
            pet.MetaFileLinks = new List<MetaFileLink>
            {
                new MetaFileLink
                {
                    Path = filePath,
                    MimeType = image.Substring(image.LastIndexOf(".")),
                    Size = 1000
                }
            };

            pet.PetTraits = new List<PetTrait>();
            foreach (var trait in traits)
            {
                pet.PetTraits.Add(
                    new PetTrait()
                    {
                        Trait = trait.Key,
                        TraitOption = trait.Value,
                    }
                );
            }

            return pet;
        }

        public override string ParseName(HtmlNode node)
        {
            return node.SelectNodes("./a/h2/b").FirstOrDefault().InnerText;
        }

        public override DateTime ParseAgeInYear(HtmlNode node) => ParseAgeInYear(node.GetAttributeValue("data-type", "none"));

        public override string ParseDescription(HtmlNode node, string name = "")
        {
            return System.Net.WebUtility.HtmlDecode(node.SelectNodes("./a/div")[1].InnerText);
        }
    }
}