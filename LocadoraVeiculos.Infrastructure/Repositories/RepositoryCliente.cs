using LocadoraVeiculos.Entities.Entities;
using LocadoraVeiculos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using LocadoraVeiculos.Application.DTOs;
using LocadoraVeiculos.Domain.Interfaces.InterfaceCliente;
using LocadoraVeiculos.Infrastructure.Repositories.Generics;

namespace LocadoraVeiculos.Infrastructure.Repositories
{
    public class RepositoryCliente : RepositoryGenerics<Cliente>, ICliente
    {
        private readonly DbContextOptions<LocadoraContext> _options;

        public RepositoryCliente(DbContextOptions<LocadoraContext> options)
        {
            _options = options;
        }
        

        public async Task AdicionarCliente(RequestAdicionarClienteDTO clienteDto)
        {
            using (var data = new LocadoraContext(_options))
            {
                Cliente cliente = new Cliente
                {
                   Nome = clienteDto.Nome,
                   CPF = clienteDto.CPF,
                   Email = clienteDto.Email,
                   Telefone = clienteDto.Telefone,
                   DataNasc = clienteDto.DataNasc,
                   Endereco = clienteDto.Endereco,
                };
                
                await data.Clientes.AddAsync(cliente);
                
                await data.SaveChangesAsync();
            }
        }

        public async Task EditarCliente(Guid id, RequestEditarClienteDTO clienteDto)
        {
            using (var data = new LocadoraContext(_options))
            {
                var clienteAntigo = await data.Clientes.FindAsync(id);
                
                clienteAntigo.Nome = clienteDto.Nome;
                clienteAntigo.CPF = clienteDto.CPF;
                clienteAntigo.Email = clienteDto.Email;
                clienteAntigo.DataNasc = clienteDto.DataNasc;
                clienteAntigo.Endereco = clienteDto.Endereco;
                clienteAntigo.Telefone = clienteDto.Telefone;
                clienteAntigo.Ativo = clienteDto.Ativo;
                
                await data.SaveChangesAsync();
            }
        }

        public async Task<Cliente> ListarClientePorId(Guid id)
        {
            using (var data = new LocadoraContext(_options))
            {
                var cliente = await data.Clientes.FindAsync(id);

                return cliente;
            }
        }

        public async Task<List<Cliente>> ListarClientes()
        {
            using (var data = new LocadoraContext(_options))
            {
                return await data.Clientes.AsNoTracking().ToListAsync();
            }
        }
    }
}
