﻿using GetPet.BusinessLogic.Model;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetPet.Crawler.Parsers.Abstractions
{
    public interface IParser
    {
        HtmlDocument document { get; set; }

        IList<PetDto> Parse();
    }
}
