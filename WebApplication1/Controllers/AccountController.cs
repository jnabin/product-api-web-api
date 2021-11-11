using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [HttpPost("signup")]
       public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            var result = await _accountRepository.SignUpAsync(model);

            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(SignInViewModel model)
        {
            var result = await _accountRepository.LoginAsync(model);

            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }
            else
            {
                return Ok(new { result });
            }
        }
        
    }
}
