using Microsoft.AspNetCore.Mvc;
using SEGUROS.TesteTecnico.APPLICATION.DTOs;
using SEGUROS.TesteTecnico.APPLICATION.UseCases;

namespace SEGUROS.TesteTecnico.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropostasController : ControllerBase
    {
        private readonly CriarPropostaUseCase _criarUseCase;
        private readonly ListarPropostasUseCase _listarUseCase;
        private readonly AtualizarStatusPropostaUseCase _atualizarStatusUseCase;
        private readonly ObterPropostaPorIdUseCase _obterPorIdUseCase;

        public PropostasController(CriarPropostaUseCase criarUseCase,
                    ListarPropostasUseCase listarUseCase,
                    AtualizarStatusPropostaUseCase atualizarStatusUseCase,
                    ObterPropostaPorIdUseCase obterPorIdUseCase)
        {
            _criarUseCase = criarUseCase;
            _listarUseCase = listarUseCase;
            _atualizarStatusUseCase = atualizarStatusUseCase;
            _obterPorIdUseCase = obterPorIdUseCase;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PropostaResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Criar([FromBody] CriarPropostaRequest request)
        {
            try
            {
                var response = await _criarUseCase.ExecutarAsync(request);
                return CreatedAtAction(nameof(ObterPorId), new { id = response.Id }, response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PropostaResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Listar()
        {
            var propostas = await _listarUseCase.ExecutarAsync();
            return Ok(propostas);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PropostaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var proposta = await _obterPorIdUseCase.ExecutarAsync(id);

            if (proposta is null)
                return NotFound(new { erro = "Proposta não encontrada" });

            return Ok(proposta);
        }

        [HttpPatch("{id}/status")]
        [ProducesResponseType(typeof(PropostaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AtualizarStatus(Guid id, [FromBody] AtualizarStatusRequest request)
        {
            try
            {
                var response = await _atualizarStatusUseCase.ExecutarAsync(id, request);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}
