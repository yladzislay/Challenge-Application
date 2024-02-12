using System;
using Application.DataAccess;

namespace Application.Domain.Services
{
    public class AccountOperationsService : IAccountOperations
    {
        private readonly IAccountRepository _accountRepository;
        private readonly INotifications _notifications;

        public AccountOperationsService(IAccountRepository accountRepository, INotifications notifications)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _notifications = notifications ?? throw new ArgumentNullException(nameof(notifications));
        }

        public void Withdraw(Account account, decimal amount)
        {
            if (AccountOperationsValidator.IsPossibleToWithdraw(account, amount)) ProcessWithdrawal(account, amount);
            else _notifications.NotifyWithdrawalFailure(account.User.Email, "Insufficient funds or withdrawal limit exceeded.");
        }

        public void Deposit(Account account, decimal amount)
        {
            if (AccountOperationsValidator.IsPossibleToDeposit(account, amount)) ProcessDeposit(account, amount);
            else _notifications.NotifyDepositFailure(account.User.Email, "Deposit limit exceeded.");
        }

        public void Transfer(Account sender, Account recipient, decimal amount)
        {
            if (AccountOperationsValidator.IsPossibleToTransfer(sender, recipient, amount)) ProcessTransfer(sender, recipient, amount);
            else _notifications.NotifyTransferFailure(sender.User.Email, "Transfer failed due to limits or insufficient funds.");
        }

        private void ProcessWithdrawal(Account account, decimal amount)
        {
            account.Balance -= amount;
            account.Withdrawn += amount;
            
            _accountRepository.UpdateAccount(account);
            
            _notifications.NotifyWithdrawalSuccess(account.User.Email, amount);
            
            if (AccountOperationsValidator.IsApproachingWithdrawLimit(account)) 
                _notifications.NotifyApproachingWithdrawLimit(
                    account.User.Email, 
                    account.Withdrawn, 
                    AccountOperationsLimits.WithdrawLimit);
        }

        private void ProcessDeposit(Account account, decimal amount)
        {
            account.Balance += amount;
            account.Deposited += amount;
            
            _accountRepository.UpdateAccount(account);
            
            _notifications.NotifyDepositSuccess(account.User.Email, amount);
            
            if (AccountOperationsValidator.IsApproachingDepositLimit(account)) 
                _notifications
                    .NotifyApproachingDepositLimit(
                        account.User.Email, 
                        account.Deposited, 
                        AccountOperationsLimits.DepositLimit);
        }

        private void ProcessTransfer(Account sender, Account recipient, decimal amount)
        {
            sender.Balance -= amount;
            sender.Transferred += amount;
            
            recipient.Balance += amount;
            recipient.Received += amount;
            
            _accountRepository.UpdateAccount(sender);
            _accountRepository.UpdateAccount(recipient);
            
            _notifications.NotifyTransferSuccessForSender(sender.User.Email, amount);
            _notifications.NotifyTransferSuccessForRecipient(recipient.User.Email, amount);
            
            if (AccountOperationsValidator.IsApproachingTransferLimit(sender)) 
                _notifications.NotifyApproachingTransferLimit(
                    sender.User.Email, 
                    sender.Transferred, 
                    AccountOperationsLimits.TransferLimit);
            
            if (AccountOperationsValidator.IsApproachingReceiveLimit(sender)) 
                _notifications
                    .NotifyApproachingReceiveLimit(
                    sender.User.Email, 
                    sender.Transferred, 
                    AccountOperationsLimits.TransferLimit);
        }
    }
}