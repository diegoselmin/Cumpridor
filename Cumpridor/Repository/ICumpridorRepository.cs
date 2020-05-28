using System;
using System.Collections.Generic;
using Cumpridor.Entities;

namespace Cumpridor.Repository
{
    public interface ICumpridorRepository
    {
        int Add(CumpridorEnt cumpridor);
        List<CumpridorEnt> GetCumpridores();

        CumpridorEnt Get(int id);
        int Edit(CumpridorEnt cumpridor);
        int Delete(int id);

        CumpridorEnt LogOn(CumpridorEnt cumpridor);
    }
}
