using ProcessoSeletivo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessoSeletivo.Domain.Interfaces
{
    public interface IPhotoRepository
    {
        Task<List<Photo>> GetPhotosByUserId(int UserId);

        Task<Photo> GetCurrentPhoto(int UserId);

        Task<Photo> Create(Photo photo);

        Task<Photo> Update(Photo photo);
    }
}
