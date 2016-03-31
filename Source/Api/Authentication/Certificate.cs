using System;
using System.Security.Cryptography.X509Certificates;

namespace OrganizationRegister.Api.Authentication
{
    internal class Certificate
    {
        private readonly StoreName storeName;
        private readonly string thumbprint;

        public Certificate(StoreName storeName, string thumbprint)
        {
            if (string.IsNullOrEmpty(thumbprint))
            {
                throw new ArgumentException("Thumbprint cannot be null or empty.", "thumbprint");
            }

            this.storeName = storeName;
            this.thumbprint = thumbprint;
        }

        public X509Certificate2 Load()
        {
            X509Store store = null;

            try
            {
                store = new X509Store(storeName, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly);
                X509Certificate2Collection results = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, true);

                if (results.Count == 1)
                {
                    return results[0];
                }

                throw new CertificateNotFoundException("Could not find a single signing certificate matching the thumbprint.");
            }
            finally
            {
                if (store != null)
                {
                    store.Close();
                }
            }
        }
    }
}