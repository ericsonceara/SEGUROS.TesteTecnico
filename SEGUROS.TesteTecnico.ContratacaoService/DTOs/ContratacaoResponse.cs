namespace SEGUROS.TesteTecnico.ContratacaoService.DTOs
{
    public record ContratacaoResponse(
        Guid Id,
        Guid PropostaId,
        string NomeCliente,
        decimal ValorPremio,
        DateTime DataContratacao
    );
}
