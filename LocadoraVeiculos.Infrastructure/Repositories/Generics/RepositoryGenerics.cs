using System.Runtime.InteropServices;
using LocadoraVeiculos.Domain.Interfaces.Generics;
using LocadoraVeiculos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;

namespace LocadoraVeiculos.Infrastructure.Repositories.Generics;


public class RepositoryGenerics<T> : IGenerics<T>, IDisposable where T : class
{
    private readonly DbContextOptions<LocadoraContext> _OptionsBuilder;



    public RepositoryGenerics()
    {
        _OptionsBuilder = new DbContextOptions<LocadoraContext>();
    }


    public async Task Add(T Objeto)
    {
        using (var data = new LocadoraContext(_OptionsBuilder))
        {
            await data.Set<T>().AddAsync(Objeto);
            await data.SaveChangesAsync();
        }  
    }

    public async Task Delete(T Objeto)
    {
        using (var data = new LocadoraContext(_OptionsBuilder))
        {
            data.Set<T>().Remove(Objeto);
            await data.SaveChangesAsync();
        }
    }
    
    public async Task<T> GetEntityById(Guid Id)
    {
        using (var data = new LocadoraContext(_OptionsBuilder))
        {
            var cliente = await data.Set<T>().FindAsync(Id);

            return cliente;
        }
    }

    public async Task<List<T>> List()
    {
        using (var data = new LocadoraContext(_OptionsBuilder))
        {
            return await data.Set<T>().AsNoTracking().ToListAsync();
        }
    }

    public async Task Update(T Objeto)
    {
        using (var data = new LocadoraContext(_OptionsBuilder))
        {
            data.Set<T>().Update(Objeto);
            await data.SaveChangesAsync();
        }
    }

    #region Disposed

    bool disposed = false;

    SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposed)
            return;

        if (disposing)
        {
            handle.Dispose();
        }

        disposed = false;
    }

    #endregion
}
