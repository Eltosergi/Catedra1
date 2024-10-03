using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catedra_1.src.Dtos;
using Catedra_1.src.Models;

namespace Catedra_1.src.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(this User productModel){
            return new UserDto{
                Name = productModel.Name,
                BirthDay = productModel.BirthDay,
                Email = productModel.Email,
                Gender = productModel.Gender,
                Rut = productModel.Rut,
            };
        }

        public static User ToUserFromCreateDto(this CreateUserRquestDto createUserRquestDto){
            return new User{
                Name = createUserRquestDto.Name,
                BirthDay = createUserRquestDto.BirthDay,
                Rut = createUserRquestDto.Rut,
                Email = createUserRquestDto.Email,
                Gender = createUserRquestDto.Gender,

            }; 
        }
    }
}