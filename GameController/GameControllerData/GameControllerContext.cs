using System;
using System.Collections.Generic;
using GameControllerData.Models;
using Microsoft.EntityFrameworkCore;

namespace GameControllerData
{
    /*
     * DbContext that represents the database itself
     */
    public class GameControllerContext : DbContext
    {
        public readonly IEnumerable<Team> GameControllerAsset;

        public GameControllerContext(DbContextOptions options) : base(options) { }

        public DbSet<Team> Team { get; set; }
        public DbSet<Adventure> Adventure { get; set; }
        public DbSet<AdventureMap> AdventureMap{ get; set; }
        public DbSet<AdventureSession> AdventureSession{ get; set; }
        public DbSet<Challenge> Challenge{ get; set; }
        public DbSet<Message> Message{ get; set; }
        public DbSet<Student> Student{ get; set; }
        public DbSet<Submission> Submission{ get; set; }
        public DbSet<Waypoint> Waypoint { get; set; }
        public DbSet<Session> Session { get; set; }
        public DbSet<newAdventureMap> newAdventureMaps { get; set; }
        //Making a composite primary key for AdventureMap
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdventureMap>().HasKey(am => new { am.FromWaypointID, am.ToWaypointID });
        }
    }
}
