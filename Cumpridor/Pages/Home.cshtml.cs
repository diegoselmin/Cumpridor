using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Cumpridor.Pages
{
    public class HomeModel : PageModel
    {
        private readonly ILogger<HomeModel> _logger;

        public HomeModel(ILogger<HomeModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public string userName { get; set; }
        public IActionResult OnGet()
        {
            var pUser = Request.Query["user"].ToString();
            if (pUser != "") 
            {
                userName = Request.Query["user"].ToString();
                return Page();
            }
            else
            {
                return RedirectToPage("/Cumpridor/Login");
            }

            
        }
    }
}
