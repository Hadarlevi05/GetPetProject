using GetPet.BusinessLogic.Azure;
using GetPet.Crawler.Utils;
using GetPet.Data.Entities;
using GetPet.Data.Enums;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GetPet.Crawler.Parsers
{
    public class RehovotSpaParser : ParserBase
    {
        public RehovotSpaParser(AzureBlobHelper azureBlobHelper) : base(azureBlobHelper)
        {
        }

        public override PetSource Source => PetSource.RehovotSpa;

        public override HtmlNodeCollection GetNodes()
        {
            try
            {
                var items = Document.DocumentNode.SelectNodes("//div[starts-with(@class, 'av-masonry-container')]/a");

                return items;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot parse nodes", ex);
            }

            return null;
        }

        public async override Task<Pet> ParseSingleNode(HtmlNode node, List<Trait> allTraits, List<AnimalType> animalTypes)
        {
            AnimalType animalType = ParseAnimalType(node, "class", animalTypes);
            int animalTypeId = animalType.Id;

            var allTraitsByAnimalType = allTraits.Where(x => x.AnimalTypeId == animalType.Id).ToList();

            string name = ParseName(node);
            var birthday = ParseAgeInYear(node);
            var gender = ParseGender(node, "title");
            var description = ParseDescription(node, "title");
            var traits = ParseTraits(node, name, allTraitsByAnimalType);
            var imageStyle = node.SelectSingleNode(".//div[@class='av-masonry-image-container']").Attributes["style"].Value;
            var image = new Regex(@"url\((.*)\)").Match(imageStyle).Groups[1].Value;
            var sourceLink = "http://rehovotspa.org.il/our-dogs/";

            var pet = new Pet
            {
                Name = name,
                Gender = gender,
                Birthday = birthday,
                Description = description,
                Source = PetSource.External,
                SourceLink = sourceLink,
                AnimalType = animalType,
                AnimalTypeId = animalTypeId,
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
            var result = node.SelectNodes(".").FirstOrDefault().InnerText;
            return result;
        }

        public override DateTime ParseAgeInYear(HtmlNode node) => ParseAgeInYear(node.GetAttributeValue("title", "0"));

        public override AnimalType ParseAnimalType(HtmlNode node, string name, List<AnimalType> animalTypes)
        {
            // Rehovot API have 'dogs' stated within the url
            var animalType = "כלב";

            return ParserUtils.ConvertAnimalType(animalType, animalTypes);
        }
    }
}