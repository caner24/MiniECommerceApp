using System.ComponentModel.DataAnnotations;

namespace MiniECommerceApp.WebUI.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = " Lütfen mail adresinizi boş bırakmayınız !.")]
        public string Email { get; set; }

        [Required(ErrorMessage = " Şifre Alanı zorunludur !.")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = " Şifreler ayni olmalıdır !.")]
        public string RePassword { get; set; }
    }
}
