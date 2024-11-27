using Microsoft.EntityFrameworkCore;
using myMeow2.Data;
using myMeow2.Models;

namespace myMeow2.Logical
{
    public class Relatorios
    {
        private readonly ApplicationDbContext _context;

        public Relatorios(ApplicationDbContext context)
        {
            _context = context;
        }

        private LinkedList<Gatinho> Gatinhos { get; set; }
        private LinkedList<Adotante> Adotantes { get; set; }
        private LinkedList<Adotante> AdotantesQueAdotaramMaisDeUmGato { get; set; }

        //Puxa todos os gatinhos do banco de dados e armazena na Lista Gatinhos
        public void adicionarGatinhosAdotados()
        {
            Gatinhos = new LinkedList<Gatinho>();
            var adocoes = _context.Adocoes.Include(a => a.Gatinho).Include(a => a.Adotante).ToList();
            foreach (var adocao in adocoes)
            {
                Gatinhos.AddLast(adocao.Gatinho);
            }
        }

        //Puxa todos os adotantes do banco de dados e armazena na Lista Adotantes
        public void adicionarAdotantes()
        {
            Adotantes = new LinkedList<Adotante>();
            var adotantes = _context.Adotantes.ToList();
            foreach (var adotante in adotantes)
            {
                Adotantes.AddLast(adotante);
            }
        }

        //Puxa todos os adotantes que adotaram mais de um gato e armazena na Lista AdotantesQueAdotaramMaisDeUmGato
        public void adicionarAdotantesQueAdotaramMaisDeUmGato()
        {
            AdotantesQueAdotaramMaisDeUmGato = new LinkedList<Adotante>();
            var adocoes = _context.Adocoes.Include(a => a.Gatinho).Include(a => a.Adotante).ToList();
            var adotantes = _context.Adotantes.ToList();
            foreach (var adotante in adotantes)
            {
                int cont = 0;
                foreach (var adocao in adocoes)
                {
                    if (adocao.Adotante == adotante)
                    {
                        cont++;
                    }
                }
                if (cont > 1)
                {
                    AdotantesQueAdotaramMaisDeUmGato.AddLast(adotante);
                }
            }
        }   

        public LinkedList<Gatinho> getGatinhosAdotados()
        {
            return Gatinhos;
        }

        public LinkedList<Adotante> getAdotantes()
        {
            return Adotantes;
        }

        public LinkedList<Adotante> getAdotantesQueAdotaramMaisDeUmGato()
        {
            return AdotantesQueAdotaramMaisDeUmGato;
        }

    }
}
