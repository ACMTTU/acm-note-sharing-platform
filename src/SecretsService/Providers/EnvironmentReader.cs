using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using ACMTTU.NoteSharing.Shared.DataContracts;
using ACMTTU.NoteSharing.SecretsService.Models;
using Newtonsoft.Json;

namespace ACMTTU.NoteSharing.SecretsService.Providers
{
    public static class EnvironmentReader
    {
        public static EnvironmentVariablePayload GetEnvironmentVariableFromKeyVault(EnvironmentVariableOptions option)
        {
            EnvironmentVariablePayload payload = new EnvironmentVariablePayload();

            switch (option)
            {
                case EnvironmentVariableOptions.Database:
                    {
                        payload.Development = File.ReadAllText("/kvmnt/database-dev");
                        payload.Staging = File.ReadAllText("/kvmnt/database-staging");
                        payload.Production = File.ReadAllText("/kvmnt/database-prod");
                        break;
                    }
                case EnvironmentVariableOptions.BlobStorage:
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
