using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GetPet.BusinessLogic.Model
{
    public class BaseFilter
    {
        [DefaultValue(1)]
        public int Page { get; set; } = 1;

        [Range(1, 100)]
        [DefaultValue(20)]
        public int PerPage { get; set; } = 20;


    }
}
