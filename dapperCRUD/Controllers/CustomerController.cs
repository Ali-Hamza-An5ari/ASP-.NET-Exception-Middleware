using Dapper;
using dapperCRUD.Middleware;
using dapperCRUD.Middleware.With_Exception;
using dapperCRUD.Models;
using dapperCRUD.Services.CustomerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace dapperCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repository;
        public CustomerController(ICustomerRepository repository)
        {
            _repository = repository;
        }
     
        [HttpGet("/GetAll")]
        public async Task<ActionResult<List<Customer>>> GetAllCustomers()
        {

            //throw new UnauthorizedAccessException();
            //throw new ForbiddenException();
            IEnumerable<Customer> customers = await _repository.GetAllCustomers();
            return Ok(customers);
        }


        [HttpGet("/Get/{customerId}")]
        public async Task<ActionResult<Customer>> GetCustomer(Guid customerId)
        {


            //if (searchedCustomer == null)
            //{
            //    return NotFound();
            //}

            //Customer searchedCustomer;

            //try
            //{

            //     searchedCustomer = await _repository.GetCustomerById(customerId);

            //}
            //catch (AException ex)
            //{
            //    // handle custom exception
            //    Console.WriteLine($"Error code: {ex.ErrorCode}. Message: {ex.Message}");
            //}
            //catch (Exception ex)
            //{
            //    // handle other exceptions
                
            //}

            var searchedCustomer = await _repository.GetCustomerById(customerId);

            if(searchedCustomer == null)
            {
                throw new AException("Customer not Found.", "ERR001");
            }
            //throw new AException();
            return Ok(searchedCustomer);

        }


        [HttpPost("Create")]
        public async Task<ActionResult<List<Customer>>> CreateCustomer(Customer customer)
        {
            await _repository.CreateCustomer(customer);
            return Ok(await _repository.GetCustomerById(customer.Id));

        }

        [HttpPut("UpdateCustomer")]
        public async Task<ActionResult<List<Customer>>> UpdateCustomer(Customer customer)
        {
            var searchedCustomer = await _repository.GetCustomerById(customer.Id);

            if (searchedCustomer == null)
            {
                return NotFound();
            }
            await _repository.UpdateCustomer(customer);
            return Ok(await _repository.GetAllCustomers());
        }


        [HttpDelete("/Delete/{customerId}")]
        public async Task<ActionResult<List<Customer>>> DeleteCustomer(Guid customerId)
        {

            // Check if the customer exists
             var searchedCustomer = await _repository.GetCustomerById(customerId);

            if (searchedCustomer == null)
            {
                return NotFound();
            }
            await _repository.DeleteCustomer(customerId);

            return Ok(await _repository.GetAllCustomers());
        }

        [HttpGet("/GetIdName")]
        public async Task<ActionResult<List<string>>> GetIdNameCustomers()
        {

            IEnumerable<Customer> customers = await _repository.GetAllCustomers();
            List<string> names = new List<string>();
            foreach (Customer customer in customers)
            {
                names.Add(customer.Name);
            }
            return Ok(names);
        }

        [HttpGet("/GetIdName{customerId}")]
        public async Task<ActionResult<string>> GetNameCustomersId(Guid customerId)
        {

           Customer customer = await _repository.GetCustomerById(customerId);
           
            return Ok(customer.Name);
        }
    }
}
