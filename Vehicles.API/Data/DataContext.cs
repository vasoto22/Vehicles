using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vehicles.API.Data.Entities;

namespace Vehicles.API.Data
{
    public class DataContext : IdentityDbContext<User>

    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) //Build a constructor & le colocamos los parametros y los ultimos parametros se los enviamos al contructor padre (base(option))
        {
        }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Detail> Details { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }

        public DbSet<History> Histories { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        //la tablas deben tener una propiedad, como la DbSet y es generico como el DBContextOption
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehiclePhoto> VehiclePhotos { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }

        //colocamos un indice, no nos interesa que queden dos descripciones igualitas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Brand>().HasIndex(x => x.Description).IsUnique();
            modelBuilder.Entity<DocumentType>().HasIndex(x => x.Description).IsUnique();
            modelBuilder.Entity<Procedure>().HasIndex(x => x.Description).IsUnique();
            modelBuilder.Entity<Vehicle>().HasIndex(x => x.Plaque).IsUnique();
            modelBuilder.Entity<VehicleType>().HasIndex(x => x.Description).IsUnique();
            

        }    
    }
}  
   

