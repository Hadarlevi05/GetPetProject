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
            var items = document.DocumentNode.SelectNodes("//div[@class='feeditem table']");

            return items;
        }

        public PetDto ParseSingleNode(HtmlNode node)
        {
            var pet = new PetDto();

            var text = node.SelectSingleNode("//div[@class='row-1']").InnerText;

            return pet;
        }
    }
}