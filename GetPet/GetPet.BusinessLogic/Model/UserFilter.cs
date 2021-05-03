using System;
using System.Collections.Generic;
using System.Text;

namespace GetPet.BusinessLogic.Model
{
    public class UserFilter : BaseFilter
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string CityName { get; set; }

        public bool? EmailSubscription { get; set; }
    }
}
