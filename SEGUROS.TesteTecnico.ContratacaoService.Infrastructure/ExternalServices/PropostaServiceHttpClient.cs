using Microsoft.Extensions.Logging;
using SEGUROS.TesteTecnico.DOMAIN.Ports;
using System.Text.Json;
using static SEGUROS.TesteTecnico.DOMAIN.Ports.IPropostaServiceClient;

namespace SEGUROS.TesteTecnico.ContratacaoService.Infrastructure.ExternalServices
{
    public class PropostaServiceHttpClient : IPropostaServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PropostaServiceHttpClient> _logger;

        public PropostaServiceHttpClient(HttpClient httpClient, ILogger<PropostaServiceHttpClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IPropostaServiceClient.PropostaDto?> ObterPropostaPorIdAsync(Guid propostaId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/propostas/{propostaId}");

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Proposta não encontrada: {PropostaId}", propostaId);
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                var proposta = JsonSerializer.Deserialize<PropostaDto>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return proposta;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar proposta {PropostaId}", propostaId);
                throw new InvalidOperationException("Erro ao comunicar com o serviço de propostas", ex);
            }
        }
    }
}
