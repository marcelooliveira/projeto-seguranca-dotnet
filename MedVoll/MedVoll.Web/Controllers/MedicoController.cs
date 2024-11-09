using MedVoll.Web.Dtos;
using MedVoll.Web.Exceptions;
using MedVoll.Web.Interfaces;
using MedVoll.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MedVoll.Web.Controllers
{
    [Route("medicos")]
    public class MedicoController : BaseController
    {
        private const string PaginaListagem = "ListagemMedicos";
        private const string PaginaCadastro = "FormularioMedico";
        private readonly IMedicoService _service;

        public MedicoController(IMedicoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{page?}")]
        public async Task<IActionResult> CarregarPaginaListagemAsync([FromQuery] int page = 1)
        {
            var medicosCadastrados = await _service.ListarAsync();
            return View(PaginaListagem, medicosCadastrados);
        }

        [HttpGet]
        [Route("formulario/{id?}")]
        public async Task<IActionResult> CarregarPaginaCadastroAsync(long? id)
        {
            var dados = id.HasValue 
                ? await _service.CarregarPorIdAsync(id.Value) 
                //: new DadosCadastroMedico();
                : new MedicoDto();

            return View(PaginaCadastro, dados);
        }

        [HttpPost]
        [Route("")]
        //public async Task<IActionResult> CadastrarAsync([FromForm] DadosCadastroMedico dados)
        public async Task<IActionResult> CadastrarAsync([FromForm] MedicoDto dados)
        {
            if (!ModelState.IsValid)
            {
                return View(PaginaCadastro, dados);
            }

            try
            {
                await _service.CadastrarAsync(dados);
                return Redirect("/medicos");
            }
            catch (RegraDeNegocioException ex)
            {
                ViewBag.Erro = ex.Message;
                ViewBag.Dados = dados;
                return View(PaginaCadastro);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> ExcluirAsync(long id)
        {
            await _service.ExcluirAsync(id);
            return Redirect("/medicos");
        }

        [HttpGet]
        [Route("especialidade/{especialidade}")]
        public async Task<IActionResult> ListarMedicosPorEspecialidadeAsync(string especialidade)
        {
            if (Enum.TryParse(especialidade, out Especialidade especEnum))
            {
                var medicos = await _service.ListarPorEspecialidadeAsync(especEnum);
                return Json(medicos);
            }
            return BadRequest("Especialidade inválida.");
        }

        [HttpGet]
        [Route("especialidades")]
        public Especialidade[] Especialidades()
        {
            return (Especialidade[])Enum.GetValues(typeof(Especialidade));
        }
    }
}
