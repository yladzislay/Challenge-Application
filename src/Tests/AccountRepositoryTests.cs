using Application.DataAccess;
using Application.Domain;
using Xunit;

namespace Tests
{
    public class AccountRepositoryTests
    {
        [Fact]
        public void GetAccountById_ExistingAccount_ShouldReturnCorrectAccount()
        {
            // Arrange
            var accountId = Guid.NewGuid();
            var expectedAccount = new Account { Id = accountId };
            var repository = new AccountRepository(new Dictionary<Guid, Account> { { accountId, expectedAccount } });

            // Act
            var retrievedAccount = repository.GetAccountById(accountId);

            // Assert
            Assert.Equal(expectedAccount, retrievedAccount);
        }

        [Fact]
        public void GetAccountById_NonExistingAccount_ShouldReturnNull()
        {
            // Arrange
            var repository = new AccountRepository(new Dictionary<Guid, Account>());

            // Act
            var retrievedAccount = repository.GetAccountById(Guid.NewGuid());

            // Assert
            Assert.Null(retrievedAccount);
        }

        [Fact]
        public void UpdateAccount_ExistingAccount_ShouldUpdateCorrectly()
        {
            // Arrange
            var accountId = Guid.NewGuid();
            var originalAccount = new Account { Id = accountId, Balance = 100 };
            var updatedAccount = new Account { Id = accountId, Balance = 200 };
            var repository = new AccountRepository(new Dictionary<Guid, Account> { { accountId, originalAccount } });

            // Act
            repository.UpdateAccount(updatedAccount);
            var retrievedAccount = repository.GetAccountById(accountId);

            // Assert
            Assert.Equal(updatedAccount, retrievedAccount);
        }

        [Fact]
        public void UpdateAccount_NonExistingAccount_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var repository = new AccountRepository(new Dictionary<Guid, Account>());

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => repository.UpdateAccount(new Account { Id = Guid.NewGuid(), Balance = 100 }));
        }
    }
}
