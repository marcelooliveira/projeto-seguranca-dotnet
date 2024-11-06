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
using MedVoll.Web.Dados;

namespace MedVoll.Web.Models
{
    [Table("consultas")]
    public class Consulta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }

        [Required]
        public string Paciente { get; private set; }

        [ForeignKey("MedicoId")]
        [JsonIgnore]
        public Medico Medico { get; private set; }

        public DateTime Data { get; private set; }

        public Consulta() { }

        public Consulta(Medico medico, DadosAgendamentoConsulta dados)
        {
            ModificarDados(medico, dados);
        }

        public void ModificarDados(Medico medico, DadosAgendamentoConsulta dados)
        {
            Medico = medico;
            Paciente = dados.Paciente;
            Data = dados.Data;
        }
    }
}


