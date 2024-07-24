using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamHouse.Core.Application.Dtos.Account
{
    public class ForgotPasswordRequest
    {
        public string? Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
