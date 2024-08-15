using Service.Repositories.Base;
using Service.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{

    public interface ITokenService
    { 

    }

    public class TokenService : ITokenService
    {

        private readonly string symetricKey = "this is my secret key for authentication iseeCovid@123";
        private readonly string url = "https://iseecovid.vn";
        private readonly IUserService _userService;

        public TokenService(IUserService userService)
        {
            _userService = userService;
        }

    }
}
