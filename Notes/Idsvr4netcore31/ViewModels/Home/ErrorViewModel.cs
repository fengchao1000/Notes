
using IdentityServer4.Models;

namespace Idsvr4.ViewModels
{
    public class ErrorViewModel
    {
        public ErrorMessage Error { get; set; } 
        public string ClientIP { get; set; }
        public string LocalIP { get; set; }
    }
}