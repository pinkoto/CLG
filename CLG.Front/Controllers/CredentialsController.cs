using CLG.Front.Models;
using Microsoft.AspNetCore.Mvc;

namespace CLG.Front.Controllers
{
    public class CredentialsController : Controller
    {
        private readonly ILogger<CredentialsController> _logger;
        private readonly CLG.Controllers.CredentialsController credentialsController;


        public CredentialsController(ILogger<CredentialsController> logger, CLG.Controllers.CredentialsController _credentialsController)
        {
            _logger = logger;
            credentialsController = _credentialsController;
        }

        [HttpGet("{key}")]
        public IActionResult Get(string key)
        {
            var apiModel = credentialsController.Get(key);

            CredentialsViewModel model = new CredentialsViewModel
            {
                Name = apiModel.Result.Name,
                Password = apiModel.Result.Password,
            };

            return View(model);
        }

    }
}