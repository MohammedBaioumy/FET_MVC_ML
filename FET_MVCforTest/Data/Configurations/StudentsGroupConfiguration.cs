using FET_MVCforTest.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FET_MVCforTest.Data.Configurations
{
	public class StudentsGroupConfiguration : IEntityTypeConfiguration<StudentsGroup>
	{
		public void Configure(EntityTypeBuilder<StudentsGroup> builder)
		{
			builder.ToTable("StudentsGroups");

			builder.HasKey(g => g.Id);

			builder.Property(g => g.Name)
				   .IsRequired()
				   .HasMaxLength(100);
		}
	}
}
