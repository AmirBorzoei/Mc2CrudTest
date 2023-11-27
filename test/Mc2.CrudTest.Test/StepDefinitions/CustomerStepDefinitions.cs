using System.Collections.Specialized;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using Mc2.CrudTest.Api.Customer.V1.Model;
using Mc2.CrudTest.Api.ExceptionHandling.Models;
using Mc2.CrudTest.ApplicationService.Contract.Customer.Command;
using Mc2.CrudTest.Domain.Contract.DomainException;
using Mc2.CrudTest.Domain.Contract.Model;
using Mc2.CrudTest.Domain.Customer;
using Mc2.CrudTest.Test.BaseTests;
using Mc2.CrudTest.Test.Constant;
using Mc2.Framework.Core.Exception;
using Mc2.Framework.Core.ValueObject;
using TechTalk.SpecFlow.Assist;

namespace Mc2.CrudTest.Test.StepDefinitions;

[Binding]
public class CustomerStepDefinitions : IntegrationTest
{
    private Guid _newCustomerId;
    private CreateCustomerCommand _createCustomerCommand = null!;
    private HttpResponseMessage _createCustomerHttpResponse = null!;

    [Given(@"prepare data for create a valid customer")]
    public void GivenPrepareDataForCreateAValidCustomer()
    {
        _newCustomerId = Guid.Empty;
        _createCustomerCommand = GenerateCreateSomeCustomerCommand();
    }

    [When(@"create one valid customer")]
    public void WhenCreateOneValidCustomer()
    {
        HttpResponseMessage createCustomerHttpResponse =
            Client.PostAsJsonAsync(RouteProvider.Customers, _createCustomerCommand).GetAwaiter().GetResult();

        createCustomerHttpResponse.EnsureSuccessStatusCode();

        _newCustomerId = createCustomerHttpResponse.Content.ReadFromJsonAsync<Guid>().GetAwaiter().GetResult();
    }

    [Then(@"new customer should created and retrievable again")]
    public void ThenNewCustomerShouldCreatedAndRetrievableAgain()
    {
        CustomerDto? loadedCustomer = Client.GetFromJsonAsync<CustomerDto>($"{RouteProvider.Customers}/{_newCustomerId}").GetAwaiter().GetResult();

        loadedCustomer.Should().NotBeNull();

        loadedCustomer!.Firstname.Should().Be(_createCustomerCommand.Firstname);
        loadedCustomer.Lastname.Should().Be(_createCustomerCommand.Lastname);
        loadedCustomer.DateOfBirth.Should().Be(_createCustomerCommand.DateOfBirth);
        loadedCustomer.PhoneNumber.Should().Be(_createCustomerCommand.PhoneNumber);
        loadedCustomer.Email.Should().Be(_createCustomerCommand.Email?.ToLower());
        loadedCustomer.BankAccountNumber.Should().Be(_createCustomerCommand.BankAccountNumber);
    }


    [Given(@"create one valid customer")]
    public void GivenCreateOneValidCustomer()
    {
        HttpResponseMessage createCustomerHttpResponse =
            Client.PostAsJsonAsync(RouteProvider.Customers, _createCustomerCommand).GetAwaiter().GetResult();

        createCustomerHttpResponse.EnsureSuccessStatusCode();
    }

    [When(@"create one customer with douplicate email")]
    public void WhenCreateOneCustomerWithDouplicateEmail()
    {
        _createCustomerCommand = GenerateCreateAnotherCustomerCommand();
        _createCustomerCommand.Email = "Amir.Borzoei@gmail.com";

        _createCustomerHttpResponse = Client.PostAsJsonAsync(RouteProvider.Customers, _createCustomerCommand).GetAwaiter().GetResult();
    }

    [Then(@"get DuplicationInCustomerEMailException")]
    public void ThenGetDuplicationInCustomerEMailException()
    {
        _createCustomerHttpResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);

        ErrorResponse? errorResponse = _createCustomerHttpResponse.Content.ReadFromJsonAsync<ErrorResponse>().GetAwaiter().GetResult();
        
        errorResponse.Should().NotBeNull();
        errorResponse!.Errors.Should().NotBeEmpty();
        errorResponse.Errors.Should().Contain(e => e.Code == 202);
    }


    [Given(@"system error codes are following")]
    public void GivenSystemErrorCodesAreFollowing(Table table)
    {
    }

    [Given(@"platform has ""([^""]*)"" customers")]
    public void GivenPlatformHasCustomers(int customerCount)
    {
        IEnumerable<CustomerDto>? loadedCustomers =
            Client.GetFromJsonAsync<IEnumerable<CustomerDto>>($"{RouteProvider.Customers}").GetAwaiter().GetResult();

        loadedCustomers.Should().HaveCount(customerCount);
    }

    [When(@"user creates a customer with following data by sending '([^']*)' through API")]
    public void WhenUserCreatesACustomerWithFollowingDataBySendingThroughAPI(string commandName, Table customerData)
    {
        CreateCustomerCommand createCustomerCommand = GenerateCreateCustomerCommand(customerData);
        _createCustomerHttpResponse = Client.PostAsJsonAsync(RouteProvider.Customers, createCustomerCommand).GetAwaiter().GetResult();
    }

    [Then(@"user can lookup all customers and filter by below properties and get ""([^""]*)"" records")]
    public void ThenUserCanLookupAllCustomersAndFilterByBelowPropertiesAndGetRecords(int customerCount, Table customerData)
    {
        string queryString = GenerateCustomerQueryString(customerData);
        string url = $"{RouteProvider.Customers}?{queryString}";
        IEnumerable<CustomerDto>? loadedCustomers = Client.GetFromJsonAsync<IEnumerable<CustomerDto>>(url).GetAwaiter().GetResult();
        
        loadedCustomers.Should().HaveCount(customerCount);
    }

    [Then(@"user must receive error codes")]
    public void ThenUserMustReceiveErrorCodes(Table errorCodes)
    {
        ErrorResponse? errorResponse = _createCustomerHttpResponse.Content.ReadFromJsonAsync<ErrorResponse>().GetAwaiter().GetResult();

        errorResponse.Should().NotBeNull();
        errorResponse!.Errors.Should().NotBeEmpty();

        foreach (TableRow errorCodeRow in errorCodes.Rows)
        {
            int errorCode = int.Parse(errorCodeRow["Code"]);
            errorResponse.Errors.Should().Contain(e => e.Code == errorCode);
        }
    }

    [When(@"user edit customer by email ""([^""]*)"" with new data")]
    public void WhenUserEditCustomerByEmailWithNewData(string email, Table customerData)
    {
        NameValueCollection query = HttpUtility.ParseQueryString(string.Empty);
        query["Email"] = email;
        string queryString = query.ToString();

        string getUrl = $"{RouteProvider.Customers}?{queryString}";
        List<CustomerDto>? loadedCustomers = Client.GetFromJsonAsync<List<CustomerDto>>(getUrl).GetAwaiter().GetResult();
        loadedCustomers.Should().HaveCount(1);

        UpdateCustomerCommand updateCustomerCommand = GenerateUpdateCustomerCommand(customerData);

        string updateUrl = $"{RouteProvider.Customers}/{loadedCustomers![0].Id}";
        _createCustomerHttpResponse = Client.PutAsJsonAsync(updateUrl, updateCustomerCommand).GetAwaiter().GetResult();
    }

    [When(@"user delete customer by Email of ""([^""]*)""")]
    public void WhenUserDeleteCustomerByEmailOf(string email)
    {
        NameValueCollection query = HttpUtility.ParseQueryString(string.Empty);
        query["Email"] = email;
        string queryString = query.ToString();

        string getUrl = $"{RouteProvider.Customers}?{queryString}";
        List<CustomerDto>? loadedCustomers = Client.GetFromJsonAsync<List<CustomerDto>>(getUrl).GetAwaiter().GetResult();
        loadedCustomers.Should().HaveCount(1);

        string deleteUrl = $"{RouteProvider.Customers}/{loadedCustomers![0].Id}";
        _createCustomerHttpResponse = Client.DeleteAsync(deleteUrl).GetAwaiter().GetResult();
    }

    [Then(@"user can get list of all customers and must receive ""([^""]*)"" record")]
    public void ThenUserCanGetListOfAllCustomersAndMustReceiveRecord(int customerCount)
    {
        IEnumerable<CustomerDto>? loadedCustomers =
            Client.GetFromJsonAsync<IEnumerable<CustomerDto>>($"{RouteProvider.Customers}").GetAwaiter().GetResult();
        loadedCustomers.Should().HaveCount(customerCount);
    }



    private static CreateCustomerCommand GenerateCreateSomeCustomerCommand()
    {
        CreateCustomerCommand createCustomerCommand = new()
        {
            Firstname = "Amir",
            Lastname = "Borzoei",
            DateOfBirth = new DateOnly(1983, 6, 25),
            PhoneNumber = "+98 21 11222333",
            Email = "Amir.Borzoei@gmail.com",
            BankAccountNumber = "11111"
        };
        return createCustomerCommand;
    }

    private static CreateCustomerCommand GenerateCreateAnotherCustomerCommand()
    {
        CreateCustomerCommand createCustomerCommand = new()
        {
            Firstname = "Amir2",
            Lastname = "Borzoei2",
            DateOfBirth = new DateOnly(1983, 6, 25),
            PhoneNumber = "+98 21 22333444",
            Email = "amir.Borzoei2@gmail.com",
            BankAccountNumber = "22222"
        };
        return createCustomerCommand;
    }

    private static CreateCustomerCommand GenerateCreateCustomerCommand(Table customerData)
    {
        CreateCustomerCommand createCustomerCommand = new()
        {
            Firstname = customerData.Rows[0]["FirstName"],
            Lastname = customerData.Rows[0]["LastName"],
            DateOfBirth = DateOnly.Parse(customerData.Rows[0]["DateOfBirth"]),
            PhoneNumber = customerData.Rows[0]["PhoneNumber"],
            Email = customerData.Rows[0]["Email"],
            BankAccountNumber = customerData.Rows[0]["BankAccountNumber"]
        };
        return createCustomerCommand;
    }

    private static UpdateCustomerCommand GenerateUpdateCustomerCommand(Table customerData)
    {
        UpdateCustomerCommand updateCustomerCommand = new()
        {
            Firstname = customerData.Rows[0]["FirstName"],
            Lastname = customerData.Rows[0]["LastName"],
            DateOfBirth = DateOnly.Parse(customerData.Rows[0]["DateOfBirth"]),
            PhoneNumber = customerData.Rows[0]["PhoneNumber"],
            Email = customerData.Rows[0]["Email"],
            BankAccountNumber = customerData.Rows[0]["BankAccountNumber"]
        };
        return updateCustomerCommand;
    }

    private static string GenerateCustomerQueryString(Table customerData)
    {
        NameValueCollection query = HttpUtility.ParseQueryString(string.Empty);
        query["Firstname"] = customerData.Rows[0]["FirstName"];
        query["Lastname"] = customerData.Rows[0]["LastName"];
        query["DateOfBirth"] = customerData.Rows[0]["DateOfBirth"];
        query["PhoneNumber"] = customerData.Rows[0]["PhoneNumber"];
        query["Email"] = customerData.Rows[0]["Email"];
        query["BankAccountNumber"] = customerData.Rows[0]["BankAccountNumber"];

        return query.ToString();
    }
}