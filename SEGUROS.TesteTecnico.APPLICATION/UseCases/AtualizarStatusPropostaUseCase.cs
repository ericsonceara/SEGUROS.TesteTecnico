using SEGUROS.TesteTecnico.APPLICATION.DTOs;
using SEGUROS.TesteTecnico.DOMAIN.Entities;
using SEGUROS.TesteTecnico.DOMAIN.Ports;

namespace SEGUROS.TesteTecnico.APPLICATION.UseCases
{
    public class AtualizarStatusPropostaUseCase
    {
        private readonly IPropostaRepository _repository;

        public AtualizarStatusPropostaUseCase(IPropostaRepository repository)
        {
            _repository = repository;
        }

        public async Task<PropostaResponse> ExecutarAsync(Guid id, AtualizarStatusRequest request)
        {
            var proposta = await _repository.ObterPorIdAsync(id);

            if (proposta is null)
                throw new InvalidOperationException("Proposta não encontrada");

            if (!Enum.TryParse<StatusProposta>(request.NovoStatus, out var novoStatus))
                throw new ArgumentException("Status inválido");

            proposta.AtualizarStatus(novoStatus);

            var propostaAtualizada = await _repository.AtualizarAsync(proposta);

            return new PropostaResponse(
                propostaAtualizada.Id,
                propostaAtualizada.NomeCliente,
                propostaAtualizada.CpfCliente,
                propostaAtualizada.TipoSeguro,
                propostaAtualizada.ValorPremio,
                propostaAtualizada.Status.ToString(),
                propostaAtualizada.DataCriacao,
                propostaAtualizada.DataAtualizacao
            );
        }
    }
}
