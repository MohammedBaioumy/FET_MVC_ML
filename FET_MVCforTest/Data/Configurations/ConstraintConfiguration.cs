using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FET_MVCforTest.Entities;


namespace FET_MVCforTest.Data.Configurations
{
	public class ConstraintConfiguration : IEntityTypeConfiguration<Constraints>
	{
		public void Configure(EntityTypeBuilder<Constraints> builder)
		{
			builder.HasKey(c => c.Id);

			builder.Property(c => c.ConstraintName).HasColumnType("varchar(100)");
		}
	}

}
