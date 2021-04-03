using GetPet.BusinessLogic.Model;
using HtmlAgilityPack;
using System.Collections.Generic;

namespace GetPet.Crawler.Parsers
{
    public class Yad2Parser : ParserBase
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
            var items = Document.DocumentNode.SelectNodes("//div[@class='feeditem table']");

            return items;
        }

        public PetDto ParseSingleNode(HtmlNode node)
        {
            var pet = new PetDto
            {
                Name = node.SelectSingleNode("//div[@class='row-1']").InnerText
            };            
            return pet;
        }

        public override string ParseName(HtmlNode node)
        {
            throw new System.NotImplementedException();
        }

        public override string ParseAgeInYear(HtmlNode node)
        {
            throw new System.NotImplementedException();
        }

        public override string ParseGender(HtmlNode node)
        {
            throw new System.NotImplementedException();
        }
    }
}