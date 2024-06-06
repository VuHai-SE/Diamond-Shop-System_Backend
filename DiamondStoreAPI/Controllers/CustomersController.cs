using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Services;

namespace DiamondStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService iCustomerService;

        public CustomersController(DiamondStoreContext context)
        {
            iCustomerService = new CustomerService();
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblCustomer>>> GetTblCustomers()
        {
            var customers = iCustomerService.GetCustomers();

            if (customers == null)
            {
                return NotFound();
            }

            return customers;
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblCustomer>> GetTblCustomer(string id)
        {
            var customers = iCustomerService.GetCustomers();

            if (customers == null)
            {
                return NotFound();
            }

            var tblCustomer = iCustomerService.GetCustomer(id);

            if (tblCustomer == null)
            {
                return NotFound();
            }

            return tblCustomer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTblCustomer(string id, TblCustomer tblCustomer)
        //{
        //    if (id != tblCustomer.CustomerId)
        //    {
        //        return BadRequest();
        //    }

        //    iCustomerService.Entry(tblCustomer).State = EntityState.Modified;

        //    try
        //    {
        //        await iCustomerService.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TblCustomerExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblCustomer>> PostTblCustomer(TblCustomer tblCustomer)
        {
            var isAdd = iCustomerService.AddCustomer(tblCustomer);
            return CreatedAtAction("GetTblCustomer", new { id = tblCustomer.CustomerId }, tblCustomer);
        }

        // DELETE: api/Customers/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTblCustomer(string id)
        //{
        //    var tblCustomer = await iCustomerService.TblCustomers.FindAsync(id);
        //    if (tblCustomer == null)
        //    {
        //        return NotFound();
        //    }

        //    iCustomerService.TblCustomers.Remove(tblCustomer);
        //    await iCustomerService.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TblCustomerExists(string id)
        //{
        //    return iCustomerService.TblCustomers.Any(e => e.CustomerId == id);
        //}
    }
}
