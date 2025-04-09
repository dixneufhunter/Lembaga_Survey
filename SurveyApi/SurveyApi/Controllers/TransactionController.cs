using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.DTO.NewFolder2;
using RestaurantAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public TransactionController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }





        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await _dbContext.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _dbContext.Transactions.Remove(transaction);

            await _dbContext.SaveChangesAsync();

            return Ok("Data Transaction has been deleted");
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetDetailTransaction(int id)
        {

            var transaction = await _dbContext.Transactions
                .Include(a => a.Food).Include(a => a.Customer).FirstOrDefaultAsync(t=>t.transaction_id == id);

            if (transaction == null)
            {
                return NotFound(); // Jika tidak ada transaksi dengan ID yang diberikan, kembalikan NotFound
            }

            return transaction;


        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetAllTransactions()
        {
            var transactions = await _dbContext.Transactions
                .Include(a => a.Food)
                .Include(a => a.Customer)
                .ToListAsync();

            var jsonSerializerOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            return Ok(JsonSerializer.Serialize(transactions, jsonSerializerOptions));

        }


        [HttpPost]
        public async Task<ActionResult> CreateTransaction([FromBody] CreateTransactionDTO createTransactionDTO) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }



            Transaction transaction = new Transaction
            {
                food_id = createTransactionDTO.food_id,
                customer_id = createTransactionDTO.customer_id,
                transaction_date = createTransactionDTO.transaction_date,
                amount = createTransactionDTO.amount
            };


            _dbContext.Transactions.Add(transaction);
            await _dbContext.SaveChangesAsync();

            return Ok(transaction);
        }

    










    }
}
