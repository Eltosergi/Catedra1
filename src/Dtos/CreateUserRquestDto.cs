using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catedra_1.src.Dtos
{
    public class CreateUserRquestDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Rut { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty ;
        [Required]
        public string Gender { get; set; } = string.Empty;
        [Required]
        public string BirthDay { get; set; } = string.Empty;
    }
}