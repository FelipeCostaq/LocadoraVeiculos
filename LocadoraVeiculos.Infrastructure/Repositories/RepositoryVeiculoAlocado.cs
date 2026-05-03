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
        private readonly LocadoraContext _context;

        public RepositoryVeiculoAlocado(LocadoraContext context)
        {
            _context = context;
        }

        public async Task AdicionarVeiculoAlocado(RequestAdicionarVeiculoAlocadoDTO veiculoDto)
        {
            VeiculoAlocado veiculoAlocado = new VeiculoAlocado
            {
                ClienteId = veiculoDto.ClienteId,
                PlacaVeiculo = veiculoDto.PlacaVeiculo,
                DataRetirada = veiculoDto.DataRetirada,
                DataPrevDevol = veiculoDto.DataPrevDevol,
            };

            _context.VeiculosAlocados.Add(veiculoAlocado);

            Veiculo veiculo = await _context.Veiculos.FindAsync(veiculoDto.PlacaVeiculo);

            veiculo.Disponivel = false;

            await _context.SaveChangesAsync();
        }

        public async Task DarBaixaVeiculoAlocado(Guid id, decimal valorTotalCalculado, DateTime dataDevolucao)
        {
            VeiculoAlocado veiculoAlocado = await _context.VeiculosAlocados.FindAsync(id);

            if (veiculoAlocado == null)
                return;

            veiculoAlocado.DataDevolução = dataDevolucao;
            veiculoAlocado.Status = Status.Concluída;

            veiculoAlocado.ValorTotal = valorTotalCalculado;

            Veiculo veiculo = await _context.Veiculos.FindAsync(veiculoAlocado.PlacaVeiculo);

            if (veiculo is not null)
                veiculo.Disponivel = true;

            await _context.SaveChangesAsync();
        }

        public async Task<VeiculoAlocado> ListarVeiculoAlocadoPorId(Guid id)
        {
            return await _context.VeiculosAlocados.FindAsync(id);
        }

        public async Task<List<VeiculoAlocado>> ListarVeiculosAlocados()
        {
            return await _context.VeiculosAlocados.AsNoTracking().ToListAsync();
        }

        public async Task<List<VeiculoAlocado>> ListarVeiculosAlocadosDisponibilidade()
        {
                return await _context.VeiculosAlocados.Select(v => new VeiculoAlocado
                {
                    PlacaVeiculo = v.PlacaVeiculo,
                    Status = v.Status
                }).AsNoTracking().ToListAsync();
        }

        public async Task<bool> VerificarVeiculoLocacaoAtiva(string placa)
        {
            return await _context.Veiculos.AnyAsync(v => v.Placa == placa && v.Disponivel == true && v.Ativo == true);
        }

        public async Task<decimal> ListarPrecoCategoriaVeiculo(string placa)
        {
            Guid categoriaId = await _context.Veiculos
                       .AsNoTracking()
                       .Where(v => v.Placa == placa)
                       .Select(v => v.CategoriaId)
                       .FirstOrDefaultAsync();

            CategoriaVeiculo categoria = await _context.CategoriasVeiculo.FindAsync(categoriaId);

            decimal preco = categoria.ValorDiaria;

            return preco;
        }

        public async Task<bool> VerificarClienteAtivo(Guid id)
        {
            return await _context.Clientes.AnyAsync(v => v.Id == id && v.Ativo == true);
        }

        public async Task CancelarVeiculoAlocado(Guid id)
        {
            VeiculoAlocado veiculoAlocado = await _context.VeiculosAlocados.FindAsync(id);

            if (veiculoAlocado == null)
                return;

            veiculoAlocado.Status = Status.Cancelada;

            Veiculo veiculo = await _context.Veiculos.FindAsync(veiculoAlocado.PlacaVeiculo);

            if (veiculo != null)
                veiculo.Disponivel = true;

            await _context.SaveChangesAsync();
        }
    }
}
