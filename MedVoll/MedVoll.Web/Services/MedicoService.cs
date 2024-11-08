﻿using MedVoll.Web.Dados;
using MedVoll.Web.Interfaces;
using MedVoll.Web.Models;

namespace MedVoll.Web.Services
{
    public class MedicoService : IMedicoService
    {
        private readonly IMedicoRepository _repository;

        public MedicoService(IMedicoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DadosListagemMedico>> ListarAsync()
        {
            var medicos = await _repository.GetAllAsync();
            return medicos.Select(m => new DadosListagemMedico(m)).ToList();
        }

        public async Task CadastrarAsync(DadosCadastroMedico dados)
        {
            if (await _repository.IsJaCadastradoAsync(dados.Email, dados.Crm, dados.Id))
            {
                throw new Exception("E-mail ou CRM já cadastrado para outro médico!");
            }

            if (dados.Id == null)
            {
                var medico = new Medico(dados);
                await _repository.InsertAsync(medico);
            }
            else
            {
                var medico = await _repository.FindByIdAsync(dados.Id.Value);
                if (medico == null) throw new Exception("Médico não encontrado.");

                medico.AtualizarDados(dados);
                await _repository.UpdateAsync(medico);
            }
        }

        public async Task<DadosCadastroMedico> CarregarPorIdAsync(long id)
        {
            var medico = await _repository.FindByIdAsync(id);
            if (medico == null) throw new Exception("Médico não encontrado.");

            return new DadosCadastroMedico(medico.Id, medico.Nome, medico.Email, medico.Telefone, medico.Crm, medico.Especialidade);
        }

        public async Task ExcluirAsync(long id)
        {
            await _repository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<DadosListagemMedico>> ListarPorEspecialidadeAsync(Especialidade especialidade)
        {
            var medicos = await _repository.FindByEspecialidadeAsync(especialidade);
            return medicos.Select(m => new DadosListagemMedico(m)).ToList();
        }
    }
}


