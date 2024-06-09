using System.ComponentModel.DataAnnotations;

namespace OnlineBankingApp.Common.DTO.Account
{
    public class AccountCreationRequest
    {
        [Required(ErrorMessage = "The account holder's name is required")]
        public string AccountHolderName { get; set; }

        [Required(ErrorMessage = "Starting balance required")]
        [Range(0, double.MaxValue, ErrorMessage = "The starting balance must be greater than zero.")]
        public decimal InitialBalance { get; set; }
    }
}
