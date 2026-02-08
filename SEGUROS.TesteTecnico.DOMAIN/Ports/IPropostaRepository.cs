using SEGUROS.TesteTecnico.DOMAIN.Entities;

namespace SEGUROS.TesteTecnico.DOMAIN.Ports
{
    public interface IPropostaRepository
    {
        Task<Proposta> CriarAsync(Proposta proposta);
        Task<Proposta?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Proposta>> ListarTodasAsync();
        Task<Proposta> AtualizarAsync(Proposta proposta);
    }
}
