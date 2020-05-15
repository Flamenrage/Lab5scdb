using Lab5.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace Lab5
{
    class Data_baseContext: DbContext
    {
        private string server;
        private string port;
        private string username;
        private string database;

        public Data_baseContext(string server, string port, string username, string database)
        {
            this.server = server;
            this.port = port;
            this.username = username;
            this.database = database;

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            String connectionString = "Server=" + server + ";" +
                                      "Port=" + port + ";" +
                                      "Username=" + username + ";" +
                                      "Database=" + database + ";";

            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("seq_teacher").IncrementsBy(1).StartsAt(1);
            modelBuilder.HasSequence<int>("seq_speciality").IncrementsBy(1).StartsAt(1);
            modelBuilder.HasSequence<int>("seq_classroom").IncrementsBy(1).StartsAt(1);
            modelBuilder.HasSequence<int>("seq_discipline").IncrementsBy(1).StartsAt(1);
            modelBuilder.HasSequence<int>("seq_timetable").IncrementsBy(1).StartsAt(1);



            modelBuilder.Entity<Teacher>(entity => { entity.Property(e => e.Id).UseHiLo("seq_teacher"); });
            modelBuilder.Entity<Speciality>(entity => { entity.Property(e => e.Id).UseHiLo("seq_speciality"); });
            modelBuilder.Entity<Classroom>(entity => { entity.Property(e => e.Id).UseHiLo("seq_classroom"); });
            modelBuilder.Entity<Discipline>(entity => { entity.Property(e => e.Id).UseHiLo("seq_discipline"); });
            modelBuilder.Entity<Timetable>(entity => { entity.Property(e => e.Id).UseHiLo("seq_timetable"); });
        }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Timetable> Timetables { get; set; }
    }
}
