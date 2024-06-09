using System.ComponentModel.DataAnnotations;

namespace OnlineBankingApp.Common.DTO.Account
{
    public class AccountUpdateRequest
    {
        [Required]
        public int Amount { get; set; }
        [Required]
        public int Version { get; set; }
    }
}
