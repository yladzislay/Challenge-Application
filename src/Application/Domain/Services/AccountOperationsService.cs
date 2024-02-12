namespace Application.Domain.Services
{
    public class AccountOperationsService : IAccountOperations
    {
        public void Withdraw(Account account, decimal amount)
        {
            if (!AccountOperationsValidator.IsPossibleToWithdraw(account, amount)) return;
            
            account.Balance -= amount;
            account.Withdrawn += amount;
        }

        public void Deposit(Account account, decimal amount)
        {
            if (!AccountOperationsValidator.IsPossibleToDeposit(account, amount)) return;
            
            account.Balance += amount;
            account.Deposited += amount;
        }

        public void Transfer(Account sender, Account recipient, decimal amount)
        {
            if (!AccountOperationsValidator.IsPossibleToTransfer(sender, recipient, amount)) return;
            
            sender.Balance -= amount;
            sender.Transferred += amount;
            recipient.Balance += amount;
            recipient.Received += amount;
        }
    }
}