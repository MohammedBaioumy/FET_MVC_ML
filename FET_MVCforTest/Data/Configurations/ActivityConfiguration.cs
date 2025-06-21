using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FET_MVCforTest.Entities;

namespace FET_MVCforTest.Data.Configurations
{
	public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
	{
		public void Configure(EntityTypeBuilder<Activity> builder)
		{
			builder.ToTable("Activities");

			builder.HasKey(a => a.Id);

			builder.HasOne(a => a.Teacher)
				   .WithMany()
				   .HasForeignKey(a => a.TeacherId)
				   .OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(a => a.Subject)
				   .WithMany()
				   .HasForeignKey(a => a.SubjectId)
				   .OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(a => a.StudentsGroup)
				   .WithMany()
				   .HasForeignKey(a => a.StudentsGroupId)
				   .OnDelete(DeleteBehavior.Restrict);
		}
	}
}
