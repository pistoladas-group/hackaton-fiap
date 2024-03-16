using Hackaton.Api.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Hackaton.Api.Data.EntitiesConfigurations;

public class FileProcessConfiguration : IEntityTypeConfiguration<FileProcess>
{
    public void Configure(EntityTypeBuilder<FileProcess> builder)
    {
        builder.ToTable("FileProcesses");

        builder.HasKey(p => p.Id).HasName("PK_FileProcesses");
        builder.Property(p => p.Id).HasColumnType("UNIQUEIDENTIFIER").HasColumnName("Id").IsRequired();
        builder.Property(p => p.CreatedAt).HasColumnType("DATETIME").HasColumnName("CreatedAt").IsRequired();
        builder.Property(p => p.IsDeleted).HasColumnType("BIT").HasColumnName("IsDeleted").IsRequired();


        builder.Property(p => p.FileId).HasColumnType("UNIQUEIDENTIFIER").HasColumnName("FileId").IsRequired();
        builder.Property(p => p.ProcessTypeId).HasColumnType("TINYINT").HasColumnName("ProcessTypeId").IsRequired();
        builder.Property(p => p.ProcessStatusId).HasColumnType("TINYINT").HasColumnName("ProcessStatusId");
        builder.Property(p => p.ErrorMessage).HasColumnType("VARCHAR(1000)").HasColumnName("ErrorMessage");
        builder.Property(p => p.StartedAt).HasColumnType("DATETIME").HasColumnName("StartedAt");
        builder.Property(p => p.FinishedAt).HasColumnType("DATETIME").HasColumnName("FinishedAt");

        builder.HasQueryFilter(p => !p.IsDeleted);
    }
}
