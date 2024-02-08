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
        private readonly Guid _accountId = new Guid("6d97355d-748f-40a2-8443-09fcb6ec15fd");
        private const decimal InitialBalance = 1000m;
        private const string UserEmail = "test@example.com";

        private readonly Mock<IAccountRepository> _mockAccountRepository;
        private readonly Mock<INotificationService> _mockNotificationService;
        private readonly Account _account;
        private readonly WithdrawMoney _withdrawMoney;

        public Features()
        {
            _mockAccountRepository = new Mock<IAccountRepository>();
            _mockNotificationService = new Mock<INotificationService>();

            _account = new Account
            {
                Id = _accountId,
                Balance = InitialBalance,
                User = new User { Email = UserEmail }
            };
            
            _mockAccountRepository.Setup(repository => repository.GetAccountById(_accountId)).Returns(_account);
            _withdrawMoney = new WithdrawMoney(_mockAccountRepository.Object, _mockNotificationService.Object);
        }

        [Fact]
        public void WithdrawMoney_WithValidAmount_ShouldUpdateBalanceAndWithdrawn()
        {
            const decimal withdrawalAmount = 500m;
            const decimal expectedBalance = InitialBalance - withdrawalAmount;

            _withdrawMoney.Execute(_accountId, withdrawalAmount);

            _mockAccountRepository.Verify(repository => repository.Update(It.IsAny<Account>()), Times.Once);
            Assert.Equal(expectedBalance, _account.Balance);
            Assert.Equal(-withdrawalAmount, _account.Withdrawn);
        }

        [Fact]
        public void WithdrawMoney_WithInsufficientFunds_ShouldThrowException()
        {
            const decimal withdrawalAmount = 1100m;

            Assert.Throws<InvalidOperationException>(() => _withdrawMoney.Execute(_accountId, withdrawalAmount));
        }

        [Fact]
        public void WithdrawMoney_WithLowFunds_ShouldNotifyUser()
        {
            const decimal withdrawalAmount = 980m;

            _withdrawMoney.Execute(_accountId, withdrawalAmount);

            _mockNotificationService.Verify(service => service.NotifyFundsLow(UserEmail), Times.Once);
        }
    }
}