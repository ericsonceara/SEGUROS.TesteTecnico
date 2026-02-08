using SEGUROS.TesteTecnico.DOMAIN.Entities;
using SEGUROS.TesteTecnico.DOMAIN.Ports;

namespace SEGUROS.TesteTecnico.ContratacaoService.Infrastructure.Repositories
{
    public class ContratacaoRepository : IContratacaoRepository
    {
        private readonly List<Contratacao> _contratacoes = new();

        public Task<Contratacao> CriarAsync(Contratacao contratacao)
        {
            _contratacoes.Add(contratacao);
            return Task.FromResult(contratacao);
        }

        public Task<IEnumerable<Contratacao>> ListarTodasAsync()
        {
            return Task.FromResult<IEnumerable<Contratacao>>(_contratacoes);
        }

        public Task<Contratacao?> ObterPorPropostaIdAsync(Guid propostaId)
        {
            var contratacao = _contratacoes.FirstOrDefault(c => c.PropostaId == propostaId);
            return Task.FromResult(contratacao);
        }
    }
}
