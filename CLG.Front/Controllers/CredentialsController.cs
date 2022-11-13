using CLG.Core.Contracts;
using CLG.Front.Models;
using Microsoft.AspNetCore.Mvc;

namespace CLG.Front.Controllers
{
    public class CredentialsController : Controller
    {
        private readonly ILogger<CredentialsController> _logger;
        private readonly ICredentialService credentialService;


        public CredentialsController(ILogger<CredentialsController> logger, ICredentialService _credentialService)
        {
            _logger = logger;
            credentialService = _credentialService;
        }

        [HttpGet]
        [Route("Credentials/Get/{key}")]
        public async Task<IActionResult> Get(string key)
        {
            try
            {
                var credentialsModel = await credentialService.ReadCredentials(key);

                CredentialsViewModel model = new CredentialsViewModel
                {
                    Name = credentialsModel.Name,
                    Password = credentialsModel.Password,
                };

                return View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return View("Error", new ErrorViewModel{Errors = e.Message});
            }

        }

    }
}