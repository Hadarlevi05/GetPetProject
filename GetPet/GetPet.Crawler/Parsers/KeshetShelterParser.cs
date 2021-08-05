using GetPet.BusinessLogic.Model;
using GetPet.Crawler.Utils;
using GetPet.Data.Entities;
using GetPet.Data.Enums;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GetPet.Crawler.Parsers
{
    public class KeshetShelterParser : ParserBase
    {
        public override HtmlNodeCollection GetNodes()
        {
            try
            {
                var items = Document.DocumentNode.SelectNodes("//ul[starts-with(@class, 'product')]/li");
                return items;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot parse nodes", ex);
            }

            return null;
        }

        public override Pet ParseSingleNode(HtmlNode node, List<Trait> allTraits, List<AnimalType> animalTypes)
        {

            AnimalType animalType = ParseAnimalType(node, "class", animalTypes);

            var allTraitsByAnimalType = allTraits.Where(x => x.AnimalTypeId == animalType.Id).ToList();

            //pet details is in a seperate page
            HtmlNode detailsNode = GetDetailsNode(node);

            string name = ParseName(detailsNode);
            var description = ParseDescription(detailsNode);
            var birthday = ParseAgeInYear(detailsNode);
            var gender = ParseGender(description);
            var traits = ParseTraits(description, allTraitsByAnimalType);
            var images = ParseImages(detailsNode);
            var sourceLink = "https://keshet-shelter.co.il/statuses/%d7%9c%d7%90%d7%99%d7%9e%d7%95%d7%a5/";


            var pet = new Pet
            {
                Name = name,
                Gender = gender,
                Birthday = birthday,
                Description = description,
                Source = PetSource.External,
                SourceLink = sourceLink,
                AnimalType = animalType,
            };

            pet.MetaFileLinks = new List<MetaFileLink>();
            foreach (var image in images)
            {
                pet.MetaFileLinks.Add(
                    new MetaFileLink
                    {
                        Path = image,
                        MimeType = image.Substring(image.LastIndexOf(".")),
                        Size = 1000
                    }
                );      
            }

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

        public IList<string> ParseImages(HtmlNode detailsNode)
        {
            IList<string> imagesSrcList = new List<string>();
            Console.WriteLine(detailsNode);
            var imagesNodes = detailsNode.SelectNodes("//div[contains(@class, 'woocommerce-product-gallery--with-images')]/figure/div/a/img");

            foreach(var imgNode in imagesNodes)
            {
                imagesSrcList.Add(imgNode.Attributes["src"].Value);
            }

            return imagesSrcList;
        }

        public override DateTime ParseAgeInYear(HtmlNode node) => ParseAgeInYear(ParseDescription(node));

        public override string ParseName(HtmlNode node)
        {
            var result = node.SelectSingleNode("//h1[starts-with(@class, 'product_title')]").InnerText;

            return result;
        }

        public override AnimalType ParseAnimalType(HtmlNode node, string name, List<AnimalType> animalTypes)
        {
            // Keshet shelter deals only with cats
            var animalType = "חתול";

            return ParserUtils.ConvertAnimalType(animalType, animalTypes);
        }

        public Gender ParseGender(string gender)
        {
            return ParserUtils.ConvertGender(gender);
        }

        public string ParseDescription(HtmlNode detailsNode)
        {
            //each line of pet description is in a different div
            string description = "";

            var descNodes = detailsNode.SelectNodes("//div[starts-with(@class, 'o9v6fnle') and count(div) > 3]/div[@dir]");
            if (descNodes == null)
            {
                descNodes = detailsNode.SelectNodes("//div[@data-editor]/div/span");
            }

            if (descNodes == null)
            {
                return "חתול חששן בן שנה צבע לבן"; //no description - problem with bisli!
            }

            
            foreach (HtmlNode node in descNodes)
            {
                description += node.InnerText;
                description += " ";
            }

            return description;
        }

        public HtmlNode GetDetailsNode(HtmlNode node)
        {
            HtmlDocument detailsDoc = new HtmlDocument();
            
            try
            {
                var detailsUrl = ParseDetailsURL(node);
                HtmlWeb web = new HtmlWeb();
                detailsDoc = web.Load(detailsUrl);

                //details node
                var htmlNode = detailsDoc.DocumentNode.SelectSingleNode("(.//div[starts-with(@class, 'elementor-row')])[3]");

                return htmlNode;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot load details page", ex);
            }

            return null;
        }

        public string ParseDetailsURL(HtmlNode node)
        {
            var url = node.SelectSingleNode("./a").Attributes["href"].Value;

            return url;
        }

    }
}
