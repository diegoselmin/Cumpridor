using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cumpridor.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cumpridor.Pages.Cumpridor
{
    public class LoginModel : PageModel
    {
        ICumpridorRepository _cumpridorRepository;
        public LoginModel(ICumpridorRepository cumpridorRepository)
        {
            _cumpridorRepository = cumpridorRepository;
        }

        [BindProperty]
        public List<Entities.CumpridorEnt> listaCumpridores { get; set; }

        [BindProperty]
        public Entities.CumpridorEnt login { get; set; }

        [TempData]
        public string Message { get; set; }
        public void OnGet()
        {
            listaCumpridores = _cumpridorRepository.GetCumpridores();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {

                var cumpridor = _cumpridorRepository.LogOn(login);
                if (cumpridor != null)
                {
                    Message = "Cumpridor Logado com Sucesso!";
                    return RedirectToPage("/Index", new { user = cumpridor.Nome });
                   
                }
            }
            return Page();
        }
    }
}