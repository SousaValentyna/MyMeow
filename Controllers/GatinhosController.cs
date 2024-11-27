using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myMeow2.Data;

namespace myMeow2.Controllers
{
    public class GatinhosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GatinhosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var gatinhos = await _context.Gatinhos.Where(g => !g.Adotado).ToListAsync();
            return View(gatinhos);
        }
    }

}
