using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessoSeletivo.Application.DTO
{
    public class PhotoDTO
    {
        public string Image { get; set; } = string.Empty;

        public PhotoDTO(string image)
        {
            Image = image;
        }
    }
}
