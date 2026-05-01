using LocadoraVeiculos.Application.DTOs;
using LocadoraVeiculos.Domain.Interfaces.InterfaceCategoriaVeiculo;
using LocadoraVeiculos.Domain.Interfaces.InterfaceServices;
using LocadoraVeiculos.Entities.Entities;

namespace LocadoraVeiculos.Domain.Services;

public class ServiceCategoriaVeiculo : IServiceCategoriaVeiculo
{
    private readonly ICategoriaVeiculo _icategoriaVeiculo;

    public ServiceCategoriaVeiculo(ICategoriaVeiculo icategoriaVeiculo)
    {
        _icategoriaVeiculo = icategoriaVeiculo;
    }
    
    public async Task AdicionarCategoriaVeiculo(RequestAdicionarCategoriaVeiculoDTO categoriaVeiculoDto)
    {
        if (categoriaVeiculoDto.ValorDiaria < 1)
            throw new Exception("O valor da diária tem que ser igual ou superior a 1");

        await _icategoriaVeiculo.AdicionarCategoriaVeiculo(categoriaVeiculoDto);
    }

    public async Task EditarCategoriaVeiculo(Guid id, RequestEditarCategoriaVeiculoDTO categoriaVeiculoDto)
    {
        CategoriaVeiculo categoriaVeiculo = await _icategoriaVeiculo.ListarCategoriasVeiculoPorId(id);

        if (categoriaVeiculo.Ativo != categoriaVeiculoDto.Ativo && await _icategoriaVeiculo.CategoriaEmUso(id))
            throw new Exception("Não é possível mudar o status de uma categoria enquanto ela está vinculada com um veículo.");

        if (categoriaVeiculoDto.ValorDiaria < 1)
            throw new Exception("O valor da diária tem que ser igual ou superior a 1");

        await _icategoriaVeiculo.EditarCategoriaVeiculo(id, categoriaVeiculoDto);   
    }
}