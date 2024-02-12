using System;

namespace Application.Domain
{
    public class Account
    {
        public Guid Id { get; set; }
        public User User { get; set; }

        public decimal Balance { get; set; }
        
        public decimal Withdrawn { get; set; }
        public decimal Deposited { get; set; }
        public decimal Received { get; set; }
        public decimal Transferred { get; set; }
    }
}
