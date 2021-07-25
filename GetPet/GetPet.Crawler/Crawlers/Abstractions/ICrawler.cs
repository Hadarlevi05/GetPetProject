using GetPet.BusinessLogic.Model;
using GetPet.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetPet.Crawler.Crawlers.Abstractions
{
    public interface ICrawler
    {
        void Load(string url);

        void Load();

        IList<Pet> Parse();

        void InsertToDB(IList<Pet> input);

        User CreateUser();
    }
}
