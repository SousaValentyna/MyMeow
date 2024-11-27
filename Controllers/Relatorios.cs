using Microsoft.AspNetCore.Mvc;
using myMeow2.Data;
using myMeow2.Logical;

namespace myMeow2.Controllers
{
    public class RelatoriosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RelatoriosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var relatorios = new Relatorios(_context);

            // Carrega os dados
            relatorios.adicionarGatinhosAdotados();
            relatorios.adicionarAdotantes();
            relatorios.adicionarAdotantesQueAdotaramMaisDeUmGato();

            // Prepara os dados para a View
            var gatinhos = relatorios.getGatinhosAdotados();
            var adotantes = relatorios.getAdotantes();
            var adotantesQueAdotaramMaisDeUmGato = relatorios.getAdotantesQueAdotaramMaisDeUmGato();

            // Passa os dados para a View
            ViewBag.Gatinhos = gatinhos;
            ViewBag.Adotantes = adotantes;
            ViewBag.AdotantesQueAdotaramMaisDeUmGato = adotantesQueAdotaramMaisDeUmGato;

            return View();
        }
    }
}
