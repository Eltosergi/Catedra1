using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catedra_1.src.Dtos
{
    public class UpdateUserRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string Rut { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty ;
        public string Gender { get; set; } = string.Empty;
        public string BirthDay { get; set; } = string.Empty;
    }
}