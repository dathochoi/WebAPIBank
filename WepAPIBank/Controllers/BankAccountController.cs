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
    public class BankAccountController : ControllerBase
    {
        private readonly BankContext _context;
        public BankAccountController (BankContext context)
        {
            _context = context;
        }
        //Get: api/BankAccount/GetBankAccount
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankAccount>>> GetBankAccount()
        {
            return await _context.BankAccounts.ToListAsync();
        }

        //Get: api/BankAccount/1
        [HttpGet("{id}")]
        public async Task<ActionResult<BankAccount>> GetBankAccount(int id)
        {
            var bank = await _context.BankAccounts.FindAsync(id);
            if (bank == null)
            {
                return NotFound();
            }
            return bank;
        }

        //Put: api/BankAccount/1
        [HttpPut("{id}")]

        public async Task<ActionResult> PutBankAccount (int id, BankAccount bankAccount)
        {
            if(id != bankAccount.BankAccountID)
            {
                return BadRequest();
            }

            _context.Entry(bankAccount).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!BankAccountExists(id))
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


        //POST: api/BankAccount
        [HttpPost]
        public async Task<ActionResult<BankAccount>> PostBankAccount (BankAccount bankAccount)
        {
            _context.BankAccounts.Add(bankAccount);
            await _context.SaveChangesAsync();
            return bankAccount;
        }

        //DELETE: api/BankAccount/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<BankAccount>> DeleteBankAcount(int id)
        {
            var bankAccount = await _context.BankAccounts.FindAsync(id);
            if(bankAccount ==null)
            {
                return NotFound();
            }
            _context.BankAccounts.Remove(bankAccount);
            await _context.SaveChangesAsync();
            return bankAccount;
        }

        private bool BankAccountExists(int id)
        {
            return _context.BankAccounts.Any(e => e.BankAccountID == id);
        }
    }
}