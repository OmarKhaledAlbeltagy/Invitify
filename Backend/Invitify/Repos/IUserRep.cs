using Invitify.Models;

namespace Invitify.Repos
{
    public interface IUserRep
    {
        Task<dynamic> Login(LoginModel obj);
    }
}
