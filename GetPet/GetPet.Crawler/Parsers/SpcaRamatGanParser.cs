using GetPet.BusinessLogic.Model;
using GetPet.Crawler.Utils;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace GetPet.Crawler.Parsers
{
    public class SpcaRamatGanParser: ParserBase
    {
        public override IList<PetDto> Parse()
        {
            var results = new List<PetDto>();

            var nodes = GetNodes();

            foreach (var node in nodes)
            {
                results.Add(ParseSingleNode(node));
            }
            return results;
        }

        public HtmlNodeCollection GetNodes()
        {
            var items = Document.DocumentNode.SelectNodes("//li[starts-with(@class, 'grid-item')]");
            return items;
        }

        public PetDto ParseSingleNode(HtmlNode node)
        {
            string name = ParseName(node);
            var year = ParseAgeInYear(node);
            var gender = ParseGender(node);

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

        public override string ParseGender(HtmlNode node)
        {
            var gender = node.GetAttributeValue("data-tag", "none");

            return ParserUtils.ConvertGender(gender);
        }
    }
}

