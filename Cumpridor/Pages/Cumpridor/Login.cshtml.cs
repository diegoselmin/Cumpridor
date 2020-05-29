using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cumpridor.Repository;
using Microsoft.AspNetCore.Http;
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
                try
                {
                    var cumpridor = _cumpridorRepository.LogOn(login);

                    if (cumpridor != null)
                    {
                        HttpContext.Session.SetString("user", cumpridor.Nome);
                        Message = "Cumpridor " + login.Nome + " Logado com Sucesso!";
                        //return RedirectToPage("/Index", new { user = cumpridor.Nome });
                        return RedirectToPage("/Servicos/Servico");

                    }
                }
                catch (Exception ex) 
                {
                    Message = "Falha ao realizar o login! - ERROR : " + ex.Message;
                    return Page();
                }

                
            }
            return Page();
        }
    }
}