using System;
using System.Collections.Generic;
using Application.Domain;

namespace Application.DataAccess
{
    public class AccountRepository : IAccountRepository
    {
        private readonly Dictionary<Guid, Account> _accounts;

        public AccountRepository(Dictionary<Guid, Account> initialAccounts)
        {
            _accounts = initialAccounts ?? throw new ArgumentNullException(nameof(initialAccounts));
        }

        public Account GetAccountById(Guid accountId)
        {
            return _accounts.GetValueOrDefault(accountId);
        }

        public void Update(Account account)
        {
            if (_accounts.ContainsKey(account.Id))
            {
                _accounts[account.Id] = account;
            }
            else
            {
                throw new KeyNotFoundException($"Account with ID {account.Id} not found.");
            }
        }
    }
}