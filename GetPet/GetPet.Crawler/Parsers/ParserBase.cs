using GetPet.BusinessLogic.Model;
using GetPet.Crawler.Parsers.Abstractions;
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
    public abstract class ParserBase : IParser
    {
        public HtmlDocument Document { get; set; }

        public virtual IList<PetDto> Parse(List<Trait> allTraits = null)
        {
            var results = new List<PetDto>();

            var nodes = GetNodes();

            foreach (var node in nodes)
            {
                var pet = ParseSingleNode(node, allTraits);
                pet.UserId = 1; // TODO: Find a better way to assign the default user

                results.Add(pet);
            }

            return results;
        }

        public abstract HtmlNodeCollection GetNodes();
        public abstract PetDto ParseSingleNode(HtmlNode node, List<Trait> allTraits = null);

        public abstract string ParseName(HtmlNode node);
        public abstract DateTime ParseAgeInYear(HtmlNode node);

        public Gender ParseGender(HtmlNode node, string name)
        {
            var gender = node.GetAttributeValue(name, "unknown");

            return ParserUtils.ConvertGender(gender);
        }

        public Data.Enums.AnimalType ParseAnimalType(HtmlNode node, string name)
        {
            var gender = node.GetAttributeValue(name, "unknown");

            return ParserUtils.ConvertAnimalType(gender);
        }

        public virtual string ParseDescription(HtmlNode node, string name)
        {
            return node.GetAttributeValue("title", "");
        }

        public virtual Dictionary<Trait, TraitOption> ParseTraits(HtmlNode node, string name, List<Trait> allTraits = null)
        {
            var results = new Dictionary<Trait, TraitOption>();
            if (allTraits == null)
                return results;

            var description = ParseDescription(node, name);

            foreach (var trait in allTraits)
            {
                switch (trait.TraitType)
                {
                    case TraitType.Boolean:
                        {
                            var isTrue = description.Contains(trait.Name) && !description.Contains($"לא {trait.Name}");
                            var isFalse = description.Contains($"לא {trait.Name}");

                            if (isTrue)
                            {
                                var yes = trait.TraitOptions.FirstOrDefault(t => t.Option == "כן");
                                results[trait] = yes;
                            }
                            else if (isFalse)
                            {
                                var no = trait.TraitOptions.FirstOrDefault(t => t.Option == "לא");
                                results[trait] = no;
                            }
                            break;
                        };
                    case TraitType.Values:
                        {
                            var result = trait.TraitOptions.FirstOrDefault(t => description.Contains(t.Option));

                            if (result != null)
                            {
                                results[trait] = result;
                            }
                            break;
                        };
                }
            }

            return results;
        }

        public virtual DateTime ParseAgeInYear(string inputAge)
        {
            int year = 0;
            int month = 0;

            var ageByGender = Regex.Match(inputAge, @"(?<=בן)(.*?)(?=\.)");

            if (!ageByGender.Success)
            {
                ageByGender = Regex.Match(inputAge, @"(?<=בת)(.*?)(?=\.)");
            }

            string age = (!ageByGender.Success) ? inputAge : ageByGender.Value;

            if (age.Split(new string[] { " ו" }, StringSplitOptions.None).Length > 1)
            {
                string[] splitByAnd = age.Split(new[] { " ו" }, 2, StringSplitOptions.None);
                year = ParserUtils.ConvertYear(splitByAnd[0]);
                month = ParserUtils.ConvertMonth(splitByAnd[1]);
            }
            else
            {
                year = ParserUtils.ConvertYear(age);
            }

            return DateTime.Now.AddYears(-year).AddMonths(-month).Date;
        }
    }
}