using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Services.Interfaces;
using AuthenticationService.View;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountservice _accountservice;
        public AccountController(IAccountservice accountservice)
        {
            this._accountservice = accountservice;
        }

        [HttpGet]
        public async Task<ActionResult<ViewAccount>> Get(Guid id)
        {
            try
            {
                return Ok(await _accountservice.GetAccount(id));
            }
            catch (Exception ex)
            {
                return BadRequest("Getting Account with Id: "+id+" was not succesful, Exception: " +ex.Message);
            }
        }

        [HttpGet]
        [Route("id")]
        public async Task<ActionResult<int>> GetIdFromUserName(string username)
        {
            try
            {
                return Ok(await _accountservice.GetId(username));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}