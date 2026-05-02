namespace LocadoraVeiculos.Domain.Interfaces.InterfaceServices;

public interface IServiceStorage
{
    Task<string> UploadFileAsync(Stream fileStream, string fileName);
}