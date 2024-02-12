namespace Application.Domain.Services
{
    public static class AccountOperationsValidator
    {
        private static bool HasSufficientBalance(Account account, decimal amount) 
            => account.Balance >= amount;

        private static bool IsNotExceedingWithdrawLimit(Account account, decimal amount) 
            => account.Withdrawn + amount <= AccountOperationsLimits.WithdrawLimit;

        private static bool IsNotExceedingDepositLimit(Account account, decimal amount) 
            => account.Deposited + amount <= AccountOperationsLimits.DepositLimit;

        private static bool IsNotExceedingTransferLimit(Account sender, decimal amount) 
            => sender.Transferred + amount <= AccountOperationsLimits.TransferLimit;

        private static bool IsNotExceedingReceiveLimit(Account recipient, decimal amount) 
            => recipient.Received + amount <= AccountOperationsLimits.ReceiveLimit;

        public static bool IsPossibleToWithdraw(Account account, decimal amount) 
            => HasSufficientBalance(account, amount) 
               && IsNotExceedingWithdrawLimit(account, amount);

        public static bool IsPossibleToDeposit(Account account, decimal amount) 
            => IsNotExceedingDepositLimit(account, amount);

        public static bool IsPossibleToTransfer(Account sender, Account recipient, decimal amount) 
            => HasSufficientBalance(sender, amount) 
               && IsNotExceedingTransferLimit(sender, amount) 
               && IsNotExceedingReceiveLimit(recipient, amount);
        
        public static bool IsApproachingWithdrawLimit(Account account)
        {
            return account.Withdrawn >= AccountOperationsLimits.WithdrawLimit * 0.8m;
        }

        public static bool IsApproachingDepositLimit(Account account)
        {
            return account.Deposited >= AccountOperationsLimits.DepositLimit * 0.8m;
        }

        public static bool IsApproachingTransferLimit(Account account)
        {
            return account.Transferred >= AccountOperationsLimits.TransferLimit * 0.8m;
        }

        public static bool IsApproachingReceiveLimit(Account account)
        {
            return account.Received >= AccountOperationsLimits.ReceiveLimit * 0.8m;
        }
    }
}