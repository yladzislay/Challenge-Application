using Application.DataAccess;
using Application.Domain;
using Application.Domain.Services;
using Application.Features;
using Moq;
using Xunit;

namespace Tests
{
    public class Features
    {
        [Fact]
        public void WithdrawMoney_WithValidAmount_ShouldUpdateBalanceAndWithdrawn()
        {
            var accountId = new System.Guid("6d97355d-748f-40a2-8443-09fcb6ec15fd");
            var initialBalance = 1000m;
            var withdrawalAmount = 500m;
            var expectedBalance = initialBalance - withdrawalAmount;

            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockNotificationService = new Mock<INotificationService>();

            var account = new Account
            {
                Id = accountId,
                Balance = initialBalance,
                User = new User {Email = "test@example.com"}
            };

            mockAccountRepository.Setup(repository => repository.GetAccountById(accountId)).Returns(account);

            var withdrawMoney = new WithdrawMoney(mockAccountRepository.Object, mockNotificationService.Object);

            withdrawMoney.Execute(accountId, withdrawalAmount);

            mockAccountRepository.Verify(repository => repository.Update(It.IsAny<Account>()), Times.Once);
            Assert.Equal(expectedBalance, account.Balance);
            Assert.Equal(-withdrawalAmount, account.Withdrawn);
        }

        [Fact]
        public void WithdrawMoney_WithInsufficientFunds_ShouldThrowException()
        {
            var accountId = new System.Guid("6d97355d-748f-40a2-8443-09fcb6ec15fd");
            var initialBalance = 100m;
            var withdrawalAmount = 500m;

            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockNotificationService = new Mock<INotificationService>();

            var account = new Account
            {
                Id = accountId,
                Balance = initialBalance,
                User = new User {Email = "test@example.com"}
            };

            mockAccountRepository.Setup(repository => repository.GetAccountById(accountId)).Returns(account);

            var withdrawMoney = new WithdrawMoney(mockAccountRepository.Object, mockNotificationService.Object);

            Assert.Throws<InvalidOperationException>(() => withdrawMoney.Execute(accountId, withdrawalAmount));
        }

        [Fact]
        public void WithdrawMoney_WithLowFunds_ShouldNotifyUser()
        {
            var accountId = new System.Guid("6d97355d-748f-40a2-8443-09fcb6ec15fd");
            var initialBalance = 150m;
            var withdrawalAmount = 100m;

            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockNotificationService = new Mock<INotificationService>();

            var account = new Account
            {
                Id = accountId,
                Balance = initialBalance,
                User = new User {Email = "test@example.com"}
            };

            mockAccountRepository.Setup(repository => repository.GetAccountById(accountId)).Returns(account);

            var withdrawMoney = new WithdrawMoney(mockAccountRepository.Object, mockNotificationService.Object);

            withdrawMoney.Execute(accountId, withdrawalAmount);

            mockNotificationService.Verify(service => service.NotifyFundsLow("test@example.com"), Times.Once);
        }
    }
}