using Moq;
using SEGUROS.TesteTecnico.APPLICATION.DTOs;
using SEGUROS.TesteTecnico.APPLICATION.UseCases;
using SEGUROS.TesteTecnico.DOMAIN.Entities;
using SEGUROS.TesteTecnico.DOMAIN.Ports;
using Xunit;

namespace SEGUROS.TesteTecico.PropostaService.Tests.Tests
{
    public class CriarPropostaUseCaseTests
    {
        private readonly Mock<IPropostaRepository> _repositoryMock;
        private readonly CriarPropostaUseCase _useCase;

        public CriarPropostaUseCaseTests()
        {
            _repositoryMock = new Mock<IPropostaRepository>();
            _useCase = new CriarPropostaUseCase(_repositoryMock.Object);
        }

        [Fact]
        public async Task DeveExecutar_CriacaoDeProposta_ComSucesso()
        {
            var request = new CriarPropostaRequest("João Silva", "12345678901", "Automóvel", 1500m);

            _repositoryMock
                .Setup(r => r.CriarAsync(It.IsAny<Proposta>()))
                .ReturnsAsync((Proposta p) => p);

            var response = await _useCase.ExecutarAsync(request);

            Assert.NotEqual(Guid.Empty, response.Id);
            Assert.Equal("João Silva", response.NomeCliente);
            Assert.Equal("EmAnalise", response.Status);
            _repositoryMock.Verify(r => r.CriarAsync(It.IsAny<Proposta>()), Times.Once);
        }
    }
}
