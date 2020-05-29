using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cumpridor.Entities
{
    public class ServicosEnt
    {
        public Guid Id { get; set; }
        public string Cliente { get; set; }
        public DateTime DataAgendamento { get; set; }
        public string Observacao { get; set; }
        public int Aceitar { get; set; }
        public int Concluido { get; set; }
        public int TipoId { get; set; }
        public TipoEnt Tipo { get; set; }
    }
}
