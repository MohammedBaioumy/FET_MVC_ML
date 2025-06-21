using FET_MVCforTest.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FET_MVCforTest.Data.Configurations
{
	public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
	{
		public void Configure(EntityTypeBuilder<Teacher> builder)
		{
			builder.ToTable("Teachers");

			builder.HasKey(t => t.Id);

			builder.Property(t => t.Name)
				   .IsRequired()
				   .HasMaxLength(100);
		}
	}
}
