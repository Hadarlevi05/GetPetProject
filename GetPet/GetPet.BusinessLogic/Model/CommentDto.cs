using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GetPet.BusinessLogic.Model
{
    public class CommentDto : BaseDto
    {
        [Required]
        public string Text { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
        public int CommentCount { get; set; }
    }
}
