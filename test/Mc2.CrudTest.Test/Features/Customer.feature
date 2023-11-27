Feature: Create List Edit Delete Customer

    Background:
        Given system error codes are following
          | Code | Description                                                |
          | 101  | Invalid Mobile Number                                      |
          | 102  | Invalid Email address                                      |
          | 103  | Invalid Bank Account Number                                |
          | 201  | Duplicate customer by First-name, Last-name, Date-of-Birth |
          | 202  | Duplicate customer by Email address                        |

    @CustomerCreation
    Scenario: User Creates, List, Edit, Delete a Customer
        Given platform has "0" customers
        When user creates a customer with following data by sending 'Create Customer Command' through API
          | FirstName | LastName | Email        | PhoneNumber   | DateOfBirth | BankAccountNumber |
          | John      | Doe      | John@doe.com | +989121234567 | 01-JAN-2000 | IR000000000000001 |
        Then user can lookup all customers and filter by below properties and get "1" records
          | FirstName | LastName | Email        | PhoneNumber   | DateOfBirth | BankAccountNumber |
          | john      | doe      | john@doe.com | +989121234567 | 01-JAN-2000 | IR000000000000001 |
        When user creates a customer with following data by sending 'Create Customer Command' through API
          | FirstName | LastName | Email        | PhoneNumber   | DateOfBirth | BankAccountNumber |
          | john      | doe      | john@doe.com | +989121234567 | 01-JAN-2000 | IR000000000000001 |
        Then user must receive error codes
          | Code |
          | 201  |
          | 202  |
        When user edit customer by email "john@doe.com" with new data
          | FirstName | LastName | Email            | PhoneNumber | DateOfBirth | BankAccountNumber |
          | Jane      | William  | jane@william.com | +3161234567 | 01-FEB-2010 | IR000000000000002 |
        Then user can lookup all customers and filter by below properties and get "0" records
          | FirstName | LastName | Email        | PhoneNumber   | DateOfBirth | BankAccountNumber |
          | John      | Doe      | john@doe.com | +989121234567 | 01-JAN-2000 | IR000000000000001 |
        And user can lookup all customers and filter by below properties and get "1" records
          | FirstName | LastName | Email            | PhoneNumber | DateOfBirth | BankAccountNumber |
          | Jane      | William  | jane@william.com | +3161234567 | 01-FEB-2010 | IR000000000000002 |
        When user delete customer by Email of "jane@william.com"
        Then user can get list of all customers and must receive "0" record

    @CustomerCreation
    Scenario: Create one customer with valid data
        Given prepare data for create a valid customer
        When create one valid customer
        Then new customer should created and retrievable again

    @CustomerCreation
    Scenario: Create customer with duplicate email not permitted
        Given prepare data for create a valid customer
        And create one valid customer
        When create one customer with douplicate email
        Then get DuplicationInCustomerEMailException