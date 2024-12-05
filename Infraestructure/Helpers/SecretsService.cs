using Amazon.SecretsManager.Model;
using Amazon.SecretsManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Amazon;

namespace Infraestructure.Helpers
{
    public class SecretsService
    {
        private readonly IAmazonSecretsManager _secretsManager;

        public SecretsService()
        {
            _secretsManager = new AmazonSecretsManagerClient(RegionEndpoint.USEast1); ;
        }

        public async Task<AppSecrets> GetAppSecretsAsync()
        {
            string secretName = "VITAL_BACKEND_ENVS";
            GetSecretValueRequest request = new GetSecretValueRequest
            {
                SecretId = secretName,
                VersionStage = "AWSCURRENT",
            };
            GetSecretValueResponse response = await _secretsManager.GetSecretValueAsync(request);

            try
            {
                return JsonSerializer.Deserialize<AppSecrets>(response.SecretString);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"Erro ao desserializar o segredo {secretName}.", ex);
            }
        }
    }

}
