using CustomerDataPlatform.Dtos;
using CustomerDataPlatform.Models;
using CustomerDataPlatform.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ZstdSharp.Unsafe;

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
            //if(string.IsNullOrEmpty(request.Name)||string.IsNullOrEmpty(request.PhoneNumber))return BadRequest("Name and PhoneNumber cannot be empty");
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

        [HttpPut("customer/{id}")]
        public async Task<IActionResult> UpdateCustomer(string id, UpdateCustomerDto request)
        {
            var customer = await _customerService.GetAsync(id);
            if (customer is null)
            {
                return NotFound();
            }
            await _customerService.UpdateCustomer(id,request);
            return NoContent();
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

        [HttpPut("address/{id}")]
        public async Task<IActionResult> UpdateAddress(string id, UpdateAddressDto request)
        {
            await _customerService.UpdateAddress(id, request);
            return Ok();
        }

        [HttpDelete("address/{id}")]
        public async Task<IActionResult> DeleteAddress(string id)
        {
            try
            {
                await _customerService.DeleteAddress(id);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest("Address not found");
                //throw;
            }

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

        [HttpPut("service/{id}")]
        public async Task<IActionResult> UpdateService(string id, UpdateServiceDto request)
        {
            try
            {
                await _customerService.UpdateService(id, request);
                return NoContent();

            }
            catch (Exception)
            {
                return BadRequest("cant update service");
            }
        }

        [HttpDelete("service/{id}")]
        public async Task<IActionResult> DeleteService(string id)
        {
            try
            {
                await _customerService.DeleteService(id);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest("Service not found");
            }
            
        }


    }
}
