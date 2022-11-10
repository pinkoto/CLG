using CLG.Core.Models;

namespace CLG.Core.Contracts
{
    public interface ICredentialService
    {
        Task<string> SaveCredentials(string inputJson); 

        Task<CredentialsModel> ReadCredentials(string key);
    }
}
