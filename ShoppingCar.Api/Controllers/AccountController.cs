using BussinesLogic.Interface;
using Microsoft.AspNetCore.Mvc;
using ShoppingCar.Common.Filter;

namespace ShoppingCar.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        readonly IAccount accountService;
        public AccountController(IAccount accountService)
        {
            this.accountService = accountService;
        }
        [HttpPost]
        public IActionResult Login([FromBody] Account account) {
         
            try
            {
                var result = accountService.VerifyUser(account.Password, account.Email);
                return Ok(new { code=result});
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }

        }

    }
}
