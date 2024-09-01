using ProcessoSeletivo.Domain.Enums;
using ProcessoSeletivo.Domain.Models;

namespace ProcessoSeletivo.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
