using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Services.Interfaces;


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
        public async Task<ActionResult<View.ViewUser>> Post(View.ViewUser user)
        {
            try
            {
                return Ok(await _loginRepository.Login(user.Username, user.Password));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// This method will be used by other services that need to validate a token.
        /// </summary>
        /// <param name="user">account model which needs to hold the username and the token</param>
        /// <returns>boolean which states if the token is correct or not.</returns>
        [HttpGet]
        public async Task<ActionResult<bool>> Get(View.ViewUser user)
        {
            try
            {
                return Ok(await _loginRepository.ValidateToken(user.Username, user.Token));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}