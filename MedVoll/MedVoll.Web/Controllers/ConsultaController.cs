using MedVoll.Web.Dtos;
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
        [Route("formulario/{id?}")]
        public async Task<IActionResult> CarregarPaginaAgendaConsultaAsync(long? id)
        {
            var dados = id.HasValue
                ? await _consultaservice.CarregarPorIdAsync(id.Value)
                //: new DadosAgendamentoConsulta { Data = DateTime.Now };
                : new ConsultaDto { Data = DateTime.Now };

            return await GetViewPaginaCadastro(dados);
        }


        [HttpPost]
        [Route("")]
        //public async Task<IActionResult> CadastrarAsync([FromForm] DadosAgendamentoConsulta dados)
        public async Task<IActionResult> CadastrarAsync([FromForm] ConsultaDto dados)
        {
            if (dados._method == "delete")
            {
                await _consultaservice.ExcluirAsync(dados.Id.Value);
                return Redirect("/consultas");
            }

            if (!ModelState.IsValid)
            {
                return await GetViewPaginaCadastro(dados);
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

        //private async Task<ViewResult> GetViewPaginaCadastro(DadosAgendamentoConsulta dados)
        private async Task<ViewResult> GetViewPaginaCadastro(ConsultaDto dados)
        {
            ViewData["Medicos"] = await _medicoService.ListarAsync();
            ViewResult viewPaginaCadastro = View(PaginaCadastro, dados);
            return viewPaginaCadastro;
        }

        //[HttpDelete]
        //[Route("{id}")]
        //public async Task<IActionResult> ExcluirAsync(long id)
        //{
        //    await _consultaservice.ExcluirAsync(id);
        //    return Redirect("/consultas");
        //}

        [HttpGet]
        [Route("especialidades")]
        public Especialidade[] Especialidades()
        {
            return (Especialidade[])Enum.GetValues(typeof(Especialidade));
        }
    }
}
