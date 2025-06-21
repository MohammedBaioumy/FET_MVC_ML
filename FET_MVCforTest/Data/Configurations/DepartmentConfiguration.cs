using FET_MVCforTest.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FET_MVCforTest.Data.Configurations
{
	public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
	{
		public void Configure(EntityTypeBuilder<Department> builder)
		{
			builder.ToTable("Departments");

			builder.HasKey(d => d.Id);

			builder.Property(d => d.Name)
				   .IsRequired()
				   .HasMaxLength(100);

			builder.HasOne(d => d.Faculty)
				   .WithMany(f => f.Departments)
				   .HasForeignKey(d => d.FacultyId)
				   .OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(d => d.Subjects)
				   .WithOne(s => s.Department)
				   .HasForeignKey(s => s.DepartmentId)
				   .OnDelete(DeleteBehavior.Cascade);
		}
	}
}
