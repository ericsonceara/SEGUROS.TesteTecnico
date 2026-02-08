using Microsoft.AspNetCore.Mvc;
using SEGUROS.TesteTecnico.ContratacaoService.DTOs;
using SEGUROS.TesteTecnico.ContratacaoService.UseCases;

namespace SEGUROS.TesteTecnico.Contratacao.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContratacoesController : ControllerBase
    {
        private readonly ContratarPropostaUseCase _contratarUseCase;
        private readonly ListarContratacoesUseCase _listarUseCase;

        public ContratacoesController(ContratarPropostaUseCase contratarUseCase, ListarContratacoesUseCase listarUseCase)
        {
            _contratarUseCase = contratarUseCase;
            _listarUseCase = listarUseCase;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ContratacaoResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Contratar([FromBody] ContratarPropostaRequest request)
        {
            try
            {
                var response = await _contratarUseCase.ExecutarAsync(request);
                return CreatedAtAction(nameof(Contratar), new { id = response.Id }, response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ContratacaoResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Listar()
        {
            var contratacoes = await _listarUseCase.ExecutarAsync();
            return Ok(contratacoes);
        }
    }
}
