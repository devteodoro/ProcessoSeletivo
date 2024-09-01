using ProcessoSeletivo.Application.DTO;
using ProcessoSeletivo.Application.Interfaces;
using ProcessoSeletivo.Domain.Interfaces;
using ProcessoSeletivo.Domain.Models;

namespace ProcessoSeletivo.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            if(id <= 0)
                throw new ArgumentException("O id do usuário é inválido!");

            User user = await _userRepository.GetById(id);

            if (user == null)
                throw new Exception("Usuário não encontrado!");

            return new UserDTO(user.Id, user.Name, user.Role);
        }

        public async Task<List<UserDTO>> ListUsers()
        {
            List<User> users = await _userRepository.List();
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

        public async Task<UserDTO> AddUser(UserDTO userDTO)
        {
            User response = await _userRepository.Create(new User(userDTO.Name, userDTO.Password, userDTO.Role));
            return new UserDTO(response.Id, response.Name, response.Role);
        }

        public async Task<UserDTO> UpdateUser(UserDTO userDTO)
        {
            User user = await _userRepository.GetById(userDTO.Id);

            if (user == null)
                throw new Exception("Usuário não encontrado!");

            user.Name = userDTO.Name;
            user.Password = userDTO.Password;
            user.Role = userDTO.Role;

            await _userRepository.Update(user);
            return userDTO;
        }

        public async Task<UserDTO> DeleteUser(int Id)
        {
            if (Id <= 0)
                throw new Exception("Id de usuário inválido!");

            User user = await _userRepository.GetById(Id);

            if (user == null)
                throw new Exception("Usuário não encontrado!");

            await _userRepository.Delete(user);
            return new UserDTO(user.Id, user.Name, user.Role);
        }
    }
}
