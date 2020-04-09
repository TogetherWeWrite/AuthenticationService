using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogin _loginRepository;
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}