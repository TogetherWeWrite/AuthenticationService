using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Interfaces;
using AuthenticationService.Models;
using Microsoft.AspNetCore.Authorization;
using AuthenticationService.Services.Interfaces;

namespace AuthenticationService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegister _registerRepostiry;

        public RegisterController(IRegister registerRepository)
        {
            this._registerRepostiry = registerRepository;
        }

        [HttpPost]
        public async Task<ActionResult<View.ViewUser>> Post(View.ViewUser user)
        {
            try
            {
                if (_registerRepostiry.RegisterAccount(user.Username, user.Password))
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
}