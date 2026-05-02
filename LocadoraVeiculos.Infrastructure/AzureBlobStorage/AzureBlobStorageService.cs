using Azure.Storage.Blobs;
using DotNetEnv;
using LocadoraVeiculos.Domain.Interfaces.InterfaceServices;
using Microsoft.Extensions.Configuration;

namespace LocadoraVeiculos.Infrastructure.AzureBlobStorage;

public class AzureBlobStorageService  : IServiceStorage
{
    private readonly string _connectionString;
    private readonly string _containerName;
    
    public AzureBlobStorageService(IConfiguration configuration)
    {
        Env.Load();

        _connectionString = Env.GetString("AZURE_STORAGE_CONNECTION_STRING");
        _containerName =  Env.GetString("AZURE_STORAGE_CONTAINER_NAME");
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