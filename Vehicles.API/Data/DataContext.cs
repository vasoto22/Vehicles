using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vehicles.API.Data.Entities;

namespace Vehicles.API.Data
{
    public class DataContext : DbContext

    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) //Build a constructor & le colocamos los parametros y los ultimos parametros se los enviamos al contructor padre (base(option))
        {
        }

        //la tablas deben tener una propiedad, como la DbSet y es generico como el DBContextOption
        public DbSet<VehicleType> VehicleTypes { get; set; }

        //colocamos un indice, no nos interesa que queden dos descripciones igualitas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<VehicleType>().HasIndex(x => x.Description).IsUnique();

        }    
    }
}  
   

