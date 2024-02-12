using System;

namespace Application.Domain.Services
{
    public class NotificationService : INotifications
    {
        private readonly static string NotificationTemplate = "[Notification]: {0}" 
                                                              + Environment.NewLine
                                                              + "---[Message]: {1}" 
                                                              + Environment.NewLine
                                                              + "--[For user]: {2}";

        private void Notify(string notification, string message, string emailAddress)
        {
            Console.WriteLine(NotificationTemplate, notification, message, emailAddress);
        }

        public void NotifyApproachingDepositLimit(string emailAddress, decimal currentDepositAmount, decimal limit)
        {
            const string notification = "Approaching deposit limit!";
            var message = $"Current deposit amount: {currentDepositAmount}, Limit: {limit}";
            Notify(notification, message, emailAddress);
        }

        public void NotifyExceededDepositLimit(string emailAddress, decimal currentDepositAmount, decimal limit)
        {
            const string notification = "Exceeded deposit limit!";
            var message = $"Current deposit amount: {currentDepositAmount}, Limit: {limit}";
            Notify(notification, message, emailAddress);
        }

        public void NotifyApproachingWithdrawLimit(string emailAddress, decimal currentWithdrawAmount, decimal limit)
        {
            const string notification = "Approaching withdraw limit!";
            var message = $"Current withdraw amount: {currentWithdrawAmount}, Limit: {limit}";
            Notify(notification, message, emailAddress);
        }

        public void NotifyExceededWithdrawLimit(string emailAddress, decimal currentWithdrawAmount, decimal limit)
        {
            const string notification = "Exceeded withdraw limit!";
            var message = $"Current withdraw amount: {currentWithdrawAmount}, Limit: {limit}";
            Notify(notification, message, emailAddress);
        }

        public void NotifyApproachingTransferLimit(string emailAddress, decimal currentTransferAmount, decimal limit)
        {
            const string notification = "Approaching transfer limit!";
            var message = $"Current transfer amount: {currentTransferAmount}, Limit: {limit}";
            Notify(notification, message, emailAddress);
        }

        public void NotifyExceededTransferLimit(string emailAddress, decimal currentTransferAmount, decimal limit)
        {
            const string notification = "Exceeded transfer limit!";
            var message = $"Current transfer amount: {currentTransferAmount}, Limit: {limit}";
            Notify(notification, message, emailAddress);
        }

        public void NotifyApproachingReceiveLimit(string emailAddress, decimal currentReceiveAmount, decimal limit)
        {
            const string notification = "Approaching receive limit!";
            var message = $"Current receive amount: {currentReceiveAmount}, Limit: {limit}";
            Notify(notification, message, emailAddress);
        }

        public void NotifyExceededReceiveLimit(string emailAddress, decimal currentReceiveAmount, decimal limit)
        {
            const string notification = "Exceeded receive limit!";
            var message = $"Current receive amount: {currentReceiveAmount}, Limit: {limit}";
            Notify(notification, message, emailAddress);
        }

        public void NotifyWithdrawalSuccess(string emailAddress, decimal amount)
        {
            const string notification = "Withdrawal successful!";
            var message = $"Amount withdrawn: {amount}";
            Notify(notification, message, emailAddress);
        }

        public void NotifyDepositSuccess(string emailAddress, decimal amount)
        {
            const string notification = "Deposit successful!";
            var message = $"Amount deposited: {amount}";
            Notify(notification, message, emailAddress);
        }

        public void NotifyTransferSuccessForSender(string emailAddress, decimal amount)
        {
            const string notification = "Transfer successful (sender)!";
            var message = $"Amount: {amount} debited from your account.";
            Notify(notification, message, emailAddress);
        }

        public void NotifyTransferSuccessForRecipient(string emailAddress, decimal amount)
        {
            const string notification = "Transfer successful (recipient)!";
            var message = $"Amount: {amount} credited to your account.";
            Notify(notification, message, emailAddress);
        }

        public void NotifyWithdrawalFailure(string emailAddress, string errorMessage)
        {
            const string notification = "Withdrawal failed!";
            Notify(notification, $"Error message: {errorMessage}", emailAddress);
        }

        public void NotifyDepositFailure(string emailAddress, string errorMessage)
        {
            const string notification = "Deposit failed!";
            Notify(notification, $"Error message: {errorMessage}", emailAddress);
        }

        public void NotifyTransferFailure(string emailAddress, string errorMessage)
        {
            const string notification = "Transfer failed!";
            Notify(notification, $"Error message: {errorMessage}", emailAddress);
        }
    }
}
