using Mc2.CrudTest.Domain.Customer;
using Mc2.Framework.Core.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mc2.CrudTest.Repository.Postgres.EntityTypeConfiguration;

public class CustomerConfiguration : EntityTypeConfiguration<Customer>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(c => c.Id).HasIdentityOptions();
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Firstname)
            .HasColumnType("character varying(256)")
            .IsRequired();

        builder.Property(c => c.Lastname)
            .HasColumnType("character varying(256)")
            .IsRequired();

        builder.Property(c => c.DateOfBirth).IsRequired();

        builder.Property(c => c.PhoneNumber)
            .HasColumnType("character varying(32)")
            .HasConversion(p => p.Value, v => new PhoneNumber(v, null));

        builder.Property(c => c.Email)
            .HasColumnType("character varying(256)")
            .HasConversion(e => e.Value, v => new EMail(v));

        builder.Property(c => c.BankAccountNumber)
            .HasColumnType("character varying(128)")
            .HasConversion(b => b.Value, v => new BankAccountNumber(v));

        builder.HasIndex(nameof(Customer.Firstname), nameof(Customer.Lastname), nameof(Customer.DateOfBirth)).IsUnique();
        builder.HasIndex(e => e.Email).IsUnique();

        CreateTable(builder);
    }
}