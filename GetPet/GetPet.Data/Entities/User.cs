using GetPet.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetPet.Data.Entities
{
    public class User : BaseEntity
    {
        [StringLength(400)]
        public string Email { get; set; }
        
        [StringLength(400)]
        public string PasswordHash { get; set; }

        [StringLength(400)]
        public string Name { get; set; }

        public UserType UserType { get; set; }
       
        public bool EmailSubscription { get; set; }

        public DateTime LastLoginDate { get; set; }

        [ForeignKey("Organization")]
        public int? OrganizationId { get; set; }
        public Organization Organization { get; set; }

        [ForeignKey("City")]
        public int CityId { get; set; }
        public virtual City City { get; set; }

        public List<Article> Articles { get; set; }
    }
}