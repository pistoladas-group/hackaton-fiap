using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Hackaton.Api.Data.Entities;

namespace Hackaton.Api.Data.EntitiesConfigurations;

public class FileConfiguration : IEntityTypeConfiguration<Entities.File>
{
    public void Configure(EntityTypeBuilder<Entities.File> builder)
    {
        builder.ToTable("Files");

        builder.HasKey(p => p.Id).HasName("PK_Files");
        builder.Property(p => p.Id).HasColumnType("UNIQUEIDENTIFIER").HasColumnName("Id").IsRequired();
        builder.Property(p => p.CreatedAt).HasColumnType("DATETIME").HasColumnName("CreatedAt").IsRequired();
        builder.Property(p => p.IsDeleted).HasColumnType("BIT").HasColumnName("IsDeleted").IsRequired();
        builder.Property(p => p.Name).HasColumnType("VARCHAR(100)").HasColumnName("Name").IsRequired();
        builder.Property(p => p.SizeInBytes).HasColumnType("INT").HasColumnName("SizeInBytes").IsRequired();
        builder.Property(p => p.Url).HasColumnType("VARCHAR(2500)").HasColumnName("Url");
        builder.Property(p => p.ContentType).HasColumnType("VARCHAR(100)").HasColumnName("ContentType").IsRequired();

        builder.HasQueryFilter(p => !p.IsDeleted);
    }
}
