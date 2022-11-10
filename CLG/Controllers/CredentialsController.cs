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

        [HttpGet("{key}")]
        public async Task<CredentialsModel> Get(string key)
        {
            CredentialsModel credentialsModel;
            
            try
            { 
                credentialsModel = await credentialService.ReadCredentials(key);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }


            return credentialsModel;
        }

        [HttpPost]
        public async Task<string> Add(string inputJson)
        {
            string key = await credentialService.SaveCredentials(inputJson);
            string uri = $"{Request.Host}/Get/{key}";

            return uri;
        }
    }
}