using System;
using System.Threading.Tasks;
using ACMTTU.NoteSharing.Shared.DataContracts;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Storage.Blob;

namespace ACMTTU.NoteSharing.Shared.SDK.Clients
{
    public interface IClientFactory
    {
        CloudBlobClient GetStorageClient(string connectionString);
        CosmosClient GetDatabaseClient(string connectionString);
    }
}