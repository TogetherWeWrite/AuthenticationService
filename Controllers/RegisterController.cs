using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Interfaces;
using AuthenticationService.Models;
using Microsoft.AspNetCore.Authorization;

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
                return Ok(_loginRepository.Login(account.username, account.password));   
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// This method will be used by other services that need to validate a token.
        /// </summary>
        /// <param name="account">account model which needs to hold the username and the token</param>
        /// <returns>boolean which states if the token is correct or not.</returns>
        [HttpGet]
        public async Task<ActionResult<bool>> Get(View.Account account)
        {
            try
            {
                return Ok(_loginRepository.ValidateToken(account.username, account.token));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class authenticate : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("yeet");
        }
    }


}