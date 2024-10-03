using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catedra_1.src.Models;

namespace Catedra_1.src.Interface
{
    public interface IUserRepository
    {
        Task<User?> GetById(int id);       
        Task<User> Post(User user);
    }
}