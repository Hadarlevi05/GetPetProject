using GetPet.BusinessLogic.Model;
using GetPet.Crawler.Utils;
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

        public override PetDto ParseSingleNode(HtmlNode node)
        {
            string name = ParseName(node);
            var year = ParseAgeInYear(node);
            var gender = ParseGender(node, "data-tag");
            //var description = ParseDescription(node, "title");
            // TODO: in all parsers: AnimalType, Description, SourceWebsite

            var pet = new PetDto
            {
                Name = name,
                Gender = gender,
                AgeInYears = year,
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
    }
}