using FET_MVCforTest.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FET_MVCforTest.Data.Configurations
{
	public class FacultyConfiguration : IEntityTypeConfiguration<Faculty>
	{
		public void Configure(EntityTypeBuilder<Faculty> builder)
		{
			builder.ToTable("Faculties");

			builder.HasKey(f => f.Id);

			builder.Property(f => f.Name)
				   .IsRequired()
				   .HasMaxLength(100);

			builder.HasMany(f => f.Departments)
				   .WithOne(d => d.Faculty)
				   .HasForeignKey(d => d.FacultyId)
				   .OnDelete(DeleteBehavior.Cascade);
		}
	}
}
