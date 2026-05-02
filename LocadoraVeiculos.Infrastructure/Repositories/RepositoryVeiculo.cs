using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using LocadoraVeiculos.Application.DTOs;
using LocadoraVeiculos.Domain.Interfaces.Generics;
using LocadoraVeiculos.Domain.Interfaces.InterfaceVeiculo;
using LocadoraVeiculos.Entities.Entities;
using LocadoraVeiculos.Infrastructure.Data;
using LocadoraVeiculos.Infrastructure.Repositories.Generics;
using Microsoft.EntityFrameworkCore;

namespace LocadoraVeiculos.Infrastructure.Repositories
{
    public class RepositoryVeiculo : RepositoryGenerics<Veiculo>, IVeiculo
    {
        private readonly DbContextOptions<LocadoraContext> _options;

        public RepositoryVeiculo(DbContextOptions<LocadoraContext> options)
        {
            _options = options;
        }

        public async Task AdicionarVeiculo(RequestAdicionarVeiculoDTO veiculoDto)
        {
            using (var data = new LocadoraContext(_options))
            {
                Veiculo veiculo = new Veiculo
                {
                    Placa = veiculoDto.Placa.Replace("-", "").Replace(" ", "").ToUpper(),
                    Marca = veiculoDto.Marca.ToUpper(),
                    Modelo = veiculoDto.Modelo.ToUpper(),
                    Ano = veiculoDto.Ano,
                    Cor = veiculoDto.Cor.ToUpper(),
                    CategoriaId =  veiculoDto.CategoriaId,
                    ImagemUrl =  veiculoDto.ImagemUrl,
                    Disponivel =  veiculoDto.Disponivel,
                };
                
                await data.Veiculos.AddAsync(veiculo);

                await data.SaveChangesAsync();
            }
        }

        public async Task EditarVeiculo(string placa, RequestEditarVeiculoDTO veiculoDto)
        {
            using (var data = new LocadoraContext(_options))
            {
                var veiculoAntigo = await data.Veiculos.FindAsync(placa);

                veiculoAntigo.Marca = veiculoDto.Marca.ToUpper();
                veiculoAntigo.Modelo = veiculoDto.Modelo.ToUpper();
                veiculoAntigo.Ano = veiculoDto.Ano;
                veiculoAntigo.Cor = veiculoDto.Cor.ToUpper();
                veiculoAntigo.CategoriaId = veiculoDto.CategoriaId;
                veiculoAntigo.Ativo = veiculoDto.Ativo;
                veiculoAntigo.Disponivel = veiculoDto.Disponivel;
                
                if (!string.IsNullOrEmpty(veiculoDto.ImagemUrl))
                    veiculoAntigo.ImagemUrl = veiculoDto.ImagemUrl;
                

                await data.SaveChangesAsync();
            }
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
                return await data.Veiculos.AsNoTracking().Where(v => v.Disponivel == true).ToListAsync();
            }
        }

        public async Task<Veiculo> ListarVeiculoPorId(string placa)
        {
            using (var data = new LocadoraContext(_options))
            {
                return await data.Veiculos.FindAsync(placa);
            }
        }

        public async Task<bool> VeiculoLocacaoAtivo(string placa)
        {
            using (var data = new LocadoraContext(_options))
            {
                return await data.VeiculosAlocados.AnyAsync(v => v.PlacaVeiculo == placa && v.Status == Entities.Enums.Status.Ativa);
            }
        }

        public async Task<bool> VeiculoCategoriaAtivo(Guid id)
        {
            using (var data = new LocadoraContext(_options))
            {
                return await data.CategoriasVeiculo.AnyAsync(c => c.Id == id && c.Ativo == true);
            }
        }
    }
}
