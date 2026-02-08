namespace SEGUROS.TesteTecnico.APPLICATION.DTOs
{
    public record CriarPropostaRequest(
        string NomeCliente,
        string CpfCliente,
        string TipoSeguro,
        decimal ValorPremio
    );
}
