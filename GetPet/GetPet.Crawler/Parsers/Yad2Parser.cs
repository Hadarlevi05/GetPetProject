﻿//using GetPet.BusinessLogic.Model;
//using GetPet.Data.Enums;
//using HtmlAgilityPack;
//using System.Collections.Generic;

//namespace GetPet.Crawler.Parsers
//{
//    public class Yad2Parser : ParserBase
//    {
//        public override HtmlNodeCollection GetNodes()
//        {
//            var items = Document.DocumentNode.SelectNodes("//div[@class='feeditem table']");

//            return items;
//        }

//        public override PetDto ParseSingleNode(HtmlNode node)
//        {
//            var pet = new PetDto
//            {
//                Name = node.SelectSingleNode("//div[@class='row-1']").InnerText
//            };            
//            return pet;
//        }

//        public override string ParseName(HtmlNode node)
//        {
//            throw new System.NotImplementedException();
//        }

//        public override string ParseAgeInYear(HtmlNode node)
//        {
//            throw new System.NotImplementedException();
//        }

//        public override Gender ParseGender(HtmlNode node)
//        {
//            throw new System.NotImplementedException();
//        }
//    }
//}