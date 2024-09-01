using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessoSeletivo.Domain.Models
{
    public class ResultModel<T>
    {
        public ResultModel(T data, List<string> errors)
        {
            Data = data;
            Errors = errors;
        }

        public ResultModel(T data)
        {
            Data = data;
        }

        public ResultModel(List<string> errors)
        {
            Errors = errors;
        }

        public ResultModel(string error)
        {
            Errors.Add(error);
        }

        public T Data { get; private set; }

        public List<string> Errors { get; private set; } = new();
    }
}
