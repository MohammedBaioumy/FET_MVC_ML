using System.ComponentModel.DataAnnotations;

namespace FET_MVCforTest.Models
{
    public class ResetPasswordViewModel
    {
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = null!;


        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
