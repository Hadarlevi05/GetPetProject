using System;
using System.Collections.Generic;
using System.Text;

namespace GetPet.BusinessLogic.Model
{
    public class BaseDto
    {
        public int Id { get; set; }

        public DateTime UpdatedTimestamp { get; set; }
        public DateTime CreationTimestamp { get; set; }
    }
}
