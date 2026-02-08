using Moq;
using SEGUROS.TesteTecnico.ContratacaoService.DTOs;
using SEGUROS.TesteTecnico.ContratacaoService.UseCases;
using SEGUROS.TesteTecnico.DOMAIN.Entities;
using SEGUROS.TesteTecnico.DOMAIN.Ports;
using Xunit;
using static SEGUROS.TesteTecnico.DOMAIN.Ports.IPropostaServiceClient;

namespace SEGUROS.TesteTecico.ContratacaoService.Tests.Tests
{
    public class ContratarPropostaUseCaseTests
    {
        private readonly Mock<IContratacaoRepository> _repositoryMock;
        private readonly Mock<IPropostaServiceClient> _propostaClientMock;
        private readonly ContratarPropostaUseCase _useCase;

        public ContratarPropostaUseCaseTests()
        {
            _repositoryMock = new Mock<IContratacaoRepository>();
            _propostaClientMock = new Mock<IPropostaServiceClient>();
            _useCase = new ContratarPropostaUseCase(_repositoryMock.Object, _propostaClientMock.Object);
        }

        [Fact]
        public async Task DeveContratar_PropostaAprovada_ComSucesso()
        {
            var propostaId = Guid.NewGuid();
            var request = new ContratarPropostaRequest(propostaId);

            var propostaDto = new PropostaDto(
                propostaId,
                "João Silva",
                "12345678901",
                "Automóvel",
                1500m,
                "Aprovada",
                DateTime.UtcNow,
                DateTime.UtcNow
            );

            _propostaClientMock
                .Setup(c => c.ObterPropostaPorIdAsync(propostaId))
                .ReturnsAsync(propostaDto);

            _repositoryMock
                .Setup(r => r.ObterPorPropostaIdAsync(propostaId))
                .ReturnsAsync((Contratacao?)null);

            _repositoryMock
                .Setup(r => r.CriarAsync(It.IsAny<Contratacao>()))
                .ReturnsAsync((Contratacao c) => c);

            var response = await _useCase.ExecutarAsync(request);

            Assert.NotEqual(Guid.Empty, response.Id);
            Assert.Equal(propostaId, response.PropostaId);
            Assert.Equal("João Silva", response.NomeCliente);
            _repositoryMock.Verify(r => r.CriarAsync(It.IsAny<Contratacao>()), Times.Once);
        }

        [Fact]
        public async Task NaoDeveContratar_PropostaNaoAprovada()
        {
            var propostaId = Guid.NewGuid();
            var request = new ContratarPropostaRequest(propostaId);

            var propostaDto = new PropostaDto(
                propostaId,
                "João Silva",
                "12345678901",
                "Automóvel",
                1500m,
                "EmAnalise",
                DateTime.UtcNow,
                null
            );

            _propostaClientMock
                .Setup(c => c.ObterPropostaPorIdAsync(propostaId))
                .ReturnsAsync(propostaDto);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _useCase.ExecutarAsync(request));

            Assert.Contains("não pode ser contratada", exception.Message);
        }

        [Fact]
        public async Task NaoDeveContratar_PropostaJaContratada()
        {
            var propostaId = Guid.NewGuid();
            var request = new ContratarPropostaRequest(propostaId);

            var propostaDto = new PropostaDto(
                propostaId,
                "João Silva",
                "12345678901",
                "Automóvel",
                1500m,
                "Aprovada",
                DateTime.UtcNow,
                DateTime.UtcNow
            );

            var contratacaoExistente = new Contratacao(propostaId, "João Silva", 1500m);

            _propostaClientMock
                .Setup(c => c.ObterPropostaPorIdAsync(propostaId))
                .ReturnsAsync(propostaDto);

            _repositoryMock
                .Setup(r => r.ObterPorPropostaIdAsync(propostaId))
                .ReturnsAsync(contratacaoExistente);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _useCase.ExecutarAsync(request));

            Assert.Contains("já foi contratada", exception.Message);
        }
    }
}
