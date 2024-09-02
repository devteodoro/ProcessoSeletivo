using ProcessoSeletivo.Application.DTO;
using ProcessoSeletivo.Application.Interfaces;
using ProcessoSeletivo.Domain.Interfaces;
using ProcessoSeletivo.Domain.Models;
using System.Text.RegularExpressions;

namespace ProcessoSeletivo.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly ICryptography _cryptographyService;

        public UserService(IUserRepository userRepository, ITokenService tokenService, ICryptography cryptographyService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _cryptographyService = cryptographyService;
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
            if (userDTO.Name.Length <= 5)
                throw new Exception("O nome deve conter pelo menos 5 caracteres!");

            if (string.IsNullOrEmpty(userDTO.Password))
                throw new Exception("A senha não pode ser vazia!");

            if (userDTO.Password.Length < 8 || userDTO.Password.Length > 12)
                throw new Exception("A senha deve conter entre 8 e 12 caracteres!");

            if (!Regex.IsMatch(userDTO.Password, @"^[a-zA-Z0-9]+$"))
                throw new Exception("A senha deve ser alfanumérica!");

            if (!Regex.IsMatch(userDTO.Password, @"[a-zA-Z]") || !Regex.IsMatch(userDTO.Password, @"\d"))
                throw new Exception("A senha deve conter letras e números!");

            string passwordHash = _cryptographyService.GenerateHash(userDTO.Password);

            User response = await _userRepository.CreateAsync(new User(userDTO.Name, passwordHash, userDTO.Role));
            return new UserDTO(response.Id, response.Name, response.Role);
        }

        public async Task<UserDTO> UpdateUserAsync(UserDTO userDTO)
        {
            User user = await _userRepository.GetByIdAsync(userDTO.Id);

            if (user == null)
                throw new Exception("Usuário não encontrado!");

            user.Name = userDTO.Name;
            user.Password = _cryptographyService.GenerateHash(userDTO.Password);
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

            string passwordHash = _cryptographyService.GenerateHash(userDTO.Password);

            if (passwordHash != user.Password)
                throw new Exception("Usuário ou senha incorretos!");

            string token = _tokenService.GenerateToken(user);
            return token;
        }
    }
}
