using GetPet.BusinessLogic.Model;
using GetPet.Crawler.Utils;
using GetPet.Data.Enums;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GetPet.Crawler.Parsers
{
    public class RehovotSpaParser: ParserBase
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

        public PetDto ParseSingleNode(HtmlNode node)
        {
            string name = ParseName(node);
            var year = ParseAgeInYear(node);
            var gender = ParseGender(node);
            var description = node.GetAttributeValue("title", "");

            var pet = new PetDto
            {
                Name = name,
                Gender = gender,
                AgeInYears = year,
                Description = description
            };

            return pet;
        }

        public override string ParseName(HtmlNode node)
        {
            var result = node.SelectNodes(".").FirstOrDefault().InnerText;
            return result;
        }

        public override string ParseAgeInYear(HtmlNode node)
        {
            var year = node.GetAttributeValue("title", "0");
            var age = Regex.Match(year, @"(?<=בן)(.*?)(?=\.)");

            if (!age.Success)
            {
                age = Regex.Match(year, @"(?<=בת)(.*?)(?=\.)");
            }

            int y = ParserUtils.ConvertYear(age.Value);

            return y.ToString();
        }

        public override Gender ParseGender(HtmlNode node)
        {
            var gender = node.GetAttributeValue("title", "");

            return ParserUtils.ConvertGender(gender);
        }
    }
}