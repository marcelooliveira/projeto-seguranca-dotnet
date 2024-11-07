using MedVoll.Web.Dados;
using MedVoll.Web.Exceptions;
using MedVoll.Web.Interfaces;
using MedVoll.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MedVoll.Web.Controllers
{
    [Route("medicos")]
    public class MedicoController : Controller
    {
        private const string PaginaListagem = "ListagemMedicos";
        private const string PaginaCadastro = "FormularioMedico";
        private const string RedirectListagem = "redirect:/medicos?sucesso";
        private readonly IMedicoService _service;

        public MedicoController(IMedicoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> CarregarPaginaListagemAsync([FromQuery] int page = 1)
        {
            var medicosCadastrados = await _service.ListarAsync();
            ViewBag.Medicos = medicosCadastrados;
            return View(PaginaListagem, medicosCadastrados);
        }

        [HttpGet]
        [Route("formulario")]
        public async Task<IActionResult> CarregarPaginaCadastroAsync(long? id)
        {
            var dados = id.HasValue 
                ? await _service.CarregarPorIdAsync(id.Value) 
                : new DadosCadastroMedico();

            ViewBag.Dados = dados;
            return View(PaginaCadastro);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CadastrarAsync([BindRequired] DadosCadastroMedico dados)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Dados = dados;
                return View(PaginaCadastro);
            }

            try
            {
                await _service.CadastrarAsync(dados);
                return Redirect(RedirectListagem);
            }
            catch (RegraDeNegocioException ex)
            {
                ViewBag.Erro = ex.Message;
                ViewBag.Dados = dados;
                return View(PaginaCadastro);
            }
        }

        [HttpDelete]
        [Route("")]
        public async Task<IActionResult> ExcluirAsync(long id)
        {
            await _service.ExcluirAsync(id);
            return Redirect(RedirectListagem);
        }

        [HttpGet]
        [Route("{especialidade}")]
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
