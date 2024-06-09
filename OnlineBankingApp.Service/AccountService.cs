using OnlineBankingApp.Common.DTO.Account;
using OnlineBankingApp.Common.Interface;
using OnlineBankingApp.Entity.DbContexts;
using OnlineBankingApp.Entity.Model;
using OnlineBankingApp.Service.RabbitMQ;
using Microsoft.EntityFrameworkCore;

namespace OnlineBankingApp.Service
{
    public class AccountService : IAccountService
    {
        private readonly BankingContext _context;
        private readonly MessageProducer _messageProducer;

        public AccountService(BankingContext context, MessageProducer messageProducer)
        {
            _context = context;
            _messageProducer = messageProducer;
        }

        public async Task<Account> CreateAccountAsync(AccountCreationRequest request)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    Account newAccount = new Account()
                    {
                        Id = GenerateAccountId(),
                        AccountHolderName = request.AccountHolderName,
                        AccountNumber = GenerateAccountNumber(),
                        Balance = request.InitialBalance,
                        CreatedDate = DateTime.UtcNow,
                        Version = 1
                    };

                    _context.Accounts.Add(newAccount);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return newAccount;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<Account> GetAccountByIdAsync(int accountId)
        {
            return await _context.Accounts.FindAsync(accountId);
        }

        public async Task<bool> DepositAccountAsnyc(int id, AccountUpdateRequest request)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var account = await _context.Accounts.FindAsync(id);
                    if (account == null)
                    {
                        return false;
                    }

                    account.Version++; // Versiyon numarasını artır
                    _context.Entry(account).Property(a => a.Version).OriginalValue = request.Version;

                    account.Balance += request.Amount;
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    string message = $"Account {account.AccountNumber}: {account.Balance} deposited.";
                    _messageProducer.SendMessage(message);
                    return true;
                }
                catch (DbUpdateConcurrencyException)
                {
                    await transaction.RollbackAsync();
                    return false;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return false;
                }
            }
        }

        public async Task<bool> WithdrawAccountAsnyc(int id, AccountUpdateRequest request)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var account = await _context.Accounts.FindAsync(id);
                    if (account == null)
                    {
                        return false;
                    }

                    account.Version++; // Versiyon numarasını artır
                    _context.Entry(account).Property(a => a.Version).OriginalValue = request.Version;

                    account.Balance -= request.Amount;
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    string message = $"Account {account.AccountNumber}: {account.Balance}  Withdraw.";
                    _messageProducer.SendMessage(message);
                    return true;
                }
                catch (DbUpdateConcurrencyException)
                {
                    await transaction.RollbackAsync();
                    return false;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return false;
                }
            }
        }

        private int GenerateAccountId()
        {
            return _context.Accounts.Count() + 1;
        }

        private string GenerateAccountNumber()
        {
            return Guid.NewGuid().ToString().Substring(0, 10).ToUpper();
        } 
    }
}
