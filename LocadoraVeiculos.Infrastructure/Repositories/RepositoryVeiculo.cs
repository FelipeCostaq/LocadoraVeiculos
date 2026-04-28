using LocadoraVeiculos.Entities.Entities;
using LocadoraVeiculos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocadoraVeiculos.Infrastructure.Repositories
{
    public class RepositoryVeiculo
    {
        private readonly DbContextOptions<LocadoraContext> _options;

        public RepositoryVeiculo(DbContextOptions<LocadoraContext> options)
        {
            _options = options;
        }

        public async Task<List<Veiculo>> ListarVeiculos()
        {
            using (var data = new LocadoraContext(_options))
            {
                return await data.Veiculos.AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<Veiculo>> ListarVeiculosDisponiveis()
        {
            using (var data = new LocadoraContext(_options))
            {
                return await data.Veiculos.Where(v => v.Disponivel == true).ToListAsync();
            }
        }
    }
}
