using DataAccess.Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Layer.Context
{
    public class SigmaDbContext : DbContext
    {
        public SigmaDbContext(DbContextOptions<SigmaDbContext> options) : base(options)
        {

        }
        public DbSet<Candidate> Candidates { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>()
                .HasIndex(c => c.FirstName)
                .IsUnique(false);

            modelBuilder.Entity<Candidate>()
                .HasIndex(c => c.LastName)
                .IsUnique(false);
        }
    }
}
