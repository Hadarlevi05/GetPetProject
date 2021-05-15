using GetPet.BusinessLogic.Model;
using GetPet.Crawler.Utils;
using GetPet.Data.Entities;
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
            string name = ParseName(node);
            var year = ParseAgeInYear(node);
            var gender = ParseGender(node, "title");
            var description = ParseDescription(node, "title");
            var traits = ParseTraits(node, name, allTraits);

            var imageStyle = node.SelectSingleNode(".//div[@class='av-masonry-image-container']").Attributes["style"].Value;
            var image = new Regex(@"url\((.*)\)").Match(imageStyle).Groups[1].Value;

            var pet = new PetDto
            {
                Name = name,
                Gender = gender,
                AgeInYears = year,
                Description = description,
                Images = new List<string> {
                    image
                },
                Traits = traits.ToDictionary(k => k.Name, v => v.Name),
                TraitDTOs = traits,
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
    }
}