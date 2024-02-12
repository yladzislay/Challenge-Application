namespace Application.Domain.Services
{
    public interface INotifications
    {
        void NotifyApproachingDepositLimit(string emailAddress, decimal currentDepositAmount, decimal limit);
        void NotifyExceededDepositLimit(string emailAddress, decimal currentDepositAmount, decimal limit);

        void NotifyApproachingWithdrawLimit(string emailAddress, decimal currentWithdrawAmount, decimal limit);
        void NotifyExceededWithdrawLimit(string emailAddress, decimal currentWithdrawAmount, decimal limit);

        void NotifyApproachingTransferLimit(string emailAddress, decimal currentTransferAmount, decimal limit);
        void NotifyExceededTransferLimit(string emailAddress, decimal currentTransferAmount, decimal limit);

        void NotifyWithdrawalSuccess(string emailAddress, decimal amount);
        void NotifyDepositSuccess(string emailAddress, decimal amount);
        
        void NotifyTransferSuccessForSender(string emailAddress, decimal amount);
        void NotifyTransferSuccessForRecipient(string emailAddress, decimal amount);

        void NotifyWithdrawalFailure(string emailAddress, string errorMessage);
        void NotifyDepositFailure(string emailAddress, string errorMessage);
        void NotifyTransferFailure(string emailAddress, string errorMessage);
    }
}