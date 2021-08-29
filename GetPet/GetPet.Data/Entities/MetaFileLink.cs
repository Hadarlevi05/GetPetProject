using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetPet.Data.Entities
{
    public class MetaFileLink : BaseEntity
    {

        [StringLength(400)]
        public string Path { get; set; }

        [StringLength(100)]
        public string MimeType { get; set; }

        public decimal Size { get; set; }
    }
}