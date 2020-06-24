
using System.ComponentModel.DataAnnotations;

namespace Idsvr4.ViewModels
{
    public class LoginInputModel
    { 
        public string UserID { get; set; } 
        public string Password { get; set; }
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; } 
    }
}