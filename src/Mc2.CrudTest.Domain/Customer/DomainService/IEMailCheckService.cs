using Mc2.Framework.Core.ValueObject;

namespace Mc2.CrudTest.Domain.Customer.DomainService;

public interface IEMailUniqueCheckService
{
    Task CheckAsync(Guid id, EMail eMail, CancellationToken cancellationToken);
}