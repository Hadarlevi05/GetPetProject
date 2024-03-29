﻿using System;
using System.Text.Json.Serialization;

namespace GetPet.BusinessLogic.Model
{
    public class UserDto : BaseDto
    {       
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public string Name { get; set; }

        public int CityId { get; set; }

        public string CityName { get; set; }

        public bool EmailSubscription { get; set; }

        public DateTime LastLoginDate { get; set; }

        public int? OrganizationId { get; set; }

        public OrganizationDto Organization { get; set; }
    }
}