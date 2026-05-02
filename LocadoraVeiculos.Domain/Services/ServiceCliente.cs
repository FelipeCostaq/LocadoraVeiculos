using LocadoraVeiculos.Application.DTOs;
using LocadoraVeiculos.Domain.Interfaces.InterfaceCliente;
using LocadoraVeiculos.Domain.Interfaces.InterfaceServices;
using LocadoraVeiculos.Entities.Entities;

namespace LocadoraVeiculos.Domain.Services;

public class ServiceCliente : IServiceCliente
{
    private readonly ICliente _cliente;

    public ServiceCliente(ICliente cliente)
    {
        _cliente = cliente;
    }
    
    public async Task AdicionarCliente(RequestAdicionarClienteDTO clienteDto)
    {
        if (!ValidarCpf(clienteDto.CPF))
            throw new Exception("CPF inválido.");
        
        int idadeCliente = DateTime.Today.Year - clienteDto.DataNasc.Year;
        
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        
        if (clienteDto.DataNasc > today.AddYears(-idadeCliente))
            idadeCliente--;
        
        if (idadeCliente < 18)
            throw new InvalidOperationException("O cliente deve ter pelo menos 18 anos para ser cadastrado.");
        
        string telefoneFormatado = SanitizarTelefone(clienteDto.Telefone);

        if (!ValidarTelefoneFormatado(telefoneFormatado))
            throw new InvalidOperationException("Telefone inválido");

        clienteDto.CPF = clienteDto.CPF.Replace(".", "").Replace("-", "");
        clienteDto.Telefone = telefoneFormatado;
        
        await _cliente.AdicionarCliente(clienteDto);
    }

    public async Task<bool> ClienteAlocacaoAtiva(Guid id)
    {
        return await _cliente.ClienteAlocacaoAtiva(id);
    }

    public async Task EditarCliente(Guid id, RequestEditarClienteDTO clienteDto)
    {
        if (!ValidarCpf(clienteDto.CPF))
            throw new Exception("CPF inválido.");
        
        int idadeCliente = DateTime.Today.Year - clienteDto.DataNasc.Year;
        
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        
        if (clienteDto.DataNasc > today.AddYears(-idadeCliente))
            idadeCliente--;
        
        if (idadeCliente < 18)
            throw new InvalidOperationException("O cliente deve ter pelo menos 18 anos para ser cadastrado.");
        
        string telefoneFormatado = SanitizarTelefone(clienteDto.Telefone);

        if (!ValidarTelefoneFormatado(telefoneFormatado))
            throw new InvalidOperationException("Telefone inválido");

        clienteDto.CPF = clienteDto.CPF.Replace(".", "").Replace("-", "");
        clienteDto.Telefone = telefoneFormatado;
        
        await _cliente.EditarCliente(id, clienteDto);
    }

    public async Task ExcluirCliente(Guid id)
    {
        if (await _cliente.ClienteAlocacaoAtiva(id))
            throw new InvalidOperationException("O Cliente está com uma alocação ativa.");

        await _cliente.ExcluirCliente(id);
    }

    public static bool ValidarCpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf)) return false;
        
        Span<int> numeros = stackalloc int[11];
        int count = 0;
        foreach (var c in cpf)
        {
            if (char.IsDigit(c))
            {
                if (count > 10) return false;
                numeros[count++] = c - '0';
            }
        }

        if (count != 11) return false;
        
        bool todosIguais = true;
        for (int i = 1; i < 11; i++)
        {
            if (numeros[i] != numeros[0])
            {
                todosIguais = false;
                break;
            }
        }
        
        if (todosIguais) return false;
        
        int soma1 = 0;
        for (int i = 0; i < 9; i++) soma1 += numeros[i] * (10 - i);
        int resto1 = soma1 % 11;
        int dv1 = resto1 < 2 ? 0 : 11 - resto1;
        if (numeros[9] != dv1) return false;
        
        int soma2 = 0;
        for (int i = 0; i < 10; i++) soma2 += numeros[i] * (11 - i);
        int resto2 = soma2 % 11;
        int dv2 = resto2 < 2 ? 0 : 11 - resto2;

        return numeros[10] == dv2;
    }
    
    public static string SanitizarTelefone(string telefone)
    {
        if (string.IsNullOrWhiteSpace(telefone))
            throw new InvalidOperationException("Telefone é obrigatório");
        
        string apenasNumeros = new string(telefone.Where(char.IsDigit).ToArray());
        
        if (apenasNumeros.Length != 11)
            throw new InvalidOperationException("Tamanho do telefone inválido.");

        return apenasNumeros;
    }
    
    public static bool ValidarTelefoneFormatado(string telefoneLimpo)
    {
        string[] dddsValidos = { 
            "11", "12", "13", "14", "15", "16", "17", "18", "19", 
            "21", "22", "24", "27", "28", "31", "32", "33", "34", 
            "35", "37", "38", "41", "42", "43", "44", "45", "46", 
            "47", "48", "49", "51", "53", "54", "55", "61", "62", 
            "63", "64", "65", "66", "67", "68", "69", "71", "73", 
            "74", "75", "77", "79", "81", "82", "83", "84", "85", 
            "86", "87", "88", "89", "91", "92", "93", "94", "95", 
            "96", "97", "98", "99" 
        };

        if (!dddsValidos.Contains(telefoneLimpo.Substring(0, 2))) return false;
        
        if (telefoneLimpo.Length == 11 && telefoneLimpo[2] != '9') return false;

        return true;
    }
}