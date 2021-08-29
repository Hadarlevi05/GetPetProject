using System;
using System.Collections.Generic;
using System.Text;

namespace GetPet.BusinessLogic.Model.Filters
{
    public class OrganizationFilter : BaseFilter
    {
        public string Name { get; set; }

        public string Email { get; set; }
    }
}
