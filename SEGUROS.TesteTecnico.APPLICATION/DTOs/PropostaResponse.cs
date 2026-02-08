namespace SEGUROS.TesteTecnico.APPLICATION.DTOs
{
    public record PropostaResponse
    (
        Guid Id,
        string NomeCliente,
        string CpfCliente,
        string TipoSeguro,
        decimal ValorPremio,
        string Status,
        DateTime DataCriacao,
        DateTime? DataAtualizacao
    );
}
