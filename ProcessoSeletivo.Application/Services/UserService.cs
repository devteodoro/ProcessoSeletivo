using ProcessoSeletivo.Application.DTO;
using ProcessoSeletivo.Application.Interfaces;
using ProcessoSeletivo.Domain.Interfaces;
using ProcessoSeletivo.Domain.Models;

namespace ProcessoSeletivo.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            if(id <= 0)
                throw new ArgumentException("O id do usuário é inválido!");

            User user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                throw new Exception("Usuário não encontrado!");

            return new UserDTO(user.Id, user.Name, user.Role);
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            List<User> users = await _userRepository.GetAllAsync();
            List<UserDTO> listUserDTO = new List<UserDTO>();

            if (users != null && users.Count > 0)
            {
                foreach (User user in users)
                {
                    listUserDTO.Add(new UserDTO(user.Id, user.Name, user.Role));
                }
            }

            return listUserDTO;
        }

        public async Task<UserDTO> AddUserAsync(UserDTO userDTO)
        {
            User response = await _userRepository.CreateAsync(new User(userDTO.Name, userDTO.Password, userDTO.Role));
            return new UserDTO(response.Id, response.Name, response.Role);
        }

        public async Task<UserDTO> UpdateUserAsync(UserDTO userDTO)
        {
            User user = await _userRepository.GetByIdAsync(userDTO.Id);

            if (user == null)
                throw new Exception("Usuário não encontrado!");

            user.Name = userDTO.Name;
            user.Password = userDTO.Password;
            user.Role = userDTO.Role;

            await _userRepository.UpdateAsync(user);
            return userDTO;
        }

        public async Task<UserDTO> DeleteUserAsync(int Id)
        {
            if (Id <= 0)
                throw new Exception("Id de usuário inválido!");

            User user = await _userRepository.GetByIdAsync(Id);

            if (user == null)
                throw new Exception("Usuário não encontrado!");

            await _userRepository.DeleteAsync(user);
            return new UserDTO(user.Id, user.Name, user.Role);
        }

        public async Task<string> AuthenticateAsync(UserDTO userDTO)
        {   
            User user = await _userRepository.GetByNameAsync(userDTO.Name);

            if (user == null)
                throw new Exception("Usuário não encontrado!");

            if(userDTO.Password != user.Password)
                throw new Exception("Usuário ou senha incorretos!");

            string token = _tokenService.GenerateToken(user);
            return token;
        }
    }
}
