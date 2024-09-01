using ProcessoSeletivo.Application.DTO;

namespace ProcessoSeletivo.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllUsersAsync();

        Task<UserDTO> GetUserByIdAsync(int Id);

        Task<UserDTO> AddUserAsync(UserDTO Id);

        Task<UserDTO> UpdateUserAsync(UserDTO Id);

        Task<UserDTO> DeleteUserAsync(int Id);

        Task<string> AuthenticateAsync(UserDTO userDTO);
    }
}
