using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myMeow2.Data;
using myMeow2.Models;
using System.Linq;

namespace myMeow2.Controllers
{
    public class AdocaoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdocaoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Create(int gatinhoId)
        {
            var gatinho = await _context.Gatinhos.FindAsync(gatinhoId);

            if (gatinho == null)
            {
                return NotFound();
            }

            var viewModel = new AdocaoViewModel
            {
                GatinhoId = gatinho.Id,
                GatinhoNome = gatinho.Nome,
                GatinhoImagem = gatinho.Imagem,
                GatinhoSexo = gatinho.Sexo,
                GatinhoAdotado = gatinho.Adotado
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdocaoViewModel model)
        {
            Console.WriteLine("AdocaoController.Create");
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is not valid");
                return View(model);
            }

            // Verifica se o gatinho existe
            var gatinho = await _context.Gatinhos.FindAsync(model.GatinhoId);
            if (gatinho == null)
            {
                ModelState.AddModelError(string.Empty, "Gatinho não encontrado.");
                return View(model);
            }

            // Verifica se o gatinho já foi adotado
            if (gatinho.Adotado)
            {
                ModelState.AddModelError(string.Empty, "Este gatinho já foi adotado.");
                return View(model);
            }

            // Usa uma transação para evitar inconsistências no banco de dados
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Verifica se o adotante já existe
                    var adotanteExistente = await _context.Adotantes
                        .FirstOrDefaultAsync(a => a.Email == model.Email || a.Telefone == model.Telefone);

                    Adotante adotante;

                    if (adotanteExistente != null)
                    {
                        // Se o adotante já existir, aproveita o registro existente
                        adotante = adotanteExistente;
                    }
                    else
                    {
                        // Caso contrário, cria um novo adotante
                        adotante = new Adotante
                        {
                            Nome = model.Nome,
                            Email = model.Email,
                            Telefone = model.Telefone
                        };

                        _context.Adotantes.Add(adotante);
                        await _context.SaveChangesAsync();
                    }

                    // Registra a adoção
                    var adocao = new Adocao
                    {
                        GatinhoId = model.GatinhoId,
                        AdotanteId = adotante.Id,
                        Data = DateTime.UtcNow
                    };

                    _context.Adocoes.Add(adocao);

                    // Atualiza o status do gatinho para "adotado"
                    gatinho.Adotado = true;
                    _context.Gatinhos.Update(gatinho);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    Console.WriteLine("Adoção realizada com sucesso");
                    return RedirectToAction("Index", "Gatinhos");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    ModelState.AddModelError(string.Empty, $"Erro ao processar adoção: {ex.Message}");
                    return View(model);
                }
            }


        }
        public IActionResult GatinhosAdotados()
        {
            var gatinhosAdotados = _context.Adocoes
                .Where(a => a.Gatinho.Adotado == true) // Filtra apenas os gatinhos adotados
                .Select(a => new GatinhoAdotadoViewModel
                {
                    NomeGatinho = a.Gatinho.Nome,
                    ImagemGatinho = a.Gatinho.Imagem,
                    SexoGatinho = a.Gatinho.Sexo,
                    NomeAdotante = a.Adotante.Nome,
                    EmailAdotante = a.Adotante.Email,
                    TelefoneAdotante = a.Adotante.Telefone,
                    DataAdocao = a.Data
                }).ToList();

            return View(gatinhosAdotados);
        }
    }
}
