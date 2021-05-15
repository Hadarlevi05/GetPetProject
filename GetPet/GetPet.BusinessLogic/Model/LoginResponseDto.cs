using System;
using System.Collections.Generic;
using System.Text;

namespace GetPet.BusinessLogic.Model
{
    public class LoginResponseDto
    {
        public string Token { get; set; }

        public UserDto User { get; set; }

    }
}
