using SEGUROS.TesteTecnico.APPLICATION.DTOs;
using SEGUROS.TesteTecnico.DOMAIN.Ports;

namespace SEGUROS.TesteTecnico.APPLICATION.UseCases
{
    public class ListarPropostasUseCase
    {
        private readonly IPropostaRepository _repository;

        public ListarPropostasUseCase(IPropostaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PropostaResponse>> ExecutarAsync()
        {
            var propostas = await _repository.ListarTodasAsync();

            return propostas.Select(p => new PropostaResponse(
                p.Id,
                p.NomeCliente,
                p.CpfCliente,
                p.TipoSeguro,
                p.ValorPremio,
                p.Status.ToString(),
                p.DataCriacao,
                p.DataAtualizacao
            ));
        }
    }
}
