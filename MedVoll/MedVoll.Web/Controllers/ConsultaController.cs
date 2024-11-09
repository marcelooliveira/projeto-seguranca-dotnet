using MedVoll.Web.Dados;
using MedVoll.Web.Exceptions;
using MedVoll.Web.Interfaces;
using MedVoll.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedVoll.Web.Controllers
{
    [Route("consultas")]
    public class ConsultaController : BaseController
    {
        private const string PaginaListagem = "ListagemConsultas";
        private const string PaginaCadastro = "FormularioConsulta";

        private readonly IConsultaService _consultaservice;
        private readonly IMedicoService _medicoService;

        public ConsultaController(IConsultaService consultaService, IMedicoService medicoService)
        {
            _consultaservice = consultaService;
            _medicoService = medicoService;
        }

        [HttpGet]
        [Route("{page?}")]
        public async Task<IActionResult> CarregarPaginaListagemAsync([FromQuery] int page = 1)
        {
            var consultasAtivas = await _consultaservice.ListarAsync();
            ViewBag.Consultas = consultasAtivas;
            return View(PaginaListagem, consultasAtivas);
        }

        [HttpGet]
        [Route("formulario/{id}")]
        public async Task<IActionResult> CarregarPaginaAgendaConsultaAsync(long? id)
        {
            var dados = id.HasValue 
                ? await _consultaservice.CarregarPorIdAsync(id.Value) 
                : new DadosAgendamentoConsulta();

            ViewData["Medicos"] = await _medicoService.ListarAsync();
            return View(PaginaCadastro, dados);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CadastrarAsync([FromForm] DadosAgendamentoConsulta dados)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Dados = dados;
                return View(PaginaCadastro);
            }

            try
            {
                await _consultaservice.CadastrarAsync(dados);
                return Redirect("/consultas");
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
            await _consultaservice.ExcluirAsync(id);
            return Redirect("/consultas");
        }

        [HttpGet]
        [Route("especialidades")]
        public Especialidade[] Especialidades()
        {
            return (Especialidade[])Enum.GetValues(typeof(Especialidade));
        }
    }
}
