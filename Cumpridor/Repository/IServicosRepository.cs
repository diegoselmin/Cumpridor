using Cumpridor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cumpridor.Repository
{
    public interface IServicosRepository
    {
        int AddList(List<ServicosEnt> servicos);

        List<ServicosEnt> GetServicos();

        void AceitarServico(Guid Id);
        void ConcluirServico(Guid Id);
    }
}
