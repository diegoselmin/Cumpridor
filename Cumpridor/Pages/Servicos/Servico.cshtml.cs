using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Cumpridor.Entities;
using Cumpridor.Repository;
using javax.jws;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;


namespace Cumpridor.Pages.Servicos
{
    public class ServicoModel : PageModel
    {
        IServicosRepository _servicosRepository;
        public ServicoModel(IServicosRepository servicosRepository)
        {
            _servicosRepository = servicosRepository;
        }

        [BindProperty]
        public List<ServicosEnt> filas { get; set; }

        [BindProperty]
        public string user { get; set; }

        [TempData]
        public string Message { get; set; }

        public async Task OnGetAsync()
        {
            user = HttpContext.Session.GetString("user");
            //var model = new CumpridorEnt();
            var url = "http://virtserver.swaggerhub.com/finch/avaliacao/1.0.0/fila";
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(url))
                {
                    using (var content = response.Content)
                    {
                        //get the json result from your api
                        var result = await content.ReadAsStringAsync();
                        filas = InserirServicos(JsonConvert.DeserializeObject<List<ServicosEnt>>(result));
                    }
                }

            }

        }


        public IActionResult OnGetAceitar(string Id)
        {
            try
            { 
                _servicosRepository.AceitarServico(Guid.Parse(Id));
                Message = "Serviço Aceito!!";
                return RedirectToPage("/Servicos/Servico");

            }
            catch (Exception ex) 
            {
                Message = ex.Message;
            }


            return Page();
        }

        public IActionResult OnGetConcluir(string Id)
        {
            try
            {
                _servicosRepository.ConcluirServico(Guid.Parse(Id));
                Message = "Serviço Concluído!!";
                return RedirectToPage("/Servicos/Servico");

            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }


            return Page();
        }

        public List<ServicosEnt> InserirServicos(List<ServicosEnt> pFila)
        {

            var s = _servicosRepository.AddList(pFila);

            List<ServicosEnt> servicosDisponiveis = _servicosRepository.GetServicos().Where(s => s.Aceitar == 0 && s.Concluido == 0).ToList();

            return servicosDisponiveis;
        }

    }
}