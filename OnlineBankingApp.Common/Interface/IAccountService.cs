using OnlineBankingApp.Common.DTO.Account;
using OnlineBankingApp.Entity.Model;

namespace OnlineBankingApp.Common.Interface
{
    public interface IAccountService
    {   
        public Task<Account> CreateAccountAsync(AccountCreationRequest request);

        public Task<Account> GetAccountByIdAsync(int accountId);

        public Task<bool> DepositAccountAsnyc(int id, AccountUpdateRequest request);

        public Task<bool> WithdrawAccountAsnyc(int id, AccountUpdateRequest request); 
    }
}
