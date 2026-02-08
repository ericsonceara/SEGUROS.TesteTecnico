using SEGUROS.TesteTecnico.DOMAIN.Entities;

namespace SEGUROS.TesteTecnico.DOMAIN.Ports
{
    public interface IContratacaoRepository
    {
        Task<Contratacao> CriarAsync(Contratacao contratacao);
        Task<Contratacao?> ObterPorPropostaIdAsync(Guid propostaId);
        Task<IEnumerable<Contratacao>> ListarTodasAsync();
    }
}
