using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetPet.BusinessLogic.Model.Filters
{
    public class MetaFileLinkFilter : BaseFilter
    {
        public int Id { get; set; }

        public int? PetId { get; set; }

        public string? Path { get; set; }

        public string? MimeType { get; set; }

    }
}
