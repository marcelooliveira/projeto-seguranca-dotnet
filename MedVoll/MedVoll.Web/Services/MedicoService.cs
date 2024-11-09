using MedVoll.Web.Dtos;
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

        //public async Task<IEnumerable<DadosListagemMedico>> ListarAsync()
        public async Task<IEnumerable<MedicoDto>> ListarAsync()
        {
            var medicos = await _repository.GetAllAsync();
            //return medicos.Select(m => new DadosListagemMedico(m)).ToList();
            return medicos.Select(m => new MedicoDto(m)).ToList();
        }

        //public async Task CadastrarAsync(DadosCadastroMedico dados)
        public async Task CadastrarAsync(MedicoDto dados)
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

        //public async Task<DadosCadastroMedico> CarregarPorIdAsync(long id)
        public async Task<MedicoDto> CarregarPorIdAsync(long id)
        {
            var medico = await _repository.FindByIdAsync(id);
            if (medico == null) throw new Exception("Médico não encontrado.");

            //return new DadosCadastroMedico(medico);
            return new MedicoDto(medico);
        }

        public async Task ExcluirAsync(long id)
        {
            await _repository.DeleteByIdAsync(id);
        }

        //public async Task<IEnumerable<DadosListagemMedico>> ListarPorEspecialidadeAsync(Especialidade especialidade)
        public async Task<IEnumerable<MedicoDto>> ListarPorEspecialidadeAsync(Especialidade especialidade)
        {
            var medicos = await _repository.FindByEspecialidadeAsync(especialidade);
            //return medicos.Select(m => new DadosListagemMedico(m)).ToList();
            return medicos.Select(m => new MedicoDto(m)).ToList();
        }
    }
}


