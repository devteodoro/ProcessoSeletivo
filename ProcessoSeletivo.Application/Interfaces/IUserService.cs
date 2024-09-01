using ProcessoSeletivo.Application.DTO;

namespace ProcessoSeletivo.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDTO>> ListUsers();

        Task<UserDTO> GetUserById(int Id);

        Task<UserDTO> AddUser(UserDTO Id);

        Task<UserDTO> UpdateUser(UserDTO Id);

        Task<UserDTO> DeleteUser(int Id);

        Task<string> Authenticate(UserDTO userDTO);
    }
}
