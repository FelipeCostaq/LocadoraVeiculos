using Azure.Storage.Blobs;
using LocadoraVeiculos.Domain.Interfaces.InterfaceServices;
using Microsoft.Extensions.Configuration;

namespace LocadoraVeiculos.Infrastructure.AzureBlobStorage;

public class AzureBlobStorageService  : IServiceStorage
{
    private readonly string _connectionString;
    private readonly string _containerName;
    
    public AzureBlobStorageService(IConfiguration configuration)
    {
        _connectionString = configuration.GetValue<string>("AzureStorage:ConnectionString");
        _containerName = configuration.GetValue<string>("AzureStorage:ContainerName");
    }

    public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
    {
        var blobServiceClient = new BlobServiceClient(_connectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(_containerName);
        
        await containerClient.CreateIfNotExistsAsync();
        
        var novoNome = $"{Guid.NewGuid()}-{fileName}";
        var blobClient = containerClient.GetBlobClient(novoNome);
        
        await blobClient.UploadAsync(fileStream);
        
        return blobClient.Uri.ToString();
    }
}