﻿using Hackaton.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hackaton.Api.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Entities.File> Files { get; set; }
    public DbSet<FileProcess> FileProcesses { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        DefineDeleteStrategy(modelBuilder);
        DefineColumnsWithoutMaxLength(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    private static void DefineDeleteStrategy(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        }
    }

    private static void DefineColumnsWithoutMaxLength(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var properties = entity.GetProperties().Where(p => p.ClrType == typeof(string));

            foreach (var property in properties)
            {
                if (string.IsNullOrEmpty(property.GetColumnType()) && !property.GetMaxLength().HasValue)
                {
                    property.SetColumnType("VARCHAR(500)");
                }
            }
        }
    }
}
