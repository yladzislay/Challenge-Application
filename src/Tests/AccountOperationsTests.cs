using Application.DataAccess;
using Application.Domain;
using Application.Domain.Services;
using Moq;
using Xunit;

namespace Tests;

public class AccountOperationsServiceTests
{
    private readonly Mock<IAccountRepository> _mockAccountRepository;
    private readonly IAccountOperations _accountOperations;

    public AccountOperationsServiceTests()
    {
        _mockAccountRepository = new Mock<IAccountRepository>();
        Mock<INotifications> mockNotificationsService = new();
        _accountOperations = new AccountOperationsService(_mockAccountRepository.Object, mockNotificationsService.Object);
    }

    [Fact]
    public void Withdraw_ValidAmount_ShouldUpdateBalanceAndWithdrawn()
    {
        // Arrange
        var account = new Account { Balance = 1000m, Withdrawn = 0m };
        const decimal amount = 500m;

        // Act
        _accountOperations.Withdraw(account, amount);

        // Assert
        Assert.Equal(500m, account.Balance);
        Assert.Equal(500m, account.Withdrawn);
        _mockAccountRepository.Verify(r => r.UpdateAccount(account), Times.Once);
    }

    [Fact]
    public void Deposit_ValidAmount_ShouldUpdateBalanceAndDeposited()
    {
        // Arrange
        var account = new Account { Balance = 1000m, Deposited = 0m };
        const decimal amount = 500m;

        // Act
        _accountOperations.Deposit(account, amount);

        // Assert
        Assert.Equal(1500m, account.Balance);
        Assert.Equal(500m, account.Deposited);
        _mockAccountRepository.Verify(r => r.UpdateAccount(account), Times.Once);
    }

    [Fact]
    public void Transfer_ValidAmount_ShouldUpdateBalancesAndTransfers()
    {
        // Arrange
        var senderAccount = new Account { Balance = 1000m, Transferred = 0m };
        var recipientAccount = new Account { Balance = 500m, Received = 0m };
        const decimal amount = 300m;

        // Act
        _accountOperations.Transfer(senderAccount, recipientAccount, amount);

        // Assert
        Assert.Equal(700m, senderAccount.Balance);
        Assert.Equal(800m, recipientAccount.Balance);
        Assert.Equal(300m, senderAccount.Transferred);
        Assert.Equal(300m, recipientAccount.Received);
        _mockAccountRepository.Verify(r => r.UpdateAccount(senderAccount), Times.Once);
        _mockAccountRepository.Verify(r => r.UpdateAccount(recipientAccount), Times.Once);
    }
}