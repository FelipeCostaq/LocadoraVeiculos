using LocadoraVeiculos.Entities.Entities;
using LocadoraVeiculos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using LocadoraVeiculos.Application.DTOs;
using LocadoraVeiculos.Domain.Interfaces.InterfaceCliente;
using LocadoraVeiculos.Infrastructure.Repositories.Generics;
using LocadoraVeiculos.Entities.Enums;

namespace LocadoraVeiculos.Infrastructure.Repositories
{
    public class RepositoryCliente : RepositoryGenerics<Cliente>, ICliente
    {
        private readonly LocadoraContext _context;

        public RepositoryCliente(LocadoraContext context)
        {
            _context = context;
        }

        public async Task AdicionarCliente(RequestAdicionarClienteDTO clienteDto)
        {
            Cliente cliente = new Cliente
            {
               Nome = clienteDto.Nome,
               CPF = clienteDto.CPF.Replace(".", "").Replace("-", ""),
               Email = clienteDto.Email.ToLower(),
               Telefone = clienteDto.Telefone,
               DataNasc = clienteDto.DataNasc,
               Endereco = clienteDto.Endereco,
            };
            
            _context.Clientes.Add(cliente);
            
            await _context.SaveChangesAsync();
        }

        public async Task EditarCliente(Guid id, RequestEditarClienteDTO clienteDto)
        {
            var clienteAntigo = await _context.Clientes.FindAsync(id);
            
            clienteAntigo.Nome = clienteDto.Nome;
            clienteAntigo.CPF = clienteDto.CPF.Replace(".", "").Replace("-", "");
            clienteAntigo.Email = clienteDto.Email.ToLower();
            clienteAntigo.DataNasc = clienteDto.DataNasc;
            clienteAntigo.Endereco = clienteDto.Endereco;
            clienteAntigo.Telefone = clienteDto.Telefone;
            clienteAntigo.Ativo = clienteDto.Ativo;
            
            await _context.SaveChangesAsync();
        }

        public async Task ExcluirCliente(Guid id)
        {
            Cliente cliente = await _context.Clientes.FindAsync(id);
            
            if (cliente is null)
                throw new NullReferenceException();

            _context.Clientes.Remove(cliente);

            await _context.SaveChangesAsync();
        }

        public async Task<Cliente> ListarClientePorId(Guid id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            
            if (cliente is null)
                throw new NullReferenceException();

            return cliente;
        }

        public async Task<List<Cliente>> ListarClientes()
        {
                return await _context.Clientes.AsNoTracking().ToListAsync();
        }

        public async Task<bool> ClienteAlocacaoAtiva(Guid id)
        {
            return await _context.VeiculosAlocados.AnyAsync(v => v.ClienteId == id && v.Status == Status.Ativa);
        }
    }
}
