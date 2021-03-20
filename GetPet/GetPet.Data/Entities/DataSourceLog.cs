using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetPet.Data.Entities
{
    public class DataSourceLog : BaseEntity
    {
        [StringLength(4000)]
        public string LogText { get; set; }

        [ForeignKey("DataSource")]
        public int DataSourceId { get; set; }
        public DataSource DataSource { get; set; }
    }
}