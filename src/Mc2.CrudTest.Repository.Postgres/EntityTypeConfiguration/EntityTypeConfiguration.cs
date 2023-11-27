using Mc2.Framework.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mc2.CrudTest.Repository.Postgres.EntityTypeConfiguration;

public abstract class EntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
{
    public abstract void Configure(EntityTypeBuilder<T> builder);

    protected virtual void CreateTable(EntityTypeBuilder<T> builder)
    {
        string tableName = typeof(T).Name;
        string? schemaName = typeof(T).Namespace?.Split('.')[1];
        builder.ToTable(tableName, schemaName);
    }
}