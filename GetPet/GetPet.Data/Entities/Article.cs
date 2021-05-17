using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GetPet.Data.Entities
{
    public class Article : BaseEntity
    {

        public string Title { get; set; }
        public string Content { get; set; }


        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("MetaFileLink")]
        public int? MetaFileLinkId { get; set; }
        public MetaFileLink MetaFileLink { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
