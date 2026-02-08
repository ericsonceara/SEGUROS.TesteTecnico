using SEGUROS.TesteTecnico.DOMAIN.Entities;
using SEGUROS.TesteTecnico.DOMAIN.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEGUROS.TesteTecnico.INFRASTRUCTURE.Repositories
{
    public class PropostaRepository : IPropostaRepository
    {
        private readonly List<Proposta> _propostas = new();

        public Task<Proposta> AtualizarAsync(Proposta proposta)
        {
            var index = _propostas.FindIndex(p => p.Id == proposta.Id);
            if (index >= 0)
            {
                _propostas[index] = proposta;
            }
            return Task.FromResult(proposta);
        }

        public Task<Proposta> CriarAsync(Proposta proposta)
        {
            _propostas.Add(proposta);
            return Task.FromResult(proposta);
        }

        public Task<IEnumerable<Proposta>> ListarTodasAsync()
        {
            return Task.FromResult<IEnumerable<Proposta>>(_propostas);
        }

        public Task<Proposta?> ObterPorIdAsync(Guid id)
        {
            var proposta = _propostas.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(proposta);
        }
    }
}
