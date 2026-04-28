using LocadoraVeiculos.Entities.Entities;
using LocadoraVeiculos.Entities.Enums;
using LocadoraVeiculos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocadoraVeiculos.Infrastructure.Repositories
{
    public class RepositoryVeiculoAlocado
    {
        private readonly DbContextOptions<LocadoraContext> _options;

        public RepositoryVeiculoAlocado(DbContextOptions<LocadoraContext> options)
        {
            _options = options;
        }

        public async Task<List<VeiculoAlocado>> ListarVeiculosAlocados()
        {
            using (var data = new LocadoraContext(_options))
            {
                return await data.VeiculosAlocados.AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<VeiculoAlocado>> ListarVeiculosAlocadosDisponibilidade()
        {
            using (var data = new LocadoraContext(_options))
            {
                return await data.VeiculosAlocados.Select(v => new VeiculoAlocado()
                {
                    PlacaVeiculo = v.PlacaVeiculo,
                    Status = v.Status
                }).AsNoTracking().ToListAsync();
            }
        }   
    }
}
