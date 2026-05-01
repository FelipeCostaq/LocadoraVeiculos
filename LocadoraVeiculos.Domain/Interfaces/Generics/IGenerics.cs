namespace LocadoraVeiculos.Domain.Interfaces.Generics;

public interface IGenerics<T> where T: class
{
    Task<T> GetEntityById(Guid Id);
    Task<List<T>> List();
}