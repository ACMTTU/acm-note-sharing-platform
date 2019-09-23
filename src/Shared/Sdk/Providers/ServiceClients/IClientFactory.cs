using System;
using System.Threading.Tasks;
using ACMTTU.NoteSharing.Shared.DataContracts;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Storage.Blob;

namespace ACMTTU.NoteSharing.Shared.SDK.Clients
{
    public interface IClientFactory
    {
        Task<CloudBlobClient> GetStorageClientAsync();
        Task<CosmosClient> GetDatabaseClientAsync();
    }
}