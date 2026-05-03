using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using LocadoraVeiculos.Application.DTOs;
using LocadoraVeiculos.Domain.Interfaces.InterfaceCategoriaVeiculo;
using LocadoraVeiculos.Domain.Interfaces.InterfaceCliente;
using LocadoraVeiculos.Entities.Entities;
using LocadoraVeiculos.Infrastructure.Data;
using LocadoraVeiculos.Infrastructure.Repositories.Generics;
using Microsoft.EntityFrameworkCore;

namespace LocadoraVeiculos.Infrastructure.Repositories
{
    public class RepositoryCategoriaVeiculo : RepositoryGenerics<CategoriaVeiculo>, ICategoriaVeiculo
    {
        private readonly LocadoraContext _context;

        public RepositoryCategoriaVeiculo(LocadoraContext context)
        {
            _context = context;
        }

        public async Task AdicionarCategoriaVeiculo(RequestAdicionarCategoriaVeiculoDTO categoriaVeiculoDto)
        {

            CategoriaVeiculo categoriaVeiculo = new CategoriaVeiculo
            {
                Nome = categoriaVeiculoDto.Nome.ToUpper(),
                Descricao = categoriaVeiculoDto.Descricao,
                ValorDiaria = categoriaVeiculoDto.ValorDiaria
            };
            
            _context.CategoriasVeiculo.Add(categoriaVeiculo);

            await _context.SaveChangesAsync();

        }

        public async Task<bool> CategoriaEmUso(Guid id)
        {
            return await _context.Veiculos.AnyAsync(v => v.CategoriaId == id);
        }

        public async Task EditarCategoriaVeiculo(Guid id, RequestEditarCategoriaVeiculoDTO categoriaVeiculoDto)
        {

            var categoriaVeiculoAntigo = await _context.CategoriasVeiculo.FindAsync(id);
            
            categoriaVeiculoAntigo.Nome = categoriaVeiculoDto.Nome.ToUpper();
            categoriaVeiculoAntigo.Descricao = categoriaVeiculoDto.Descricao;
            categoriaVeiculoAntigo.ValorDiaria = categoriaVeiculoDto.ValorDiaria;
            categoriaVeiculoAntigo.Ativo = categoriaVeiculoDto.Ativo;

            await _context.SaveChangesAsync();
        }

        public async Task<List<CategoriaVeiculo>> ListarCategoriasVeiculo()
        {
            return await _context.CategoriasVeiculo.AsNoTracking().ToListAsync();
        }

        public async Task<CategoriaVeiculo> ListarCategoriasVeiculoPorId(Guid id)
        {
            return await _context.CategoriasVeiculo.FindAsync(id);
        }
    }
}
