using System;
using System.Collections.Generic;
using System.Text;

namespace GetPet.BusinessLogic.Model
{
    public class ArticleDto : BaseDto
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public int UserId { get; set; }
        public UserDto User { get; set; }

        public string ImageUrl { get; set; }
        public List<CommentDto> Comments { get; set; }

        public int CommentCount { get; set; }
    }
}
