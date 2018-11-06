using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TestePratico.Models;

namespace TestePratico.Controllers
{
    public class RepositoriosController : Controller
    {
        private readonly GitHubContext _context;

        public RepositoriosController(GitHubContext context)
        {
            _context = context;
        }

        // GET: Repositorios
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var url = new Uri($"https://api.github.com/repositories?since=364");
            List<Repositorios> repositorio = new List<Repositorios>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Other");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string lista = await response.Content.ReadAsStringAsync();
                    repositorio = JsonConvert.DeserializeObject<List<Repositorios>>(lista) as  List<Repositorios>;
                }
                if (ModelState.IsValid)
                {
                    foreach (var item in repositorio)
                    {
                        _context.Add(item);
                        await _context.SaveChangesAsync();
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(repositorio);
        }

        // GET: Repositorios/Detalhes/5
        public async Task<IActionResult> Detalhes(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var repositorios = await _context.Repositorios
                .FirstOrDefaultAsync(m => m.Nome == id);
            if (repositorios == null)
            {
                return NotFound();
            }
            return View(repositorios);
        }

        [HttpGet]
        public async Task<IActionResult> BuscarPorLinguagem(string linguagem)
        {
            if (linguagem == null)
            {
                return NotFound();
            }
            var url = new Uri($"https://api.github.com/search/repositories?q=stars:%3E=10000+language:{linguagem}&sort=stars&order=desc");
            List<Repositorios> items = new List<Repositorios>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Outro");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string lista = await response.Content.ReadAsStringAsync();
                    var item = JsonConvert.DeserializeObject<RootObject>(lista) as RootObject;
                    items = item.items;
                }
            }
                return RedirectToAction("BuscarPorLinguagem", items);
        }
        
        private bool RepositoriosExists(string id)
        {
            return _context.Repositorios.Any(e => e.Nome == id);
        }
    }
}
