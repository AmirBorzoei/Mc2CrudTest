using Mc2.CrudTest.Api.Customer.V1.Model;
using Mc2.CrudTest.ApplicationService.Contract.Customer;
using Mc2.CrudTest.ApplicationService.Contract.Customer.Command;
using Mc2.CrudTest.Domain.Contract.Model;
using Mc2.CrudTest.Domain.Customer.DomainService;
using Microsoft.AspNetCore.Mvc;

namespace Mc2.CrudTest.Api.Customer.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/customers")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepository _repository;
    private readonly ICustomerApplicationService _service;

    public CustomerController(ICustomerRepository repository,
        ICustomerApplicationService service)
    {
        _repository = repository;
        _service = service;
    }

    [HttpPost(Name = nameof(CreateCustomer))]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<ActionResult<Guid>> CreateCustomer([FromBody] CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        Guid result = await _service.CreateCustomerAsync(command, cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:Guid}", Name = nameof(UpdateCustomer))]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateCustomer([FromRoute] Guid id, [FromBody] UpdateCustomerCommand command,
        CancellationToken cancellationToken)
    {
        await _service.UpdateCustomerAsync(id, command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:Guid}", Name = nameof(DeleteCustomer))]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteCustomer([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        DeleteCustomerCommand command = new()
            { Id = id };
        await _service.DeleteCustomerAsync(command, cancellationToken);
        return NoContent();
    }



    //Todo: Move to ReadModel
    [HttpGet(Name = nameof(GetCustomers))]
    [ProducesResponseType(typeof(IEnumerable<CustomerDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers([FromQuery] CustomerQuery query,
        CancellationToken cancellationToken)
    {
        List<Domain.Customer.Customer> customers = await _repository.GetAllAsync(query, cancellationToken);
        List<CustomerDto> customerDtos = customers.Select(CustomerDto.GenerateCustomerDto).ToList();
        return Ok(customerDtos);
    }

    [HttpGet("{id:Guid}", Name = nameof(GetCustomer))]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<CustomerDto>> GetCustomer([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        Domain.Customer.Customer customer = await _repository.GetByIdAsync(id, cancellationToken);
        CustomerDto customerDto = CustomerDto.GenerateCustomerDto(customer);
        return Ok(customerDto);
    }
}