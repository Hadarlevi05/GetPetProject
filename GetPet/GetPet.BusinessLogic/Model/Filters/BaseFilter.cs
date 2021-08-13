using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GetPet.BusinessLogic.Model.Filters
{
    public class BaseFilter
    {
        [DefaultValue(1)]
        public int Page { get; set; } = 1;

        [Range(1, 1000)]
        [DefaultValue(100)]
        public int PerPage { get; set; } = 100;


    }
}
