using FET_MVCforTest.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FET_MVCforTest.Data.Configurations
{
	public class RoomConfiguration : IEntityTypeConfiguration<Room>
	{
		public void Configure(EntityTypeBuilder<Room> builder)
		{
			builder.ToTable("Rooms");

			builder.HasKey(r => r.Id);

			builder.Property(r => r.Name)
				   .IsRequired()
				   .HasMaxLength(100);
		}
	}
}
