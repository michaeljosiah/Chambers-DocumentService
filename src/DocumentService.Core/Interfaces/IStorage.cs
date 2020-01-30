using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace DocumentService.Core.Interfaces
{
    public interface IStorage
    {

        Task<CloudBlockBlob> SaveFileAsync(string filename, string contentType, byte[] file);

        string GetFileUrl(string filename);
        
        Task DeleteFile(string filename);
    }
}
