using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cumpridor.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cumpridor.Pages.Cumpridor
{
    public class IndexModel : PageModel
    {
        ICumpridorRepository _cumpridorRepository;
        public IndexModel(ICumpridorRepository cumpridorRepository)
        {
            _cumpridorRepository = cumpridorRepository;
        }

        [BindProperty]
        public List<Entities.CumpridorEnt> listaCumpridores { get; set; }
        [BindProperty]
        public Entities.CumpridorEnt cumpridor { get; set; }

        [TempData]
        public string Message { get; set; }

        public void OnGet()
        {
            listaCumpridores = _cumpridorRepository.GetCumpridores();
        }
    }
}