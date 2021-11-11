using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApplication1.Data;

namespace WebApplication1.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUpViewModel model);
        Task<string> LoginAsync(SignInViewModel model);
    }
}
