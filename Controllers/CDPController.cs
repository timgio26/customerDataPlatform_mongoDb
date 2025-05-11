using CustomerDataPlatform.Dtos;
using CustomerDataPlatform.Models;
using CustomerDataPlatform.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace CustomerDataPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CDPController(CustomerService customerService) : ControllerBase
    {
        private readonly CustomerService _customerService = customerService;

        [HttpGet("customer")]
        public async Task<ActionResult<List<Customer>>> Get()
        {
            var allCustomer = await _customerService.GetAsync();
            return allCustomer;
        }

        [HttpPost("customer")]
        public async Task<IActionResult> Post(NewCustomerDto request)
        {
            Customer newCustomer = new Customer
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                CreatedAt = DateTime.UtcNow
            }
            ;
            await _customerService.CreateAsync(newCustomer);
            return CreatedAtAction(nameof(Get), new { id = newCustomer.Id }, newCustomer);
        }

        [HttpGet("customer/{id}")]
        public async Task<ActionResult<Customer>> Get(string id)
        {
            var customer = await _customerService.GetAsync(id);
            if (customer is null)
            {
                return NotFound();
            }
            return Ok(customer);
        }


    }
}
