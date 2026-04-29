namespace LocadoraVeiculos.Domain.Interfaces.Generics;

public interface IGenerics<T> where T: class
{
    Task Add(T Objeto);
    Task Update(T Objeto);
    Task<T> GetEntityById(Guid Id);
    Task<List<T>> List();
}