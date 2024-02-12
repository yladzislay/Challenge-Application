using Application.DataAccess;
using Application.Domain;
using Application.Domain.Services;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public class ScenarioTests(ITestOutputHelper testOutputHelper)
    {
        [Fact]
        public void RandomMoneyTransferUntilAllLimitsExceededScenario()
        {
            // Arrange
            var random = new Random();
            var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), Name = "User1", Email = "user1@example.com" },
                new User { Id = Guid.NewGuid(), Name = "User2", Email = "user2@example.com" },
                new User { Id = Guid.NewGuid(), Name = "User3", Email = "user3@example.com" },
                new User { Id = Guid.NewGuid(), Name = "User4", Email = "user4@example.com" }
            };

            var accounts = users.Select(user => new Account
            {
                Id = Guid.NewGuid(),
                User = user,
                Balance = 10000m,
                Transferred = 0m,
                Deposited = 0m,
                Received = 0m
            }).ToList();

            var accountRepository = new AccountRepository(accounts.ToDictionary(acc => acc.Id));
            var notifications = new NotificationService();
            var accountOperations = new AccountOperationsService(accountRepository, notifications);
            var operationsCounter = 0;

            // Act
            while (!AllLimitsExceeded(accounts))
            {
                var sender = accounts[random.Next(0, accounts.Count)];
                var recipient = accounts[random.Next(0, accounts.Count)];

                // Check if transfer between these accounts is possible
                var maxTransferAmount = Math.Min(sender.Balance, AccountOperationsLimits.TransferLimit - sender.Transferred);
                maxTransferAmount = Math.Min(maxTransferAmount, AccountOperationsLimits.ReceiveLimit - recipient.Received);

                if (maxTransferAmount > 0)
                {
                    var amount = random.Next(1, (int)maxTransferAmount);
                    accountOperations.Transfer(sender, recipient, amount);
                    operationsCounter++;
                }
            }

            // Assert
            foreach (var account in accounts) 
                Assert.True(account.Balance == 10000m + account.Deposited - account.Withdrawn + account.Received - account.Transferred,
                    $"Account balance for {account.Id} does not match the transaction history.");

            var accountsWithNotExceededLimitsCount = accounts.Count(account => (
                account.Transferred < AccountOperationsLimits.TransferLimit
                || account.Received < AccountOperationsLimits.ReceiveLimit));
            
            Assert.True(accountsWithNotExceededLimitsCount <= 1,
                    $"There are more then one ({accountsWithNotExceededLimitsCount}) accounts with positive balance when not all limits are exceeded.");
            
            // Output
            testOutputHelper.WriteLine($"Total operations performed: {operationsCounter}");
        }

        private static bool AllLimitsExceeded(IEnumerable<Account> accounts)
        {
            return accounts.All(account =>
                account is {Transferred: AccountOperationsLimits.TransferLimit, Received: AccountOperationsLimits.ReceiveLimit} 
                || account.Balance == 0);
        }
    }
}
