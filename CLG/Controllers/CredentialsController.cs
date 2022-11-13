using System.Security.Cryptography;
using CLG.Core.Contracts;
using CLG.Core.Models;
using CLG.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace CLG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CredentialsController : ControllerBase
    {
        private readonly ICredentialService credentialService;

        public CredentialsController(ICredentialService _credentialService)
        {
            credentialService = _credentialService;
        }
        
        [HttpPost]
        public async Task<string> Add(string inputJson)
        {
            string key = await credentialService.SaveCredentials(inputJson);
            string uri = $"https://{Request.Host.Host}:7270/Credentials/Get/{key}";

            return uri;
        }
    }
}