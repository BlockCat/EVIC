using Eve_Calender.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eve_Calender.Database
{
    /*public class CharacterContext : DbContext
    {
        public DbSet<AccessTokenModel> AccessTokens { get; set; }

        public CharacterContext(DbContextOptions<CharacterContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccessTokenModel>()
                .HasKey(p => p.CharacterId );

        }

        
    }*/
}
