using System;
using System.Threading.Tasks;
using ACMTTU.NoteSharing.Shared.DataContracts;

namespace ACMTTU.NoteSharing.Shared.SDK.Clients
{
    public abstract class ClientFactory<T>
    {
        public virtual Task<T> GetClientAsync()
        {
            throw new Exception("Must Override");
        }

        /// <summary>
        /// Checks which namespace the service is running in to determine the
        /// correct connection string to use
        /// </summary>
        /// <returns></returns>
        protected string DetermineCorrectConnectionString(EnvironmentVariablePayload payload)
        {
            string currentNamespace = Environment.GetEnvironmentVariable("NAMESPACE");

            if (currentNamespace.Contains("prod"))
            {
                return payload.Production;
            }
            else if (currentNamespace.Contains("staging"))
            {
                return payload.Staging;
            }
            else
            {
                return payload.Development;
            }
        }
    }
}