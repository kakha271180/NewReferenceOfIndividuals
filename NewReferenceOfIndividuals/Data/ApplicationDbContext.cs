using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewReferenceOfIndividuals.Models;

namespace NewReferenceOfIndividuals.Data
{
    public class ApplicationDbContext: IdentityDbContext
    {
        private readonly DbContextOptions _options;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option)
            : base(option)
        {
            _options = option;
        }

        public DbSet<Person> Persons { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Person>().HasKey(l => l.PeopelId);
            builder.Entity<Person>().Property(l => l.Identificator).IsRequired().HasColumnType("nchar(36)");
            builder.Entity<Person>().Property(l => l.FirstNameGeo).IsRequired().HasColumnType("nvarchar(15)");
            builder.Entity<Person>().Property(l => l.FirstNameEn).IsRequired().HasColumnType("nvarchar(15)");
            builder.Entity<Person>().Property(l => l.LastNameGeo).IsRequired().HasColumnType("nvarchar(30)");
            builder.Entity<Person>().Property(l => l.LastNameEn).IsRequired().HasColumnType("nvarchar(30)");
            builder.Entity<Person>().Property(l => l.PersonalNumber).IsRequired().HasColumnType("nchar(11)");
            builder.Entity<Person>().Property(l => l.BirthDate).IsRequired().HasColumnType("datetime2(7)");
            builder.Entity<Person>().Property(l => l.Addres).IsRequired().HasColumnType("nvarchar(max)");
            builder.Entity<Person>().Property(l => l.Mobile1).HasColumnType("nvarchar(20)");
            builder.Entity<Person>().Property(l => l.Mobile2).HasColumnType("nvarchar(20)");
            builder.Entity<Person>().Property(l => l.Picture).HasColumnType("nvarchar(max)");
            builder.Entity<Person>().Property(l => l.KinshipIdentificator).HasColumnType("nchar(20)");
            builder.Entity<Person>().Property(l => l.WhoIs).HasColumnType("nvarchar(20)");

        }
    }
}
