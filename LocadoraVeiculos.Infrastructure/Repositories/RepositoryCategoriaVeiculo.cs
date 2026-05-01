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
        private readonly DbContextOptions<LocadoraContext> _options;

        public RepositoryCategoriaVeiculo(DbContextOptions<LocadoraContext> options)
        {
            _options = options;
        }

        public async Task AdicionarCategoriaVeiculo(RequestAdicionarCategoriaVeiculoDTO categoriaVeiculoDto)
        {
            using (var data = new LocadoraContext(_options))
            {
                CategoriaVeiculo categoriaVeiculo = new CategoriaVeiculo
                {
                    Nome = categoriaVeiculoDto.Nome.ToUpper(),
                    Descricao = categoriaVeiculoDto.Descricao,
                    ValorDiaria = categoriaVeiculoDto.ValorDiaria
                };
                
                await data.AddAsync(categoriaVeiculo);

                await data.SaveChangesAsync();
            }
        }

        public async Task<bool> CategoriaEmUso(Guid id)
        {
            using (var data = new LocadoraContext(_options))
            {
                return await data.Veiculos.AnyAsync(v => v.CategoriaId == id);
            }
        }

        public async Task EditarCategoriaVeiculo(Guid id, RequestEditarCategoriaVeiculoDTO categoriaVeiculoDto)
        {
            using (var data = new LocadoraContext(_options))
            {
                var categoriaVeiculoAntigo = await data.CategoriasVeiculo.FindAsync(id);


                categoriaVeiculoAntigo.Nome = categoriaVeiculoDto.Nome.ToUpper();
                categoriaVeiculoAntigo.Descricao = categoriaVeiculoDto.Descricao;
                categoriaVeiculoAntigo.ValorDiaria = categoriaVeiculoDto.ValorDiaria;
                categoriaVeiculoAntigo.Ativo = categoriaVeiculoDto.Ativo;

                await data.SaveChangesAsync();
            }
        }

        public async Task<List<CategoriaVeiculo>> ListarCategoriasVeiculo()
        {
            using (var data = new LocadoraContext(_options))
            {
                return await data.CategoriasVeiculo.AsNoTracking().ToListAsync();
            }
        }

        public async Task<CategoriaVeiculo> ListarCategoriasVeiculoPorId(Guid id)
        {
            using (var data = new LocadoraContext(_options))
            {
                return await data.CategoriasVeiculo.FindAsync(id);
            }
        }
    }
}
