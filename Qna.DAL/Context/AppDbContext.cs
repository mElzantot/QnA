using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QnA.DbModels;

namespace Qna.DAL.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        #region Properties and DbSets 
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Vote> Votes { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Vote>().HasKey(x => new { x.UserId, x.AnswerId });

            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Ahmed",
                    PasswordHash = "AFdDaV7xPSc76LKz37gq7akbcG5TK6hnX//AedyawbPCObpCTCIXEmlX9YXIMn1OMQ=="
                });

            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Islam",
                    PasswordHash = "AFdDaV7xPSc76LKz37gq7akbcG5TK6hnX//AedyawbPCObpCTCIXEmlX9YXIMn1OMQ=="
                });

            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Mustafa",
                    PasswordHash = "AFdDaV7xPSc76LKz37gq7akbcG5TK6hnX//AedyawbPCObpCTCIXEmlX9YXIMn1OMQ=="
                });

            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Maha",
                    PasswordHash = "AFdDaV7xPSc76LKz37gq7akbcG5TK6hnX//AedyawbPCObpCTCIXEmlX9YXIMn1OMQ=="
                });

            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Aya",
                    PasswordHash = "AFdDaV7xPSc76LKz37gq7akbcG5TK6hnX//AedyawbPCObpCTCIXEmlX9YXIMn1OMQ=="
                });


            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
