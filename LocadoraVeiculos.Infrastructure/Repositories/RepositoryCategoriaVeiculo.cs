using LocadoraVeiculos.Entities.Entities;
using LocadoraVeiculos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocadoraVeiculos.Infrastructure.Repositories
{
    public class RepositoryCategoriaVeiculo
    {
        private readonly DbContextOptions<LocadoraContext> _options;

        public RepositoryCategoriaVeiculo(DbContextOptions<LocadoraContext> options)
        {
            _options = options;
        }

        public async Task<List<CategoriaVeiculo>> ListarCategoriasVeiculos()
        {
            using (var data = new LocadoraContext(_options))
            {
                return await data.CategoriasVeiculo.AsNoTracking().ToListAsync();
            }
        }
    }
}
