using GetPet.BusinessLogic.Model;
using GetPet.Crawler.Utils;
using GetPet.Data.Entities;
using GetPet.Data.Enums;
using HtmlAgilityPack;
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
            string name = ParseName(node);
            var year = ParseAgeInYear(node);
            var gender = ParseGender(node, "data-tag");
            var description = ParseDescription(node);
            var traits = ParseTraits(node, name, allTraits);

            // TODO: in all parsers: AnimalType, SourceWebsite, Images

            var pet = new PetDto
            {
                Name = name,
                Gender = gender,
                AgeInYears = year,
                Description = description,
                Traits = traits.ToDictionary(k => k.Name, v => v.Name),
                TraitDTOs = traits,
            };

            return pet;
        }

        public override string ParseName(HtmlNode node)
        {
            return node.SelectNodes("./a/h2/b").FirstOrDefault().InnerText;
        }

        public override string ParseAgeInYear(HtmlNode node)
        {
            var year = node.GetAttributeValue("data-type", "none");

            // int y = ParserUtils.ConvertYear(year.Split(" ")[0]);

            return year;
        }

        public override string ParseDescription(HtmlNode node, string name = "")
        {
            return node.InnerText;
        }
    }
}