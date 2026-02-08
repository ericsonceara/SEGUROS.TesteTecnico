using SEGUROS.TesteTecnico.DOMAIN.Entities;
using Xunit;

namespace SEGUROS.TesteTecico.ContratacaoService.Tests.Tests
{
    public class ContratacaoTests
    {
        [Fact]
        public void DeveCriarContratacao_ComDadosValidos()
        {
            var propostaId = Guid.NewGuid();

            var contratacao = new Contratacao(propostaId, "João Silva", 1500m);

            Assert.NotEqual(Guid.Empty, contratacao.Id);
            Assert.Equal(propostaId, contratacao.PropostaId);
            Assert.Equal("João Silva", contratacao.NomeCliente);
            Assert.Equal(1500m, contratacao.ValorPremio);
        }

        [Fact]
        public void NaoDeveCriarContratacao_ComNomeClienteNulo()
        {
            var propostaId = Guid.NewGuid();

            Assert.Throws<ArgumentNullException>(() =>
                new Contratacao(propostaId, null!, 1500m));
        }
    }
}
