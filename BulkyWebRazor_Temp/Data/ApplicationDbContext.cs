using BulkyWebRazor_Temp.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyWebRazor_Temp.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		public DbSet<Category> Categories { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder) /*Seed data for category*/
		{
			modelBuilder.Entity<Category>().HasData(
				new Category { Id = 1, Name = "Action", DisplayOrder = 1 }, /*Create 3 records*/
				new Category { Id = 2, Name = "Scifi", DisplayOrder = 2 },   /*Pack Manager console. add-Migrations SeedCategoryTable*/
				new Category { Id = 3, Name = "History", DisplayOrder = 3 }  /*always add-Migrations for any changes*/
				);
		}
	}
}