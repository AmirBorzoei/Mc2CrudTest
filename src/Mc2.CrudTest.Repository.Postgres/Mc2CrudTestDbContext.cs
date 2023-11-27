using Mc2.CrudTest.Domain.Customer;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Repository.Postgres;

public class Mc2CrudTestDbContext : DbContext
{
    public Mc2CrudTestDbContext(DbContextOptions<Mc2CrudTestDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    public virtual DbSet<Customer> Customers { get; set; } = null!;
}