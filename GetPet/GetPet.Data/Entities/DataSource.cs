using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GetPet.Data.Entities
{
    public class DataSource : BaseEntity
    {
        [StringLength(400)]
        public int Name { get; set; }
        [StringLength(1000)]
        public int Url { get; set; }
        public int CronCrawlTime { get; set; }
    }
}
