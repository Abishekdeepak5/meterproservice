using Microsoft.EntityFrameworkCore;
using MeterProService.Models;
using System.Collections.Generic;

namespace MeterProService.Data
{
    public class MeterproDbContext : DbContext
    {
        public MeterproDbContext(DbContextOptions<MeterproDbContext> options) : base(options)
        {

        }
        public DbSet<User_detail> User_detail { get; set; }
        public DbSet<Cab> Cab { get; set; }

        public DbSet<Trip> Trip { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<User_detail>()
                .Property(u => u.user_name)
                .HasMaxLength(50);

            modelBuilder.Entity<User_detail>()
                .Property(u => u.password)
                .HasMaxLength(50);

            modelBuilder.Entity<User_detail>()
                .Property(u => u.first_name)
                .HasMaxLength(50);

            modelBuilder.Entity<User_detail>()
                .Property(u => u.last_name)
                .HasMaxLength(50);

            modelBuilder.Entity<User_detail>()
                .Property(u => u.role)
                .HasMaxLength(50);


            modelBuilder.Entity<Cab>()
               .Property(u => u.carNumber)
               .HasMaxLength(50);

            modelBuilder.Entity<Cab>()
                .Property(u => u.make )
                .HasMaxLength(50);

            modelBuilder.Entity<Cab>()
                .Property(u => u.model)
                .HasMaxLength(50);

        }
    }
}
