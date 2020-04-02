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
        private IRegister _registerRepostiry;

        public RegisterController(IRegister registerRepository)
        {
            this._registerRepostiry = registerRepository;
        }

        [HttpPost]
        public async Task<ActionResult<View.Account>> Post(View.Account account)
        {
            try
            {
                if (_registerRepostiry.RegisterAccount(account.username, account.password))
                {
                    return Ok("Your account has succesfully been registered.");
                }
                else
                {
                    return BadRequest("Something went wrong, please try again at a later date.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILogin _loginRepository;
        public LoginController(ILogin _loginRepository)
        {
            this._loginRepository = _loginRepository;
        }
        [HttpPost]
        public async Task<ActionResult<View.Account>> Post(View.Account account)
        {
            try
            {
                if(_loginRepository.Login(account.username, account.password))
                {
                    return Ok("You have succesfully logged in"); //TODO @Stijn: Make sure a session token will be added in the future.
                }
                else
                {
                    return BadRequest("Your username or password was not correct");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<View.SessionToken>> Get(View.SessionToken sessionToken)
        {
            return Ok();
        }
    }
}