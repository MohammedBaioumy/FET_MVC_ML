using FET_MVCforTest.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FET_MVCforTest.Data.Configurations
{
	public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
	{
		public void Configure(EntityTypeBuilder<Subject> builder)
		{
			builder.ToTable("Subjects");

			builder.HasKey(s => s.Id);

			builder.Property(s => s.Name)
				   .IsRequired()
				   .HasMaxLength(50);
			builder.Property(s => s.FullScientificName)
				   .IsRequired()
				   .HasMaxLength(100);
		}
	}
}
