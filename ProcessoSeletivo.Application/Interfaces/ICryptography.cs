using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessoSeletivo.Application.Interfaces
{
    public interface ICryptography
    {
        string GenerateHash(string valor);
    }
}
