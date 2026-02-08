namespace SEGUROS.TesteTecnico.DOMAIN.Entities
{
    public class Proposta: Base
    {

        public Proposta(string nomeCliente, string cpfCliente, string tipoSeguro, decimal valorPremio)
        {
            Id = Guid.NewGuid();
            NomeCliente = nomeCliente ?? throw new ArgumentNullException(nameof(nomeCliente));
            CpfCliente = cpfCliente ?? throw new ArgumentNullException(nameof(cpfCliente));
            TipoSeguro = tipoSeguro ?? throw new ArgumentNullException(nameof(tipoSeguro));
            ValorPremio = valorPremio > 0 ? valorPremio : throw new ArgumentException("Valor do prêmio deve ser maior que zero");
            Status = StatusProposta.EmAnalise;
            DataCriacao = DateTime.UtcNow;
        }


        public Guid Id { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataAtualizacao { get; private set; }
        public string NomeCliente { get; private set; }
        public string CpfCliente { get; private set; }
        public string TipoSeguro { get; private set; }
        public decimal ValorPremio { get; private set; }
        public StatusProposta Status { get; private set; }


        public void AtualizarStatus(StatusProposta novoStatus)
        {
            if (!PodeAlterarPara(novoStatus))
                throw new InvalidOperationException($"Não é possível alterar status de {Status} para {novoStatus}");

            Status = novoStatus;
            DataAtualizacao = DateTime.UtcNow;
        }
        private bool PodeAlterarPara(StatusProposta novoStatus)
        {
            return Status switch
            {
                StatusProposta.EmAnalise => novoStatus == StatusProposta.Aprovada || novoStatus == StatusProposta.Rejeitada,
                StatusProposta.Aprovada => false,
                StatusProposta.Rejeitada => false,
                _ => false
            };
        }

        public bool EstaAprovada() => Status == StatusProposta.Aprovada;
    }
}
