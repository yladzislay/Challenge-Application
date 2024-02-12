namespace Application.Domain.Services
{
    public static class AccountOperationsLimits
    {
        public const decimal ReceiveLimit = 2000m;
        public const decimal TransferLimit = 2000m;
        public const decimal WithdrawLimit = 1000m;
        public const decimal DepositLimit = 1000m;
    }
}