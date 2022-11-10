using CLG.Core.Contracts;
using CLG.Core.Models;
using CLG.Infrastructure.Data.Common;
using CLG.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace CLG.Core.Services
{
    public class CredentialService : ICredentialService
    {
        private readonly IRepository repo;

        public CredentialService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<string> SaveCredentials(string inputJson)
        {
            string key = GetUniqueKey(20);

            CredentialsModel model = JsonConvert.DeserializeObject<CredentialsModel>(inputJson);

            Credentials credentials = new Credentials
            {
                Name = model.Name,
                Password = model.Password,
                ExpiryDate = DateTime.UtcNow + TimeSpan.FromMinutes(30),
                Seen = false,
                Key = key
            };

            await repo.AddAsync(credentials);
            await repo.SaveChangesAsync();

            return key;
        }

        public async Task<CredentialsModel> ReadCredentials(string key)
        {
            var DbCredentials = await repo.All<Credentials>()
                .FirstOrDefaultAsync(c => c.Key == key);

            if (DbCredentials == null)
            {
                throw new NullReferenceException("Invalid key");
            }

            if (DbCredentials.Seen)
            {
                throw new ValidationException("Credentials already shown");
            }

            if (DbCredentials.ExpiryDate < DateTime.UtcNow)
            {
                throw new TimeoutException("Key expired");
            }

            CredentialsModel model = new CredentialsModel
            {
                Name = DbCredentials.Name,
                Password = DbCredentials.Password
            };

            DbCredentials.Seen = true;
            await repo.SaveChangesAsync();

            return model;
        }


        internal static readonly char[] chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        public static string GetUniqueKey(int size)
        {
            byte[] data = new byte[4 * size];
            using (var crypto = RandomNumberGenerator.Create())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;

                result.Append(chars[idx]);
            }

            return result.ToString();
        }
    }
}
