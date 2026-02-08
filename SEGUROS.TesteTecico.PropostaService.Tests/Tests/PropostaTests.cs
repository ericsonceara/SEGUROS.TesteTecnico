using SEGUROS.TesteTecnico.DOMAIN.Entities;
using Xunit;

namespace SEGUROS.TesteTecico.PropostaService.Tests.Domain
{
    public class PropostaTests
    {
        [Fact]
        public void DeveCriarProposta_ComStatusEmAnalise()
        {
            var proposta = new Proposta("João Silva", "12345678901", "Automóvel", 1500m);

            Assert.NotEqual(Guid.Empty, proposta.Id);
            Assert.Equal("João Silva", proposta.NomeCliente);
            Assert.Equal(StatusProposta.EmAnalise, proposta.Status);
        }

        [Fact]
        public void DeveAtualizarStatus_ParaAprovada_QuandoEmAnalise()
        {
            var proposta = new Proposta("João Silva", "12345678901", "Automóvel", 1500m);

            proposta.AtualizarStatus(StatusProposta.Aprovada);

            Assert.Equal(StatusProposta.Aprovada, proposta.Status);
            Assert.NotNull(proposta.DataAtualizacao);
        }

        [Fact]
        public void NaoDeveAtualizarStatus_QuandoJaAprovada()
        {
            var proposta = new Proposta("João Silva", "12345678901", "Automóvel", 1500m);
            proposta.AtualizarStatus(StatusProposta.Aprovada);

            var exception = Assert.Throws<InvalidOperationException>(() =>
                proposta.AtualizarStatus(StatusProposta.Rejeitada));

            Assert.Contains("Não é possível alterar status", exception.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCriarProposta_ComNomeClienteInvalido(string nomeInvalido)
        {
            Assert.Throws<ArgumentNullException>(() =>
                new Proposta(nomeInvalido, "12345678901", "Automóvel", 1500m));
        }

        [Fact]
        public void NaoDeveCriarProposta_ComValorPremioZero()
        {
            var exception = Assert.Throws<ArgumentException>(() =>
                new Proposta("João Silva", "12345678901", "Automóvel", 0m));

            Assert.Contains("Valor do prêmio deve ser maior que zero", exception.Message);
        }

        [Fact]
        public void EstaAprovada_DeveRetornarTrue_QuandoStatusAprovada()
        {
            var proposta = new Proposta("João Silva", "12345678901", "Automóvel", 1500m);
            proposta.AtualizarStatus(StatusProposta.Aprovada);

            Assert.True(proposta.EstaAprovada());
        }
    }
}
