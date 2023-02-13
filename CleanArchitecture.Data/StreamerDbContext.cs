﻿using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Data
{
    public class StreamerDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost\sqlexpress; 
                       Initial Catalog=Streamer;Integrated Security=True")
                .LogTo(Console.WriteLine, new[] {DbLoggerCategory.Database.Command.Name},
                        Microsoft.Extensions.Logging.LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //esta declaracion es opcional del fluent API
            //se usa cuando las clases no siguen las convenciones del EF
            modelBuilder.Entity<Streamer>()
                .HasMany(m=>m.Videos)
                .WithOne(m=>m.Streamer)
                .HasForeignKey(m=>m.StreamerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Video>()
                .HasMany(m => m.Actores)
                .WithMany(m => m.Videos)
                .UsingEntity<VideoActor>(
                    pt => pt.HasKey(e=> new { e.ActorId,e.VideoId})
                    );
        }

        public DbSet<Streamer>? Streamers { get; set; }

        public DbSet<Video>? Videos { get; set; }


    }
}


