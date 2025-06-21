using FET_MVCforTest.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FET_MVCforTest.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

			base.OnModelCreating(modelBuilder);
		}
		public DbSet<Basic> Basics { get; set; }

		public DbSet<Faculty> Faculties { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Subject> Subjects { get; set; }
		public DbSet<Teacher> Teachers { get; set; }
		public DbSet<StudentsGroup> StudentsGroups { get; set; }
		public DbSet<Activity> Activities { get; set; }
		public DbSet<Room> Rooms { get; set; }
		public DbSet<Constraints> Constraints { get; set; }
		//
		public DbSet<SubjectArticle> SubjectArticles { get; set; }

	}
}
