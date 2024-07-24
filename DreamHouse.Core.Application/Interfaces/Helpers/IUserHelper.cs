using DreamHouse.Core.Application.Dtos.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamHouse.Core.Application.Interfaces.Helpers
{
    public interface IUserHelper
    {
        public void SetUser(AuthenticationResponse user);
        public AuthenticationResponse? GetUser();
        public void RemoveUser();
        public bool HasUser();
    }
}
