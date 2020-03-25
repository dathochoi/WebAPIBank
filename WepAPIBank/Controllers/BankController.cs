using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WepAPIBank.Model;

namespace WepAPIBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly BankContext _context;

        public BankController( BankContext context)
        {
            _context = context;
        }

        //GET: api/Bank
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bank>>> GetBanks()
        {
            return await _context.Banks.ToListAsync();
        }

        //GET: api/Bank/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Bank>> GetBank(int id)
        {
            var bank = await _context.Banks.FindAsync(id);
            if (bank ==null)
            {
                return NotFound();
            }
            return bank;
        }

        //PUT: api/Bank/1
        public async Task<ActionResult<Bank>> PutBank(int id,Bank bank)
        {
            if (bank.BankID != id)
            {
                return BadRequest();
            }

            _context.Entry(bank).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!_context.Banks.Any(e => e.BankID ==id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //POST: api/Bank
        [HttpPost]
        public async Task<ActionResult<Bank>> PostBank(Bank bank)
        {
            _context.Banks.Add(bank);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetBank", new { id = bank.BankID }, bank);
        }

        //DELETE: api/Bank/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bank>> DeleteBank(int id)
        {
            var bank = await _context.Banks.FindAsync(id);
            if (bank ==null)
            {
                return NotFound();
            }
            _context.Banks.Remove(bank);
            await _context.SaveChangesAsync();
            return bank;
        }
    }
}