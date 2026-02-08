namespace SEGUROS.TesteTecnico.DOMAIN.Entities
{
    public class Contratacao
    {
        public Contratacao(Guid propostaId, string nomeCliente, decimal valorPremio)
        {
            Id = Guid.NewGuid();
            PropostaId = propostaId;
            NomeCliente = nomeCliente ?? throw new ArgumentNullException(nameof(nomeCliente));
            ValorPremio = valorPremio;
            DataContratacao = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public Guid PropostaId { get; private set; }
        public DateTime DataContratacao { get; private set; }
        public string NomeCliente { get; private set; }
        public decimal ValorPremio { get; private set; }
    }
}
