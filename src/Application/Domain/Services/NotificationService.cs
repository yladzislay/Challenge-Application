using System;

namespace Application.Domain.Services
{
    public class NotificationService : INotifications
    {
        public void NotifyApproachingDepositLimit(string emailAddress, decimal currentDepositAmount, decimal limit)
        {
            Console.WriteLine("[Notification]: Approaching deposit limit!"
                              + Environment.NewLine
                              + $"-[Current deposit amount]: {currentDepositAmount}"
                              + Environment.NewLine
                              + $"-[Limit]: {limit} "
                              + Environment.NewLine
                              + $"-[Notifying user]: {emailAddress}");
        }

        public void NotifyExceededDepositLimit(string emailAddress, decimal currentDepositAmount, decimal limit)
        {
            Console.WriteLine("[Notification]: Exceeded deposit limit!"
                              + Environment.NewLine
                              + $"-[Current deposit amount]: {currentDepositAmount}"
                              + Environment.NewLine
                              + $"-[Limit]: {limit} "
                              + Environment.NewLine
                              + $"-[Notifying user]: {emailAddress}");
        }

        public void NotifyApproachingWithdrawLimit(string emailAddress, decimal currentWithdrawAmount, decimal limit)
        {
            Console.WriteLine("[Notification]: Approaching withdraw limit!"
                              + Environment.NewLine
                              + $"-[Current withdraw amount]: {currentWithdrawAmount}"
                              + Environment.NewLine
                              + $"-[Limit]: {limit} "
                              + Environment.NewLine
                              + $"-[Notifying user]: {emailAddress}");
        }

        public void NotifyExceededWithdrawLimit(string emailAddress, decimal currentWithdrawAmount, decimal limit)
        {
            Console.WriteLine("[Notification]: Exceeded withdraw limit!"
                              + Environment.NewLine
                              + $"-[Current withdraw amount]: {currentWithdrawAmount}"
                              + Environment.NewLine
                              + $"-[Limit]: {limit} "
                              + Environment.NewLine
                              + $"-[Notifying user]: {emailAddress}");
        }

        public void NotifyApproachingTransferLimit(string emailAddress, decimal currentTransferAmount, decimal limit)
        {
            Console.WriteLine("[Notification]: Approaching transfer limit!"
                              + Environment.NewLine
                              + $"-[Current transfer amount]: {currentTransferAmount}"
                              + Environment.NewLine
                              + $"-[Limit]: {limit} "
                              + Environment.NewLine
                              + $"-[Notifying user]: {emailAddress}");
        }

        public void NotifyExceededTransferLimit(string emailAddress, decimal currentTransferAmount, decimal limit)
        {
            Console.WriteLine("[Notification]: Exceeded transfer limit!"
                              + Environment.NewLine
                              + $"-[Current transfer amount]: {currentTransferAmount}"
                              + Environment.NewLine
                              + $"-[Limit]: {limit} "
                              + Environment.NewLine
                              + $"-[Notifying user]: {emailAddress}");
        }

        public void NotifyWithdrawalSuccess(string emailAddress, decimal amount)
        {
            Console.WriteLine("[Notification]: Withdrawal successful!"
                              + Environment.NewLine
                              + $"-[Amount withdrawn]: {amount}"
                              + Environment.NewLine
                              + $"-[Notifying user]: {emailAddress}");
        }

        public void NotifyDepositSuccess(string emailAddress, decimal amount)
        {
            Console.WriteLine("[Notification]: Deposit successful!"
                              + Environment.NewLine
                              + $"-[Amount deposited]: {amount}"
                              + Environment.NewLine
                              + $"-[Notifying user]: {emailAddress}");
        }

        public void NotifyTransferSuccessForSender(string emailAddress, decimal amount)
        {
            Console.WriteLine($"[Notification]: Transfer of {amount} was successful. Amount debited from your account.");
            Console.WriteLine($"-[Amount]: {amount}");
            Console.WriteLine($"-[Recipient email]: {emailAddress}");
        }

        public void NotifyTransferSuccessForRecipient(string emailAddress, decimal amount)
        {
            Console.WriteLine($"[Notification]: Transfer of {amount} was successful. Amount credited to your account.");
            Console.WriteLine($"-[Amount]: {amount}");
            Console.WriteLine($"-[Sender email]: {emailAddress}");
        }

        public void NotifyWithdrawalFailure(string emailAddress, string errorMessage)
        {
            Console.WriteLine("[Notification]: Withdrawal failed!"
                              + Environment.NewLine
                              + $"-[Error message]: {errorMessage}"
                              + Environment.NewLine
                              + $"-[Notifying user]: {emailAddress}");
        }

        public void NotifyDepositFailure(string emailAddress, string errorMessage)
        {
            Console.WriteLine("[Notification]: Deposit failed!"
                              + Environment.NewLine
                              + $"-[Error message]: {errorMessage}"
                              + Environment.NewLine
                              + $"-[Notifying user]: {emailAddress}");
        }

        public void NotifyTransferFailure(string emailAddress, string errorMessage)
        {
            Console.WriteLine("[Notification]: Transfer failed!"
                              + Environment.NewLine
                              + $"-[Error message]: {errorMessage}"
                              + Environment.NewLine
                              + $"-[Notifying user]: {emailAddress}");
        }
    }
}
