namespace SEGUROS.TesteTecnico.DOMAIN.Entities
{
    public abstract class Base
    {
        public Guid Id { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataAtualizacao { get; private set; }
    }
}
