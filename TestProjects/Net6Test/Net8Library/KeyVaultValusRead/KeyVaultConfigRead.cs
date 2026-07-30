using Azure.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace Net8Library.KeyVaultValusRead
{
    public static class KeyVaultConfigRead
    {
        public static KeyVaultValueDictionary GetConfiguration(AzureKeyVaultSetting setting)
        {
            KeyVaultValueDictionary rsl = new();
            IConfigurationBuilder cfg = new ConfigurationBuilder();
            var certificateCredential = GetCertificateCredential(
                setting.TenantId,
                setting.ClientId,
                setting.CertificateThumbprint);
            cfg.AddAzureKeyVault(new Uri(setting.KeyVaultUri), certificateCredential);
            var config = cfg.Build();
            foreach (var itm in config.AsEnumerable())
            {
                rsl.AddOrUpdate(itm.Key, itm.Value);
            }
            return rsl;
        }

        private static ClientCertificateCredential GetCertificateCredential(string tenantId, string clientId, string thumbprint, bool validOnly = false)
        {
            using var certificateStore = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            certificateStore.Open(OpenFlags.ReadOnly);
            var collection = certificateStore.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, validOnly);
            var certificate = collection.OfType<X509Certificate2>().Single();
            return new ClientCertificateCredential(tenantId, clientId, certificate);
        }
    }
}
