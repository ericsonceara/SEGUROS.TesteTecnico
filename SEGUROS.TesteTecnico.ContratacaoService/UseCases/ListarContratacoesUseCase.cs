using SEGUROS.TesteTecnico.ContratacaoService.DTOs;
using SEGUROS.TesteTecnico.DOMAIN.Ports;

namespace SEGUROS.TesteTecnico.ContratacaoService.UseCases
{
    public class ListarContratacoesUseCase
    {
        private readonly IContratacaoRepository _repository;

        public ListarContratacoesUseCase(IContratacaoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ContratacaoResponse>> ExecutarAsync()
        {
            var contratacoes = await _repository.ListarTodasAsync();

            return contratacoes.Select(c => new ContratacaoResponse(
                c.Id,
                c.PropostaId,
                c.NomeCliente,
                c.ValorPremio,
                c.DataContratacao
            ));
        }
    }
}
