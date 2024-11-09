﻿using MedVoll.Web.Dtos;
using MedVoll.Web.Interfaces;
using MedVoll.Web.Models;

namespace MedVoll.Web.Services
{
    public class ConsultaService : IConsultaService
    {
        private readonly IConsultaRepository _consultaRepository;
        private readonly IMedicoRepository _medicoRepository;

        public ConsultaService(IConsultaRepository consultaRepository, IMedicoRepository medicoRepository)
        {
            _consultaRepository = consultaRepository;
            _medicoRepository = medicoRepository;
        }

        //public async Task<IEnumerable<DadosListagemConsulta>> ListarAsync()
        public async Task<IEnumerable<ConsultaDto>> ListarAsync()
        {
            var consultas = await _consultaRepository.GetAllOrderedByDataAsync();
            //return consultas.Select(c => new DadosListagemConsulta(c)).ToList();
            return consultas.Select(c => new ConsultaDto(c)).ToList();
        }

        //public async Task CadastrarAsync(DadosAgendamentoConsulta dados)
        public async Task CadastrarAsync(ConsultaDto dados)
        {
            var medicoConsulta = await _medicoRepository.FindByIdAsync(dados.IdMedico);
            if (medicoConsulta == null)
            {
                throw new Exception("Medico não encontrado.");
            }

            if (dados.Id == null)
            {
                var consulta = new Consulta(medicoConsulta, dados);
                await _consultaRepository.SaveAsync(consulta);
            }
            else
            {
                var consulta = await _consultaRepository.FindByIdAsync(dados.Id.Value);
                if (consulta == null) throw new Exception("Consulta não encontrada.");

                consulta.ModificarDados(medicoConsulta, dados);
                await _consultaRepository.UpdateAsync(consulta);
            }
        }

        //public async Task<DadosAgendamentoConsulta> CarregarPorIdAsync(long id)
        public async Task<ConsultaDto> CarregarPorIdAsync(long id)
        {
            var consulta = await _consultaRepository.FindByIdAsync(id);
            if (consulta == null) throw new Exception("Consulta não encontrada.");

            var medicoConsulta = await _medicoRepository.FindByIdAsync(consulta.Medico.Id);
            //return new DadosAgendamentoConsulta(consulta.Id, consulta.Medico.Id, consulta.Paciente, consulta.Data, medicoConsulta.Especialidade);
            return new ConsultaDto(consulta.Id, consulta.Medico.Id, consulta.Paciente, consulta.Data, medicoConsulta.Especialidade);
        }

        public async Task ExcluirAsync(long id)
        {
            await _consultaRepository.DeleteByIdAsync(id);
        }
    }
}


