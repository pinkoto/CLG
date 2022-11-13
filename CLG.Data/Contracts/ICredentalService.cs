using CLG.Core.Models;

namespace CLG.Core.Contracts
{
    public interface ICredentialService
    {
        /// <summary>
        /// Receives an object, saves it to DB and returns the key to access it
        /// </summary>
        /// <param name="inputJson"></param>
        /// <returns>key</returns>
        Task<string> SaveCredentials(string inputJson); 

        Task<CredentialsModel> ReadCredentials(string key);
    }
}
