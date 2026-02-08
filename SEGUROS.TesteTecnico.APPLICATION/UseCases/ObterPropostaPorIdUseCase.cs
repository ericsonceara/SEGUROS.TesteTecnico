using SEGUROS.TesteTecnico.APPLICATION.DTOs;
using SEGUROS.TesteTecnico.DOMAIN.Ports;

namespace SEGUROS.TesteTecnico.APPLICATION.UseCases
{
    public class ObterPropostaPorIdUseCase
    {
        private readonly IPropostaRepository _repository;

        public ObterPropostaPorIdUseCase(IPropostaRepository repository)
        {
            _repository = repository;
        }

        public async Task<PropostaResponse?> ExecutarAsync(Guid id)
        {
            var proposta = await _repository.ObterPorIdAsync(id);

            if (proposta is null)
                return null;

            return new PropostaResponse(
                proposta.Id,
                proposta.NomeCliente,
                proposta.CpfCliente,
                proposta.TipoSeguro,
                proposta.ValorPremio,
                proposta.Status.ToString(),
                proposta.DataCriacao,
                proposta.DataAtualizacao
            );
        }
    }
}
