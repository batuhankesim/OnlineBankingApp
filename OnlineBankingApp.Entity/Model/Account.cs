using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingApp.Entity.Model
{
    public class Account
    {
        public int Id { get; set; }
        public string? AccountNumber { get; set; }
        public string? AccountHolderName { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedDate { get; set; }

        public int Version { get; set; }
    }
}
