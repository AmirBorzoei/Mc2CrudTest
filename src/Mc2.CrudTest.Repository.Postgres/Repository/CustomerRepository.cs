using Mc2.CrudTest.Domain.Contract.Model;
using Mc2.CrudTest.Domain.Customer;
using Mc2.CrudTest.Domain.Customer.DomainService;
using Mc2.Framework.Core.Exception;
using Mc2.Framework.Core.ValueObject;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Repository.Postgres.Repository;

public class CustomerRepository : ICustomerRepository
{
    private readonly Mc2CrudTestDbContext _dbContext;

    public CustomerRepository(Mc2CrudTestDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public Task<List<Customer>> GetAllAsync(CustomerQuery query, CancellationToken cancellationToken)
    {
        return _dbContext.Customers
            .Where(c => (string.IsNullOrEmpty(query.Firstname) || c.Firstname.ToLower() == query.Firstname.ToLower()) &&
                        (string.IsNullOrEmpty(query.Lastname) || c.Lastname.ToLower() == query.Lastname.ToLower()) &&
                        (query.DateOfBirth == null || c.DateOfBirth == query.DateOfBirth) &&
                        (string.IsNullOrEmpty(query.PhoneNumber) || c.PhoneNumber == new PhoneNumber(query.PhoneNumber.ToLower(), null)) &&
                        (string.IsNullOrEmpty(query.Email) || c.Email == new EMail(query.Email)) &&
                        (string.IsNullOrEmpty(query.BankAccountNumber) || c.BankAccountNumber == new BankAccountNumber(query.BankAccountNumber)))
            .OrderBy(c => c.Firstname)
            .ThenBy(c => c.Lastname)
            .ToListAsync(cancellationToken);
    }

    public async Task<Customer> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Customer? customer = await _dbContext.Customers.FirstOrDefaultAsync(
            c => c.Id == id,
            cancellationToken);

        if (customer == null)
        {
            throw new NotFoundException($"Expected customer whit id [{id}] not found.");
        }

        return customer;
    }


    public async Task<bool> IsDuplicateEMailAsync(Guid id, EMail eMail, CancellationToken cancellationToken)
    {
        bool isExistCustomer = await _dbContext.Customers.AnyAsync(
            c => c.Id != id &&
                 c.Email == eMail,
            cancellationToken);

        return isExistCustomer;
    }

    public async Task<bool> IsDuplicateUniqueInfoAsync(Guid id, string firstname, string lastname, DateOnly dateOfBirth,
        CancellationToken cancellationToken)
    {
        bool isExistCustomer = await _dbContext.Customers.AnyAsync(
            c => c.Id != id &&
                 c.Firstname.ToLower() == firstname.ToLower() &&
                 c.Lastname.ToLower() == lastname.ToLower() &&
                 c.DateOfBirth == dateOfBirth,
            cancellationToken);

        return isExistCustomer;
    }


    public async Task SaveAsync(Customer customer, CancellationToken cancellationToken)
    {
        await _dbContext.Customers.AddAsync(customer, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Customer customer, CancellationToken cancellationToken)
    {
        _dbContext.Customers.Update(customer);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _dbContext.Customers.Where(c => c.Id == id).ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }
}