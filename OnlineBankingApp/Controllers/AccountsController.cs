using Microsoft.AspNetCore.Mvc;
using OnlineBankingApp.Common.DTO.Account;
using OnlineBankingApp.Common.Interface;

namespace OnlineBankingApp.Controllers
{
    [Route("api/v1/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;     

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;         
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountCreationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var account = await _accountService.CreateAccountAsync(request);

            return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account);
           
            
        }

        [HttpPost("{id}/deposit")]
        public async Task<IActionResult> Deposit(int id, [FromBody] AccountUpdateRequest request)
        {
            var result = await _accountService.DepositAccountAsnyc(id, request);

            if (!result) 
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }

        [HttpPost("{id}/withdraw")]
        public async Task<IActionResult> Withdraw(int id, [FromBody] AccountUpdateRequest request)
        {
            var result = await _accountService.WithdrawAccountAsnyc(id, request);

            if (!result)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }

        [HttpGet("{id}/balance")]
        public async Task<IActionResult> GetBalance(int id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            if (account == null) return NotFound();
            return Ok(account.Balance);
        }

       

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount(int id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }
    }
}
