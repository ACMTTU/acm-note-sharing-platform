using System;
using System.IO;
using ACMTTU.NoteSharing.Shared.DataContracts;

namespace ACMTTU.NoteSharing.SecretsService.Providers
{
    public class EnvironmentReader : IEnvironmentReader
    {
        public EnvironmentVariablePayload GetEnvironmentVariableFromKeyVault(string option)
        {
            EnvironmentVariablePayload payload = new EnvironmentVariablePayload();

            switch (option)
            {
                case "Database":
                    {
                        payload.Development = File.ReadAllText("/kvmnt/database-dev");
                        payload.Staging = File.ReadAllText("/kvmnt/database-staging");
                        payload.Production = File.ReadAllText("/kvmnt/database-prod");
                        break;
                    }
                case "BlobStorage":
                    {
                        payload.Development = File.ReadAllText("/kvmnt/blobstorage-dev");
                        payload.Staging = File.ReadAllText("/kvmnt/blobstorage-staging");
                        payload.Production = File.ReadAllText("/kvmnt/blobstorage-prod");
                        break;
                    }
                default:
                    {
                        throw new Exception("Unknown error has occured");
                    }
            }

            return payload;
        }
    }
}