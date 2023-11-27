using Mc2.CrudTest.Repository.Postgres;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Test.Migrator;

public class Migrator
{
    private readonly Mc2CrudTestDbContext _dbContext;

    public Migrator(Mc2CrudTestDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Migrate()
    {
        _dbContext.Database.Migrate();
    }
}