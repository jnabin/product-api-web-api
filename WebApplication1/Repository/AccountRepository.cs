using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace WebApplication1.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ProductContext _jobContext;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        public AccountRepository(ProductContext jobContext,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _jobContext = jobContext;
            _signInManager = signInManager;
            _userManager = userManager;
            _config = configuration;
        }

        public async Task<IdentityResult> SignUpAsync(SignUpViewModel model)
        {
            var user = new ApplicationUser
            {
                Name = model.Name,
                Email = model.Email,
                Address = model.Address,
                UserName = model.Email
            };
            return await _userManager.CreateAsync(user, model.Password);
        }

        public async Task<string> LoginAsync(SignInViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Succeeded)
            {
                var authClaim = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                var authSignKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["JWT:Secret"]));
                var token = new JwtSecurityToken(
                    issuer: _config["JWT:ValidIssuer"],
                    audience: _config["JWT:ValidAudience"],
                    expires: DateTime.Now.AddDays(1),
                    claims:authClaim,
                    signingCredentials: new SigningCredentials(authSignKey, SecurityAlgorithms.HmacSha256Signature)

                    );
                return new JwtSecurityTokenHandler().WriteToken(token);

            }
            else
            {
                return null;
            }
        }
    }
}
