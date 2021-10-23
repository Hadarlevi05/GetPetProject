using GetPet.BusinessLogic.Azure;
using GetPet.Crawler.Utils;
using GetPet.Data.Entities;
using GetPet.Data.Enums;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetPet.Crawler.Parsers
{
    public class RlaParser : ParserBase
    {
        public RlaParser(AzureBlobHelper azureBlobHelper) : base(azureBlobHelper)
        {
        }

        public override PetSource Source => PetSource.Rla;

        public override HtmlNodeCollection GetNodes(HtmlDocument document)
        {
            try
            {
                var items = document.DocumentNode.SelectNodes("//div[starts-with(@class, 'ui-column')]");

                return items;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot parse nodes", ex);
                throw;
            }
        }

        public async override Task<Pet> ParseSingleNode(HtmlNode node, List<Trait> allTraits, List<AnimalType> animalTypes, DocumentType docType)
        {
            string petPage;

            AnimalType animalType = ParseAnimalType(node, "class", animalTypes, docType);
            int animalTypeId = animalType.Id;

            var allTraitsByAnimalType = allTraits.Where(x => x.AnimalTypeId == animalType.Id).ToList();

            //pet details is in a seperate page
            HtmlNode detailsNode = GetDetailsNode(node, out petPage);
            string description = GetDescription(detailsNode, docType);

            if (description == null)
            {
                return null;
            }

            string name = ParseName(detailsNode, docType);
            var birthday = ParseAgeInYear(detailsNode, docType);
            var gender = ParseGender(description);
            var decodedDescription = ParseDescription(description);
            var traits = ParseTraits(description, allTraitsByAnimalType);
            var image = node.SelectSingleNode(".//img[starts-with(@class, 'ui--content-box-image')]").Attributes["src"].Value;
            var sourceLink = petPage;


            var pet = new Pet
            {
                Name = name,
                Gender = gender,
                Birthday = birthday,
                Description = decodedDescription,
                Source = PetSource.External,
                SourceLink = sourceLink,
                AnimalType = animalType,
                AnimalTypeId = animalTypeId,
            };

            var filePath = await _azureBlobHelper.Upload(image);
            pet.MetaFileLinks = new List<MetaFileLink>
            {
                new MetaFileLink
                {
                    Path = filePath,
                    MimeType = image.Substring(image.LastIndexOf(".")),
                    Size = 1000
                }
            };

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

        private string GetDescription(HtmlNode detailsNode, DocumentType docType)
        {
            string description = null;

            if (docType == DocumentType.DOC_CATS)
            {
                description = detailsNode.SelectSingleNode(".//div[starts-with(@class, 'auto-format ui--animation')]/p").InnerText;
            }

            if (docType == DocumentType.DOC_DOGS)
            {
                var descriptionNode = detailsNode.SelectSingleNode(".//h4[@id='custom-title-h4-1']/p") ?? null;
                if (descriptionNode != null)
                { 
                    description = descriptionNode.InnerText;
                }
            }

            return description;
        }

        public override string ParseName(HtmlNode node, DocumentType docType)
        {
            string result = string.Empty;

            if (docType == DocumentType.DOC_CATS) { result = node.SelectSingleNode(".//div[starts-with(@class, 'ui--title-holder')]/h1").InnerText; }
            if (docType == DocumentType.DOC_DOGS) { result = node.SelectSingleNode(".//h2[@id='titlebar-title']").InnerText; }

            return result;
        }

        public override DateTime? ParseAgeInYear(HtmlNode node, DocumentType docType) => ParseAgeInYear(GetDescription(node, docType));

        public override AnimalType ParseAnimalType(HtmlNode node, string name, List<AnimalType> animalTypes, DocumentType docType)
        {
            string animalType = string.Empty;

            if (docType == DocumentType.DOC_CATS) { animalType = "חתול"; }
            if (docType == DocumentType.DOC_DOGS) { animalType = "כלב"; }

            return ParserUtils.ConvertAnimalType(animalType, animalTypes);
        }

        public HtmlNode GetDetailsNode(HtmlNode node, out string _petPage)
        {
            HtmlDocument detailsDoc = new HtmlDocument();
            _petPage = null;

            try
            {
                var petPage = ParseDetailsURL(node);
                _petPage = petPage;
                HtmlWeb web = new HtmlWeb();
                detailsDoc = web.Load(petPage);

                //details node
                var htmlNode = detailsDoc.DocumentNode.SelectSingleNode("(.//div[@id='page-wrap'])");

                return htmlNode;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot load details page", ex);
                throw;
            }
        }

        public string ParseDetailsURL(HtmlNode node)
        {
            var url = node.SelectSingleNode(".//a[starts-with(@class, 'ui--content-box-link')]").Attributes["href"].Value;

            return url;
        }
    }
}
