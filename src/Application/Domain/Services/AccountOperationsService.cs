using System;
using Application.DataAccess;

namespace Application.Domain.Services
{
    public class AccountOperationsService : IAccountOperations
    {
        private readonly IAccountRepository _accountRepository;

        public AccountOperationsService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        public void Withdraw(Account account, decimal amount)
        {
            if (!AccountOperationsValidator.IsPossibleToWithdraw(account, amount)) return;

            account.Balance -= amount;
            account.Withdrawn += amount;
            _accountRepository.UpdateAccount(account);
        }

        public void Deposit(Account account, decimal amount)
        {
            if (!AccountOperationsValidator.IsPossibleToDeposit(account, amount)) return;

            account.Balance += amount;
            account.Deposited += amount;
            _accountRepository.UpdateAccount(account);
        }

        public void Transfer(Account sender, Account recipient, decimal amount)
        {
            if (!AccountOperationsValidator.IsPossibleToTransfer(sender, recipient, amount)) return;

            sender.Balance -= amount;
            sender.Transferred += amount;
            recipient.Balance += amount;
            recipient.Received += amount;
            _accountRepository.UpdateAccount(sender);
            _accountRepository.UpdateAccount(recipient);
        }
    }
}