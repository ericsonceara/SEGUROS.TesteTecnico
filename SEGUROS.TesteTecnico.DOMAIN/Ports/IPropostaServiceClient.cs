namespace SEGUROS.TesteTecnico.DOMAIN.Ports
{
    public interface IPropostaServiceClient
    {
        Task<PropostaDto?> ObterPropostaPorIdAsync(Guid propostaId);

        public record PropostaDto(
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
}
