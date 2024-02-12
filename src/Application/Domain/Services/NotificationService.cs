using System;

namespace Application.Domain.Services
{
    public class NotificationService : INotifications
    {
        private readonly static string NotificationTemplate = "[Notification]: {0}" 
                                                              + Environment.NewLine
                                                              + "---[Details]: {1}" 
                                                              + Environment.NewLine
                                                              + "--[For user]: {2}";

        public void NotifyApproachingDepositLimit(string emailAddress, decimal currentDepositAmount, decimal limit)
        {
            var message = $"Approaching deposit limit! Current deposit amount: {currentDepositAmount}, Limit: {limit}";
            Console.WriteLine(NotificationTemplate, message, emailAddress);
        }

        public void NotifyExceededDepositLimit(string emailAddress, decimal currentDepositAmount, decimal limit)
        {
            var message = $"Exceeded deposit limit! Current deposit amount: {currentDepositAmount}, Limit: {limit}";
            Console.WriteLine(NotificationTemplate, message, emailAddress);
        }

        public void NotifyApproachingWithdrawLimit(string emailAddress, decimal currentWithdrawAmount, decimal limit)
        {
            var message = $"Approaching withdraw limit! Current withdraw amount: {currentWithdrawAmount}, Limit: {limit}";
            Console.WriteLine(NotificationTemplate, message, emailAddress);
        }

        public void NotifyExceededWithdrawLimit(string emailAddress, decimal currentWithdrawAmount, decimal limit)
        {
            var message = $"Exceeded withdraw limit! Current withdraw amount: {currentWithdrawAmount}, Limit: {limit}";
            Console.WriteLine(NotificationTemplate, message, emailAddress);
        }

        public void NotifyApproachingTransferLimit(string emailAddress, decimal currentTransferAmount, decimal limit)
        {
            var message = $"Approaching transfer limit! Current transfer amount: {currentTransferAmount}, Limit: {limit}";
            Console.WriteLine(NotificationTemplate, message, emailAddress);
        }

        public void NotifyExceededTransferLimit(string emailAddress, decimal currentTransferAmount, decimal limit)
        {
            var message = $"Exceeded transfer limit! Current transfer amount: {currentTransferAmount}, Limit: {limit}";
            Console.WriteLine(NotificationTemplate, message, emailAddress);
        }

        public void NotifyApproachingReceiveLimit(string emailAddress, decimal currentReceiveAmount, decimal limit)
        {
            var message = $"Approaching receive limit! Current receive amount: {currentReceiveAmount}, Limit: {limit}";
            Console.WriteLine(NotificationTemplate, message, emailAddress);
        }

        public void NotifyExceededReceiveLimit(string emailAddress, decimal currentReceiveAmount, decimal limit)
        {
            var message = $"Exceeded receive limit! Current receive amount: {currentReceiveAmount}, Limit: {limit}";
            Console.WriteLine(NotificationTemplate, message, emailAddress);
        }

        public void NotifyWithdrawalSuccess(string emailAddress, decimal amount)
        {
            var message = $"Withdrawal successful! Amount withdrawn: {amount}";
            Console.WriteLine(NotificationTemplate, message, emailAddress);
        }

        public void NotifyDepositSuccess(string emailAddress, decimal amount)
        {
            var message = $"Deposit successful! Amount deposited: {amount}";
            Console.WriteLine(NotificationTemplate, message, emailAddress);
        }

        public void NotifyTransferSuccessForSender(string emailAddress, decimal amount)
        {
            var message = $"Transfer of {amount} was successful. Amount debited from your account.";
            Console.WriteLine(NotificationTemplate, message, emailAddress);
        }

        public void NotifyTransferSuccessForRecipient(string emailAddress, decimal amount)
        {
            var message = $"Transfer of {amount} was successful. Amount credited to your account.";
            Console.WriteLine(NotificationTemplate, message, emailAddress);
        }

        public void NotifyWithdrawalFailure(string emailAddress, string errorMessage)
        {
            var message = $"Withdrawal failed! Error message: {errorMessage}";
            Console.WriteLine(NotificationTemplate, message, emailAddress);
        }

        public void NotifyDepositFailure(string emailAddress, string errorMessage)
        {
            var message = $"Deposit failed! Error message: {errorMessage}";
            Console.WriteLine(NotificationTemplate, message, emailAddress);
        }

        public void NotifyTransferFailure(string emailAddress, string errorMessage)
        {
            var message = $"Transfer failed! Error message: {errorMessage}";
            Console.WriteLine(NotificationTemplate, message, emailAddress);
        }
    }
}
