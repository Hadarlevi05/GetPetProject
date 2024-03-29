﻿using GetPet.BusinessLogic.Azure;
using GetPet.Crawler.Parsers.Abstractions;
using GetPet.Crawler.Utils;
using GetPet.Data.Entities;
using GetPet.Data.Enums;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GetPet.Crawler.Parsers
{
    public abstract class ParserBase : IParser
    {
        protected readonly AzureBlobHelper _azureBlobHelper;

        public ParserBase(AzureBlobHelper azureBlobHelper)
        {
            _azureBlobHelper = azureBlobHelper;
        }

        public abstract PetSource Source { get; }

        public HtmlDocument Document { get; set; }
        public HtmlDocument Document2 { get; set; }

        public async virtual Task<IList<Pet>> Parse(List<Trait> allTraits, User user, List<AnimalType> animalTypes, HtmlDocument document, DocumentType docType)
        {
            var results = new List<Pet>();

            var nodes = GetNodes(document);

            foreach (var node in nodes)
            {
                var pet = await ParseSingleNode(node, allTraits, animalTypes, docType);

                if (IsValidPetDetails(pet))
                {
                    pet.User = user;
                    results.Add(pet);
                }
            }

            return results;
        }

        public abstract HtmlNodeCollection GetNodes(HtmlDocument document);

        public abstract Task<Pet> ParseSingleNode(HtmlNode node, List<Trait> allTraits, List<AnimalType> animalTypes, DocumentType docType);

        public abstract string ParseName(HtmlNode node, DocumentType docType);

        public abstract DateTime? ParseAgeInYear(HtmlNode node, DocumentType doctype);

        public bool IsValidPetDetails(Pet pet)
        {
            if (pet == null)
            {
                return false;
            }

            var nameRegex = new Regex("^[א-ת ']*$");

            return (nameRegex.IsMatch(pet.Name) &&
                    pet.MetaFileLinks.Count > 0 &&
                    pet.Description != string.Empty);
        }

        public Gender ParseGender(HtmlNode node)
        {
            var gender = node.SelectSingleNode(".//table[starts-with(@class, 'gladtoknow__table')]/tbody/tr[4]/td[2]").InnerText;

            return ParserUtils.ConvertGender(gender);
        }

        public Gender ParseGender(HtmlNode node, string name)
        {
            var gender = node.GetAttributeValue(name, "unknown");

            return ParserUtils.ConvertGender(gender);
        }

        public Gender ParseGender(string description)
        {
            Gender gender = ParserUtils.ConvertGender(description);

            if (gender == Gender.Unknown)
            {
                gender = ParserUtils.ParseGenderByKeyWords(description);
            }

            return gender;
        }

        public virtual AnimalType ParseAnimalType(HtmlNode node, string name, List<AnimalType> animalTypes, DocumentType docType)
        {
            var animalType = node.GetAttributeValue(name, "unknown");

            return ParserUtils.ConvertAnimalType(animalType, animalTypes);
        }

        public virtual string ParseDescription(HtmlNode node, string name)
        {
            return System.Net.WebUtility.HtmlDecode(node.GetAttributeValue("title", ""));
        }

        public virtual string ParseDescription(string description)
        {
            return System.Net.WebUtility.HtmlDecode(description);
        }

        public virtual Dictionary<Trait, TraitOption> ParseTraits(HtmlNode node, string name, List<Trait> allTraits)
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

                            var isTrueFemale = description.Contains(trait.FemaleName) && !description.Contains($"לא {trait.FemaleName}");
                            var isFalseFemale = description.Contains($"לא {trait.FemaleName}");

                            if (isTrue || isTrueFemale)
                            {
                                var yes = trait.TraitOptions.FirstOrDefault(t => t.Option == "כן");
                                results[trait] = yes;
                            }
                            else if (isFalse || isFalseFemale)
                            {
                                var no = trait.TraitOptions.FirstOrDefault(t => t.Option == "לא");
                                results[trait] = no;
                            }
                            break;
                        };
                    case TraitType.Values:
                        {
                            var result = trait.TraitOptions.FirstOrDefault(t => description.Contains(t.Option) || description.Contains(t.FemaleOption));

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

        public virtual Dictionary<Trait, TraitOption> ParseTraits(string description, List<Trait> allTraits)
        {
            var results = new Dictionary<Trait, TraitOption>();
            if (allTraits == null)
                return results;

            foreach (var trait in allTraits)
            {
                switch (trait.TraitType)
                {
                    case TraitType.Boolean:
                        {
                            var isTrue = description.Contains(trait.Name) && !description.Contains($"לא {trait.Name}");
                            var isFalse = description.Contains($"לא {trait.Name}");

                            var isTrueFemale = description.Contains(trait.FemaleName) && !description.Contains($"לא {trait.FemaleName}");
                            var isFalseFemale = description.Contains($"לא {trait.FemaleName}");

                            if (isTrue || isTrueFemale)
                            {
                                var yes = trait.TraitOptions.FirstOrDefault(t => t.Option == "כן");
                                results[trait] = yes;
                            }
                            else if (isFalse || isFalseFemale)
                            {
                                var no = trait.TraitOptions.FirstOrDefault(t => t.Option == "לא");
                                results[trait] = no;
                            }
                            break;
                        };
                    case TraitType.Values:
                        {
                            var result = trait.TraitOptions.FirstOrDefault(t => description.Contains(t.Option) || description.Contains(t.FemaleOption));

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

        public virtual DateTime? ParseAgeInYear(string inputAge)
        {
            int year = 0;
            int month = 0;
            string age = string.Empty;

            var ageByGender = Regex.Match(inputAge, @"(?<=בן)(.*?)(?=[\.\,])");

            if (!ageByGender.Success)
            {
                ageByGender = Regex.Match(inputAge, @"(?<=בת)(.*?)(?=[\.\,])");
            }

            if (ageByGender.Success)
            {
                age = ageByGender.Value;

                //age includes years and months
                if (age.Split(new string[] { " ו" }, StringSplitOptions.None).Length > 1)
                {
                    string[] splitByAnd = age.Split(new[] { " ו" }, 2, StringSplitOptions.None);
                    year = ParserUtils.ConvertYear(splitByAnd[0]);
                    month = ParserUtils.ConvertMonth(splitByAnd[1]);
                }
            }
            else
            {
                age = inputAge;
            }
            
            if (age.Contains("חודש"))
            {
                //age includes only months
                month = ParserUtils.ConvertMonth(age);
            }
            else if (age.Contains("חצי שנה"))
            {
                month = ParserUtils.ConvertMonth("6");
            }
            else if (Regex.IsMatch(inputAge, @"\b\d{4}\b")) 
            {
                //age given in year (e.g. 2017)
                year = ParserUtils.ConvertYear(Regex.Match(inputAge, @"\b\d{4}\b").Value);
            }
            else 
            {
                //age include only years
                year = ParserUtils.ConvertYear(age);
            }

            //in case of unvalid date of birth
            if ((year == 0 && month == 0)  || year > 25)
            {
                return null;
            }

            return DateTime.Now.AddYears(-year).AddMonths(-month).Date;
        }
    }
}