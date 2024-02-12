using Application.Domain;
using Application.Domain.Services;
using Xunit;

namespace Tests
{
    public class AccountOperationsTests
    {
        [Fact]
        public void Withdraw_ValidAmount_ShouldUpdateBalanceAndWithdrawn()
        {
            // Arrange
            var account = new Account { Balance = 1000m, Withdrawn = 0m };
            var accountOperations = new AccountOperationsService();

            // Act
            accountOperations.Withdraw(account, 500m);

            // Assert
            Assert.Equal(500m, account.Balance);
            Assert.Equal(500m, account.Withdrawn);
        }

        [Fact]
        public void Withdraw_InsufficientFunds_ShouldNotUpdateBalanceAndWithdrawn()
        {
            // Arrange
            var account = new Account { Balance = 1000m, Withdrawn = 0m };
            var accountOperations = new AccountOperationsService();

            // Act
            accountOperations.Withdraw(account, 1500m);

            // Assert
            Assert.Equal(1000m, account.Balance);
            Assert.Equal(0m, account.Withdrawn);
        }

        [Fact]
        public void Deposit_ValidAmount_ShouldUpdateBalanceAndDeposited()
        {
            // Arrange
            var account = new Account { Balance = 1000m, Deposited = 0m };
            var accountOperations = new AccountOperationsService();

            // Act
            accountOperations.Deposit(account, 500m);

            // Assert
            Assert.Equal(1500m, account.Balance);
            Assert.Equal(500m, account.Deposited);
        }

        [Fact]
        public void Transfer_ValidAmount_ShouldUpdateBalancesAndTransfers()
        {
            // Arrange
            var senderAccount = new Account { Balance = 1000m, Transferred = 0m };
            var recipientAccount = new Account { Balance = 500m, Received = 0m };
            var accountOperations = new AccountOperationsService();

            // Act
            accountOperations.Transfer(senderAccount, recipientAccount, 300m);

            // Assert
            Assert.Equal(700m, senderAccount.Balance);
            Assert.Equal(800m, recipientAccount.Balance);
            Assert.Equal(300m, senderAccount.Transferred);
            Assert.Equal(300m, recipientAccount.Received);
        }

        [Fact]
        public void Transfer_InsufficientFunds_ShouldNotUpdateBalancesAndTransfers()
        {
            // Arrange
            var senderAccount = new Account { Balance = 1000m, Transferred = 0m };
            var recipientAccount = new Account { Balance = 500m, Received = 0m };
            var accountOperations = new AccountOperationsService();

            // Act
            accountOperations.Transfer(senderAccount, recipientAccount, 1500m);

            // Assert
            Assert.Equal(1000m, senderAccount.Balance);
            Assert.Equal(500m, recipientAccount.Balance);
            Assert.Equal(0m, senderAccount.Transferred);
            Assert.Equal(0m, recipientAccount.Received);
        }
    }
}
