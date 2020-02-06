using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Animal_Medicine.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Animal_Medicine.Data
{
    public class AnimalMedicineContext : IdentityDbContext<User>
    {
        public AnimalMedicineContext(DbContextOptions<AnimalMedicineContext> options) : base(options)
        {
        }

        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PetType> PetTypes { get; set; }
    }
}
