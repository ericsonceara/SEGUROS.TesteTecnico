using SEGUROS.TesteTecnico.APPLICATION.DTOs;
using SEGUROS.TesteTecnico.DOMAIN.Entities;

namespace SEGUROS.TesteTecnico.APPLICATION.Mappers
{
    public static class PropostaMapper
    {
        public static PropostaResponse MapearParaResponse(Proposta proposta)
        {
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
