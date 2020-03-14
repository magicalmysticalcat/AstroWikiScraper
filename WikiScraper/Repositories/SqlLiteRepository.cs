using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using WikiScraper.Models;

namespace WikiScraper.Repositories
{
    public sealed class SqlLiteRepository: DbContext, IRepository
    {
        public DbSet<Event> Events { get; set; }
        private readonly string _connectionString;
        private static bool _created = false;
        public SqlLiteRepository(string connectionString)
        {
            _connectionString = connectionString;
            if (!_created)
            {
                _created = true;
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite(_connectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().ToTable("Event");
        }

        public void Save(Event item)
        {
            Events.Add(item);
            SaveChanges();
        }

        public void Save(IEnumerable<Event> items)
        {
            try
            {
                Events.AddRange(items);
                SaveChanges();
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}