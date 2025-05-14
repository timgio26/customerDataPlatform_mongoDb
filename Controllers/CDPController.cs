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
        public async Task<ActionResult<List<Customer>>> GetAllCustomer()
        {
            var allCustomer = await _customerService.GetAsync();
            return Ok(allCustomer);
        }

        [HttpPost("customer")]
        public async Task<IActionResult> AddNewCustomer(NewCustomerDto request)
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
            return CreatedAtAction(nameof(GetAllCustomer), new { id = newCustomer.Id }, newCustomer);
        }

        [HttpGet("customer/{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(string id)
        {
            var customer = await _customerService.GetAsync(id);
            if (customer is null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost("address")]
        public async Task<ActionResult<Customer>> AddAddress(NewAddressDto request)
        {
            var existingCustomer = await _customerService.GetAsync(request.CustomerId);
            if (existingCustomer is null)
            {
                return NotFound();
            }
            Address newAddress = new Address
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Alamat = request.Alamat,
                Kategori = request.Kategori,
                CreatedAt = DateTime.UtcNow,
            };  
            await _customerService.AddAddressAsync(request.CustomerId, newAddress);
            return CreatedAtAction(nameof(GetAllCustomer), new { id = request.CustomerId }, existingCustomer);
        }

        [HttpPost("service")]
        public async Task<ActionResult<Customer>> AddService(NewServiceDto request)
        {
            try
            {
                Service newService = new Service
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Keluhan = request.Keluhan,
                    Tindakan = request.Tindakan,
                    Hasil = request.Hasil,
                    ServiceDate = DateOnly.FromDateTime(DateTime.UtcNow)
                };
                await _customerService.AddServiceAsync(request.AddressId, newService);
                return Ok(newService);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("customer/{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            var customer = await _customerService.GetAsync(id);
            if (customer is null)
            {
                return NotFound();
            }
            await _customerService.DeleteCustomer(id);
            return NoContent();
        }

        [HttpDelete("address")]
        public async Task<IActionResult> DeleteAddress(DeleteAddressDto request)
        {
            //var customer = await _customerService.GetAsync(customerId);
            //if (customer is null)
            //{
            //    return NotFound();
            //}
            //var address = customer.AddressList.FirstOrDefault(x => x.Id == id);
            //if (address is null)
            //{
            //    return NotFound();
            //}
            await _customerService.DeleteAddress(request.customerId, request.addressId);
            return NoContent();
        }

        [HttpDelete("service/{id}")]
        public async Task<IActionResult> DeleteService(string id)
        {
  
            await _customerService.DeleteService(id);
            return NoContent();
        }


    }
}
