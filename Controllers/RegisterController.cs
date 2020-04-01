using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Interfaces;

namespace AuthenticationService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private IAuthenticationRepository _registerRepostiry;

        public RegisterController(IAuthenticationRepository registerRepository)
        {
            this._registerRepostiry = registerRepository;
        }

        [HttpPost]
        public bool Post(string username, string password)
        {
            try
            {
                _registerRepostiry.RegisterAccount(username, password);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        [HttpGet]
        public bool Get()
        {
            _registerRepostiry.RegisterAccount("", "");
            return true;
        }
    }

    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public bool Post(string username, string password)
        {
            try
            {
                //logic
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}