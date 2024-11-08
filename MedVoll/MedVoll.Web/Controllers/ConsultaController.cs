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

        private readonly IConsultaService _service;

        public ConsultaController(IConsultaService consultaService)
        {
            _service = consultaService;
        }

        [HttpGet]
        [Route("{page?}")]
        public async Task<IActionResult> CarregarPaginaListagemAsync([FromQuery] int page = 1)
        {
            var consultasAtivas = await _service.ListarAsync();
            ViewBag.Consultas = consultasAtivas;
            return View(PaginaListagem, consultasAtivas);
        }

        [HttpGet]
        [Route("formulario/{id}")]
        public async Task<IActionResult> CarregarPaginaAgendaConsultaAsync(long? id)
        {
            var dados = id.HasValue 
                ? await _service.CarregarPorIdAsync(id.Value) 
                : new DadosAgendamentoConsulta();

            //ViewData["Medicos"] = 
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
                await _service.CadastrarAsync(dados);
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
            await _service.ExcluirAsync(id);
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
