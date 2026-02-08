using SEGUROS.TesteTecnico.ContratacaoService.DTOs;
using SEGUROS.TesteTecnico.DOMAIN.Entities;
using SEGUROS.TesteTecnico.DOMAIN.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace SEGUROS.TesteTecnico.ContratacaoService.UseCases
{
    public class ContratarPropostaUseCase
    {
        private readonly IContratacaoRepository _repository;
        private readonly IPropostaServiceClient _propostaClient;

        public ContratarPropostaUseCase(IContratacaoRepository repository, IPropostaServiceClient propostaClient)
        {
            _repository = repository;
            _propostaClient = propostaClient;
        }

        public async Task<ContratacaoResponse> ExecutarAsync(ContratarPropostaRequest request)
        {
            var proposta = await _propostaClient.ObterPropostaPorIdAsync(request.PropostaId);

            if (proposta is null)
                throw new InvalidOperationException("Proposta não encontrada");

            if (proposta.Status != "Aprovada")
                throw new InvalidOperationException($"Proposta não pode ser contratada. Status atual: {proposta.Status}");

            var contratacaoExistente = await _repository.ObterPorPropostaIdAsync(request.PropostaId);
            if (contratacaoExistente is not null)
                throw new InvalidOperationException("Proposta já foi contratada anteriormente");

            var contratacao = new Contratacao(
                proposta.Id,
                proposta.NomeCliente,
                proposta.ValorPremio
            );

            var contratacaoCriada = await _repository.CriarAsync(contratacao);

            return new ContratacaoResponse(
                contratacaoCriada.Id,
                contratacaoCriada.PropostaId,
                contratacaoCriada.NomeCliente,
                contratacaoCriada.ValorPremio,
                contratacaoCriada.DataContratacao
            );
        }
    }
}
