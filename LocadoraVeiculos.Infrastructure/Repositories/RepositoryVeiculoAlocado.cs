using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using LocadoraVeiculos.Domain.Interfaces.InterfaceVeiculoAlocado;
using LocadoraVeiculos.Entities.DTOs;
using LocadoraVeiculos.Entities.Entities;
using LocadoraVeiculos.Entities.Enums;
using LocadoraVeiculos.Infrastructure.Data;
using LocadoraVeiculos.Infrastructure.Repositories.Generics;
using Microsoft.EntityFrameworkCore;

namespace LocadoraVeiculos.Infrastructure.Repositories
{
    public class RepositoryVeiculoAlocado : RepositoryGenerics<VeiculoAlocado>, IVeiculoAlocado
    {
        private readonly DbContextOptions<LocadoraContext> _options;

        public RepositoryVeiculoAlocado(DbContextOptions<LocadoraContext> options)
        {
            _options = options;
        }

        public async Task AdicionarVeiculoAlocado(RequestAdicionarVeiculoAlocadoDTO veiculoDto)
        {
            using (var data = new LocadoraContext(_options))
            {
                VeiculoAlocado veiculoAlocado = new VeiculoAlocado
                {
                    ClienteId = veiculoDto.ClienteId,
                    PlacaVeiculo = veiculoDto.PlacaVeiculo,
                    DataRetirada = veiculoDto.DataRetirada,
                    DataPrevDevol = veiculoDto.DataPrevDevol,
                };

                await data.VeiculosAlocados.AddAsync(veiculoAlocado);

                Veiculo veiculo = await data.Veiculos.FindAsync(veiculoDto.PlacaVeiculo);

                veiculo.Disponivel = false;

                await data.SaveChangesAsync();
            }
        }

        public async Task DarBaixaVeiculoAlocado(Guid id, decimal valorTotalCalculado)
        {
            using (var data = new LocadoraContext(_options))
            {
                VeiculoAlocado veiculoAlocado = await data.VeiculosAlocados.FindAsync(id);

                if (veiculoAlocado == null)
                    return;

                veiculoAlocado.DataDevolução = DateTime.Now;
                veiculoAlocado.Status = Status.Concluída;

                veiculoAlocado.ValorTotal = valorTotalCalculado;

                Veiculo veiculo = await data.Veiculos.FindAsync(veiculoAlocado.PlacaVeiculo);

                if (veiculo != null)
                    veiculo.Disponivel = true;

                await data.SaveChangesAsync();
            }
        }

        public async Task<VeiculoAlocado> ListarVeiculoAlocadoPorId(Guid id)
        {
            using (var data = new LocadoraContext(_options))
            {
                VeiculoAlocado veiculoAlocado = await data.VeiculosAlocados.FindAsync(id);

                return veiculoAlocado;
            }
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

        public async Task<bool> VerificarVeiculoLocacaoAtiva(string placa)
        {
            using (var data = new LocadoraContext(_options))
            {
                return await data.Veiculos.AnyAsync(v => v.Placa == placa && v.Disponivel == true);
            }
        }

        public async Task<decimal> ListarPrecoCategoriaVeiculo(string placa)
        {
            using (var data = new LocadoraContext(_options))
            {
                Guid categoriaId = await data.Veiculos
                           .AsNoTracking()
                           .Where(v => v.Placa == placa)
                           .Select(v => v.CategoriaId)
                           .FirstOrDefaultAsync();

                CategoriaVeiculo categoria = await data.CategoriasVeiculo.FindAsync(categoriaId);

                decimal preco = categoria.ValorDiaria;

                return preco;
            }
        }

        public async Task<bool> VerificarClienteAtivo(Guid id)
        {
            using (var data = new LocadoraContext(_options))
            {
                return await data.Clientes.AnyAsync(v => v.Id == id && v.Ativo == true);
            }
        }
    }
}
