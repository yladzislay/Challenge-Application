namespace Application.Domain.Services
{
    public interface IAccountOperations
    {
        void Withdraw(Account account, decimal amount);
        void Deposit(Account account, decimal amount);
        void Transfer(Account sender, Account recipient, decimal amount);
    }
}