using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Idsvr4.Attributes;
using Idsvr4.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks; 

namespace Idsvr4.Controllers
{
    [SecurityHeaders]
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService interaction;
        private readonly IEventService events;
        private readonly IHostingEnvironment environment;
        private readonly IConfiguration config;
        public HomeController(IIdentityServerInteractionService interaction, IEventService events, IHostingEnvironment environment, IConfiguration config)
        {
            this.interaction = interaction;
            this.events = events;
            this.environment = environment;
            this.config = config;
        }

        public IActionResult Index()
        {
            //if (!environment.IsDevelopment())
            //{
            //    return Redirect(config.GetValue<string>(ConstanceHelper.AppSettings.PASystemPage));
            //}

            return View();
        }

        /// <summary>
        /// Shows the error page
        /// </summary>
        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();

            // retrieve error details from identityserver
            var message = await interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                vm.Error = message;
            }

            return View("Error", vm);
        }
    }
}