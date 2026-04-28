using LocadoraVeiculos.Entities.Entities;
using LocadoraVeiculos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocadoraVeiculos.Infrastructure.Repositories
{
    public class RepositoryCliente
    {
        private readonly DbContextOptions<LocadoraContext> _options;

        public RepositoryCliente(DbContextOptions<LocadoraContext> options)
        {
            _options = options;
        }

        public async Task<List<Cliente>> ListarClientes(Cliente cliente)
        {
            using (var data = new LocadoraContext(_options))
            {
                return await data.Clientes.AsNoTracking().ToListAsync();
            }
        }

        public async Task<Cliente?> ListarClientePorId(Guid id)
        {
            using (var data = new LocadoraContext(_options))
            {
                return await data.Clientes.FindAsync(id);
            }
        }
    }
}
