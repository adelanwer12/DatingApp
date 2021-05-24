using DataService.Models;

namespace Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}