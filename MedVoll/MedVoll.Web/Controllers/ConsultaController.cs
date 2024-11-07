using MedVoll.Web.Dados;
using MedVoll.Web.Exceptions;
using MedVoll.Web.Interfaces;
using MedVoll.Web.Models;
using MedVoll.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MedVoll.Web.Controllers
{
    [Route("consultas")]
    public class ConsultaController : Controller
    {
        private const string PaginaListagem = "ListagemConsultas";
        private const string PaginaCadastro = "FormularioConsulta";
        private const string RedirectListagem = "redirect:/consultas?sucesso";

        private readonly IConsultaService _service;

        public ConsultaController(IConsultaService consultaService)
        {
            _service = consultaService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> CarregarPaginaListagemAsync([FromQuery] int page = 1)
        {
            var consultasAtivas = await _service.ListarAsync();
            ViewBag.Consultas = consultasAtivas;
            return View(PaginaListagem, consultasAtivas);
        }

        [HttpGet]
        [Route("formulario")]
        public async Task<IActionResult> CarregarPaginaAgendaConsultaAsync(long? id)
        {
            var dados = id.HasValue 
                ? await _service.CarregarPorIdAsync(id.Value) 
                : new DadosAgendamentoConsulta();
            ViewBag.Dados = dados;
            return View(PaginaCadastro);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CadastrarAsync([BindRequired] DadosAgendamentoConsulta dados)
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
        [Route("especialidades")]
        public Especialidade[] Especialidades()
        {
            return (Especialidade[])Enum.GetValues(typeof(Especialidade));
        }
    }
}
