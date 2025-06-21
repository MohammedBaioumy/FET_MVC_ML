using FET_MVCforTest.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FET_MVCforTest.Data.Configurations
{
	public class BasicConfiguration : IEntityTypeConfiguration<Basic>
	{
		public void Configure(EntityTypeBuilder<Basic> builder)
		{
			builder.Property(c => c.startOfWeek)
				.HasConversion(
					v => v.ToString(),
					v => (DayOfWeek)Enum.Parse(typeof(DayOfWeek), v))
				.IsRequired(false)
				.HasDefaultValue(DayOfWeek.Saturday);

			builder.Property(c => c.endOfWeek)
				.HasConversion(
					v => v.ToString(),
					v => (DayOfWeek)Enum.Parse(typeof(DayOfWeek), v))
				.IsRequired(false)
				.HasDefaultValue(DayOfWeek.Thursday);

			//builder.Property(c => c.TimeStart)
			//	   .HasDefaultValue(TimeOnly.FromTimeSpan(TimeSpan.FromHours(9)))
			//	   .IsRequired(false);
			//builder.Property(c => c.TimeEnd)
			//	   .HasDefaultValue(TimeOnly.FromTimeSpan(TimeSpan.FromHours(5)))
			//	   .IsRequired(false);
		}
	}
}
