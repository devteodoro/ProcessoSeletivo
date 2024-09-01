using ProcessoSeletivo.Application.DTO;
using ProcessoSeletivo.Application.Interfaces;
using ProcessoSeletivo.Domain.Interfaces;
using ProcessoSeletivo.Domain.Models;

namespace ProcessoSeletivo.Application.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoRepository _photoRepository;

        public PhotoService(IPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }

        public async Task<PhotoDTO> GetCurrentPhotoByUserId(int UserId)
        {
            if(UserId <= 0)
                throw new ArgumentException("O id do usuário é inválido!");

            Photo photo = await _photoRepository.GetCurrentPhoto(UserId);

            if (photo == null)
                throw new Exception("Nenhuma foto cadastrada!");

            return new PhotoDTO(photo.Image);
        }
    }
}
