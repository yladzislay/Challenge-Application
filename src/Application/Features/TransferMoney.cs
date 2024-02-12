using Application.Domain;
using Application.DataAccess;
using Application.Domain.Services;
using System;

namespace Application.Features
{
    public class TransferMoney
    {
        private readonly IAccountRepository _accountRepository;
        private readonly INotificationService _notificationService;

        public TransferMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            _accountRepository = accountRepository;
            _notificationService = notificationService;
        }

        public void Execute(Guid senderAccountId, Guid recipientAccountId, decimal transferAmount)
        {
            var senderAccount = _accountRepository.GetAccountById(senderAccountId);
            var recipientAccount = _accountRepository.GetAccountById(recipientAccountId);

            var fromBalance = senderAccount.Balance - transferAmount;
            
            if (fromBalance < 0m)
            {
                throw new InvalidOperationException("Insufficient funds to make transfer");
            }

            if (fromBalance < 500m)
            {
                _notificationService.NotifyFundsLow(senderAccount.User.Email);
            }

            var paidIn = recipientAccount.Transferred + transferAmount;
            if (paidIn > AccountOperationsLimits.TransferLimit)
            {
                throw new InvalidOperationException("Account pay in limit reached");
            }

            if (AccountOperationsLimits.ReceiveLimit - paidIn < 500m)
            {
                _notificationService.NotifyApproachingPayInLimit(recipientAccount.User.Email);
            }

            senderAccount.Balance = senderAccount.Balance - transferAmount;
            senderAccount.Withdrawn = senderAccount.Withdrawn - transferAmount;

            recipientAccount.Balance = recipientAccount.Balance + transferAmount;
            recipientAccount.Transferred = recipientAccount.Transferred + transferAmount;

            _accountRepository.Update(senderAccount);
            _accountRepository.Update(recipientAccount);
        }
    }
}
