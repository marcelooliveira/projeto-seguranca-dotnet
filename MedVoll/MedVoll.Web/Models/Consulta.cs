using System;
using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using MedVoll.Web.Dtos;

namespace MedVoll.Web.Models
{
    [Table("consultas")]
    public class Consulta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }

        [Required(ErrorMessage = "Campo obrigatório"), StringLength(11, MinimumLength =11, ErrorMessage = "CPF deve ter 11 digitos")]
        public string Paciente { get; private set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public long MedicoId { get; private set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public DateTime Data { get; private set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public virtual Medico Medico { get; private set; }

        public Consulta() { }

        //public Consulta(Medico medico, DadosAgendamentoConsulta dados)
        public Consulta(Medico medico, ConsultaDto dados)
        {
            ModificarDados(medico, dados);
        }

        //public void ModificarDados(Medico medico, DadosAgendamentoConsulta dados)
        public void ModificarDados(Medico medico, ConsultaDto dados)
        {
            Medico = medico;
            Paciente = dados.Paciente;
            Data = dados.Data;
        }
    }
}


