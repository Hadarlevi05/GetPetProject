﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GetPet.BusinessLogic.Model.Filters
{
    public class CityFilter : BaseFilter
    {
        public int? Id { get; set; }

        public string Name { get; set; }
    }
}