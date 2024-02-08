using Application.DataAccess;
using Application.Domain.Services;
using System;

namespace Application.Features
{
    public class WithdrawMoney
    {
        private IAccountRepository accountRepository;
        private INotificationService notificationService;

        public WithdrawMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            this.accountRepository = accountRepository;
            this.notificationService = notificationService;
        }

        public void Execute(Guid fromAccountId, decimal amount)
        {
            var account = this.accountRepository.GetAccountById(fromAccountId);

            var fromBalance = account.Balance - amount;
            
            if (fromBalance < 0m)
            {
                throw new InvalidOperationException("Insufficient funds to make withdraw");
            }

            if (fromBalance < 100m)
            {
                this.notificationService.NotifyFundsLow(account.User.Email);
            }

            account.Balance = account.Balance - amount;
            account.Withdrawn = account.Withdrawn - amount;

            this.accountRepository.Update(account);
        }
    }
}
