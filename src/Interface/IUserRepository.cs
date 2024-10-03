using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catedra_1.src.Dtos;
using Catedra_1.src.Models;

namespace Catedra_1.src.Interface
{
    public interface IUserRepository
    {
        Task<User?> GetById(int id); 
        Task<User?> GetByRut(string Rut);         
        Task<User> Post(User user);
        Task <List<User>> GetAll(); 

        Task<User?> Delete(int id);
        Task<User?> Put(int id, UpdateUserRequestDto userDto);
    }
}