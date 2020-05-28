using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cumpridor.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cumpridor.Pages.Cumpridor
{
    public class AddModel : PageModel
    {

        ICumpridorRepository _cumpridorRepository;
        public AddModel(ICumpridorRepository cumpridorRepository)
        {
            _cumpridorRepository = cumpridorRepository;
        }

        [BindProperty]
        public Entities.CumpridorEnt cumpridor { get; set; }

        [TempData]
        public string Message { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var count = _cumpridorRepository.Add(cumpridor);
                if(count > 0)
                {
                    Message = "Novo Cumpridor incluído com sucesso!";
                    return RedirectToPage("/Cumpridor/Index");
                }
            }
            return Page();
        }
    }
}