using DocumentService.Core.Interfaces;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Threading.Tasks;

namespace DocumentService.Infrastructure.Storage
{
    public class AzureStorage : IStorage
    {
        public Task DeleteFile(string filename)
        {
            throw new NotImplementedException();
        }

        public string GetFileUrl(string filename)
        {
            throw new NotImplementedException();
        }

        public Task<CloudBlockBlob> SaveFileAsync(string filename, string contentType, byte[] file)
        {
            throw new NotImplementedException();
        }
    }
}
