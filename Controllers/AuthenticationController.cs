using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        
        [HttpPost]
        public bool Register(string username, string password)
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