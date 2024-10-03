using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catedra_1.src.Data;
using Catedra_1.src.Interface;
using Catedra_1.src.Models;
using Microsoft.EntityFrameworkCore;

namespace Catedra_1.src.Repocitory
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;
        public UserRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAll()
        {
             return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetByRut(string Rut)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Rut == Rut);
        }

        public async Task<User> Post(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}