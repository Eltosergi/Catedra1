using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catedra_1.src.Models;
using Microsoft.EntityFrameworkCore;

namespace Catedra_1.src.Data
{
    public class ApplicationDBContext(DbContextOptions dbContextOptions): DbContext(dbContextOptions)
    {
        public DbSet<User> Users { get; set; } = null;
    }
}