using Cumpridor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cumpridor.Repository
{
    public interface ITipoRepository
    {
        List<TipoEnt> GetTipos();
        int Add(TipoEnt tipo);
    }
}
