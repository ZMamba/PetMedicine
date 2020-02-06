using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Animal_Medicine.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Animal_Medicine.Models;

namespace Animal_Medicine.Data
{
    public static class PetTypeInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using(var context = new AnimalMedicineContext(
                serviceProvider.GetRequiredService <DbContextOptions<AnimalMedicineContext>>()))
            {
                if (context.PetTypes.Any())
                {
                    return;
                }

                await context.AddRangeAsync(
                    new PetType {Title = "Кошка" },
                    new PetType {Title = "Собака" });

                context.SaveChanges();
            }
        }
    }
}
