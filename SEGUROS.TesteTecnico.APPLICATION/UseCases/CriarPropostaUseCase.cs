using SEGUROS.TesteTecnico.APPLICATION.DTOs;
using SEGUROS.TesteTecnico.APPLICATION.Mappers;
using SEGUROS.TesteTecnico.DOMAIN.Entities;
using SEGUROS.TesteTecnico.DOMAIN.Ports;

namespace SEGUROS.TesteTecnico.APPLICATION.UseCases
{
    public class CriarPropostaUseCase
    {
        private readonly IPropostaRepository _repository;

        public CriarPropostaUseCase(IPropostaRepository repository)
        {
            _repository = repository;
        }

        public async Task<PropostaResponse> ExecutarAsync(CriarPropostaRequest request)
        {
            var proposta = new Proposta(
                request.NomeCliente,
                request.CpfCliente,
                request.TipoSeguro,
                request.ValorPremio
            );

            var propostaCriada = await _repository.CriarAsync(proposta);

            return PropostaMapper.MapearParaResponse(propostaCriada);
        }
    }
}
