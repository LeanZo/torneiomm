using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using matamata.Models;
using matamata.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace matamata.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (matamataContext.Context.Torneio.Any())
            { 
                ViewData["Torneio"] = matamataContext.Context.Torneio;
            }
            return View();
        }

        public IActionResult IndexTableClick(int Id)
        {
            State.Torneio = matamataContext.Context.Torneio.ToList().Where(x => x.Id == Id).Single();

            return RedirectToAction("Torneio");
        }

        public IActionResult CriarClick(string nomeNovoTorneio)
        {
            State.torneioASerCriado = new Models.Torneio() { Nome = nomeNovoTorneio, Rodada = 1 };
            matamataContext.Context.Torneio.Add(State.torneioASerCriado);

            return RedirectToAction("CriandoTorneio", State.torneioASerCriado);
        }

        public IActionResult CriandoTorneio(Torneio torneio)
        {
            ViewBag.TorneioNome = torneio.Nome;

            return View();
        }

        public IActionResult CriarTorneio(string t1, string t2, string t3, string t4, string t5, string t6, string t7, string t8, string t9,
                                          string t10, string t11, string t12, string t13, string t14,string t15, string t16)
        {
            matamataContext.Context.SaveChanges();

            int id = matamataContext.Context.Torneio.ToList().Last().Id;

            matamataContext.Context.Time.AddRange(
                new Time() { Nome = t1, TorneioId = id },
                new Time() { Nome = t2, TorneioId = id },
                new Time() { Nome = t3, TorneioId = id },
                new Time() { Nome = t4, TorneioId = id },
                new Time() { Nome = t5, TorneioId = id },
                new Time() { Nome = t6, TorneioId = id },
                new Time() { Nome = t7, TorneioId = id },
                new Time() { Nome = t8, TorneioId = id },
                new Time() { Nome = t9, TorneioId = id },
                new Time() { Nome = t10, TorneioId = id },
                new Time() { Nome = t11, TorneioId = id },
                new Time() { Nome = t12, TorneioId = id },
                new Time() { Nome = t13, TorneioId = id },
                new Time() { Nome = t14, TorneioId = id },
                new Time() { Nome = t15, TorneioId = id },
                new Time() { Nome = t16, TorneioId = id }
                );

            matamataContext.Context.SaveChanges();

            List<Time> Times = matamataContext.Context.Time.ToList().Where(x => x.TorneioId == id).ToList();

            matamataContext.Context.Partida.AddRange(
                new Partida() { 
                    Time1Nome = t1,
                    Time2Nome = t2,
                    TorneioId = id,
                    Rodada = 1,
                    Slot = 1,
                    },
                new Partida()
                {
                    Time1Nome = t3,
                    Time2Nome = t4,
                    TorneioId = id,
                    Rodada = 1,
                    Slot = 2,
                },
                new Partida()
                {
                    Time1Nome = t5,
                    Time2Nome = t6,
                    TorneioId = id,
                    Rodada = 1,
                    Slot = 3,
                },
                new Partida()
                {
                    Time1Nome = t7,
                    Time2Nome = t8,
                    TorneioId = id,
                    Rodada = 1,
                    Slot = 4,
                },
                new Partida()
                {
                    Time1Nome = t9,
                    Time2Nome = t10,
                    TorneioId = id,
                    Rodada = 1,
                    Slot = 5,
                },
                new Partida()
                {
                    Time1Nome = t11,
                    Time2Nome = t12,
                    TorneioId = id,
                    Rodada = 1,
                    Slot = 6,
                },
                new Partida()
                {
                    Time1Nome = t13,
                    Time2Nome = t14,
                    TorneioId = id,
                    Rodada = 1,
                    Slot = 7,
                },
                new Partida()
                {
                    Time1Nome = t15,
                    Time2Nome = t16,
                    TorneioId = id,
                    Rodada = 1,
                    Slot = 8,
                }
                );

            matamataContext.Context.SaveChanges();

            State.Torneio = State.torneioASerCriado;

            return RedirectToAction("Torneio");
        }

        public IActionResult Torneio()
        {
            List<Partida> partidas = matamataContext.Context.Partida.ToList().Where(x => x.TorneioId == State.Torneio.Id
                                                                                   && x.Rodada == 1).OrderBy(x => x.Slot).ToList();
            for(int i = 1, j = 0; i <= 16; i += 2)
            {
                ViewData["1"+i.ToString()] = partidas[j].Time1Nome;
                ViewData["1"+(i+1).ToString()] = partidas[j].Time2Nome;

                if(State.Torneio.Rodada > 1)
                {
                    ViewData["1g" + i.ToString()] = partidas[j].Gols1;
                    ViewData["1g" + (i + 1).ToString()] = partidas[j].Gols2;
                }

                j++;
            }

            if (State.Torneio.Rodada > 1)
            {
                partidas = matamataContext.Context.Partida.ToList().Where(x => x.TorneioId == State.Torneio.Id
                                                                                       && x.Rodada == 2).OrderBy(x => x.Slot).ToList();
                for (int i = 1, j = 0; i <= 8; i += 2)
                {
                    ViewData["2" + i.ToString()] = partidas[j].Time1Nome;
                    ViewData["2" + (i + 1).ToString()] = partidas[j].Time2Nome;

                    if (State.Torneio.Rodada > 2)
                    {
                        ViewData["2g" + i.ToString()] = partidas[j].Gols1;
                        ViewData["2g" + (i + 1).ToString()] = partidas[j].Gols2;
                    }

                    j++;
                }
            }

            if (State.Torneio.Rodada > 2)
            {
                partidas = matamataContext.Context.Partida.ToList().Where(x => x.TorneioId == State.Torneio.Id
                                                                                   && x.Rodada == 3).OrderBy(x => x.Slot).ToList();
                for (int i = 1, j = 0; i <= 4; i += 2)
                {
                    ViewData["3" + i.ToString()] = partidas[j].Time1Nome;
                    ViewData["3" + (i + 1).ToString()] = partidas[j].Time2Nome;

                    if (State.Torneio.Rodada > 3)
                    {
                        ViewData["3g" + i.ToString()] = partidas[j].Gols1;
                        ViewData["3g" + (i + 1).ToString()] = partidas[j].Gols2;
                    }

                    j++;
                }
            }

            if (State.Torneio.Rodada > 3)
            {
                partidas = matamataContext.Context.Partida.ToList().Where(x => x.TorneioId == State.Torneio.Id
                                                                                   && x.Rodada == 4).OrderBy(x => x.Slot).ToList();
                for (int i = 1, j = 0; i <= 2; i += 2)
                {
                    ViewData["4" + i.ToString()] = partidas[j].Time1Nome;
                    ViewData["4" + (i + 1).ToString()] = partidas[j].Time2Nome;

                    if (State.Torneio.Rodada > 4)
                    {
                        ViewData["4g" + i.ToString()] = partidas[j].Gols1;
                        ViewData["4g" + (i + 1).ToString()] = partidas[j].Gols2;
                    }

                    j++;
                }
            }

            ViewBag.Rodada = State.Torneio.Rodada;
            ViewBag.Vencedor = State.Torneio.Rodada == 5 ? State.Torneio.VencedorNome : "";

            return View();
        }

        public IActionResult ConcluirRodada1(string t1g, string t2g, string t3g, string t4g, string t5g, string t6g, string t7g, string t8g, string t9g,
                                          string t10g, string t11g, string t12g, string t13g, string t14g, string t15g, string t16g)
        {
            List<Partida> partidas = matamataContext.Context.Partida.ToList().Where(x => x.TorneioId == State.Torneio.Id
                                                                                   && x.Rodada == 1).OrderBy(x => x.Slot).ToList();

            partidas[0].Gols1 = Convert.ToInt32(t1g);
            partidas[0].Gols2 = Convert.ToInt32(t2g);
            partidas[0].VencedorNome = partidas[0].Gols1 > partidas[0].Gols2 ? partidas[0].Time1Nome : partidas[0].Time2Nome;
            partidas[1].Gols1 = Convert.ToInt32(t3g);
            partidas[1].Gols2 = Convert.ToInt32(t4g);
            partidas[1].VencedorNome = partidas[1].Gols1 > partidas[1].Gols2 ? partidas[1].Time1Nome : partidas[1].Time2Nome;
            partidas[2].Gols1 = Convert.ToInt32(t5g);
            partidas[2].Gols2 = Convert.ToInt32(t6g);
            partidas[2].VencedorNome = partidas[2].Gols1 > partidas[2].Gols2 ? partidas[2].Time1Nome : partidas[2].Time2Nome;
            partidas[3].Gols1 = Convert.ToInt32(t7g);
            partidas[3].Gols2 = Convert.ToInt32(t8g);
            partidas[3].VencedorNome = partidas[3].Gols1 > partidas[3].Gols2 ? partidas[3].Time1Nome : partidas[3].Time2Nome;
            partidas[4].Gols1 = Convert.ToInt32(t9g);
            partidas[4].Gols2 = Convert.ToInt32(t10g);
            partidas[4].VencedorNome = partidas[4].Gols1 > partidas[4].Gols2 ? partidas[3].Time1Nome : partidas[4].Time2Nome;
            partidas[5].Gols1 = Convert.ToInt32(t11g);
            partidas[5].Gols2 = Convert.ToInt32(t12g);
            partidas[5].VencedorNome = partidas[5].Gols1 > partidas[5].Gols2 ? partidas[5].Time1Nome : partidas[5].Time2Nome;
            partidas[6].Gols1 = Convert.ToInt32(t13g);
            partidas[6].Gols2 = Convert.ToInt32(t14g);
            partidas[6].VencedorNome = partidas[6].Gols1 > partidas[6].Gols2 ? partidas[6].Time1Nome : partidas[6].Time2Nome;
            partidas[7].Gols1 = Convert.ToInt32(t15g);
            partidas[7].Gols2 = Convert.ToInt32(t16g);
            partidas[7].VencedorNome = partidas[7].Gols1 > partidas[7].Gols2 ? partidas[7].Time1Nome : partidas[7].Time2Nome;

            matamataContext.Context.Partida.UpdateRange(partidas[0], partidas[1], partidas[2], partidas[3], partidas[4], partidas[5], partidas[6], partidas[7]);
            matamataContext.Context.SaveChanges();

            matamataContext.Context.Partida.AddRange(
                new Partida()
                {
                    Time1Nome = partidas[0].VencedorNome,
                    Time2Nome = partidas[1].VencedorNome,
                    TorneioId = State.Torneio.Id,
                    Rodada = 2,
                    Slot = 1,
                },
                new Partida()
                {
                    Time1Nome = partidas[2].VencedorNome,
                    Time2Nome = partidas[3].VencedorNome,
                    TorneioId = State.Torneio.Id,
                    Rodada = 2,
                    Slot = 2,
                },
                new Partida()
                {
                    Time1Nome = partidas[4].VencedorNome,
                    Time2Nome = partidas[5].VencedorNome,
                    TorneioId = State.Torneio.Id,
                    Rodada = 2,
                    Slot = 3,
                },
                new Partida()
                {
                    Time1Nome = partidas[6].VencedorNome,
                    Time2Nome = partidas[7].VencedorNome,
                    TorneioId = State.Torneio.Id,
                    Rodada = 2,
                    Slot = 4,
                }
                );

            matamataContext.Context.SaveChanges();

            State.Torneio.Rodada = 2;

            matamataContext.Context.SaveChanges();

            return RedirectToAction("Torneio");
        }

        public IActionResult ConcluirRodada2(string t1g, string t2g, string t3g, string t4g, string t5g, string t6g, string t7g, string t8g)
        {
            List<Partida> partidas = matamataContext.Context.Partida.ToList().Where(x => x.TorneioId == State.Torneio.Id
                                                                                   && x.Rodada == 2).OrderBy(x => x.Slot).ToList();

            partidas[0].Gols1 = Convert.ToInt32(t1g);
            partidas[0].Gols2 = Convert.ToInt32(t2g);
            partidas[0].VencedorNome = partidas[0].Gols1 > partidas[0].Gols2 ? partidas[0].Time1Nome : partidas[0].Time2Nome;
            partidas[1].Gols1 = Convert.ToInt32(t3g);
            partidas[1].Gols2 = Convert.ToInt32(t4g);
            partidas[1].VencedorNome = partidas[1].Gols1 > partidas[1].Gols2 ? partidas[1].Time1Nome : partidas[1].Time2Nome;
            partidas[2].Gols1 = Convert.ToInt32(t5g);
            partidas[2].Gols2 = Convert.ToInt32(t6g);
            partidas[2].VencedorNome = partidas[2].Gols1 > partidas[2].Gols2 ? partidas[2].Time1Nome : partidas[2].Time2Nome;
            partidas[3].Gols1 = Convert.ToInt32(t7g);
            partidas[3].Gols2 = Convert.ToInt32(t8g);
            partidas[3].VencedorNome = partidas[3].Gols1 > partidas[3].Gols2 ? partidas[3].Time1Nome : partidas[3].Time2Nome;

            matamataContext.Context.Partida.UpdateRange(partidas[0], partidas[1], partidas[2], partidas[3]);
            matamataContext.Context.SaveChanges();

            matamataContext.Context.Partida.AddRange(
                new Partida()
                {
                    Time1Nome = partidas[0].VencedorNome,
                    Time2Nome = partidas[1].VencedorNome,
                    TorneioId = State.Torneio.Id,
                    Rodada = 3,
                    Slot = 1,
                },
                new Partida()
                {
                    Time1Nome = partidas[2].VencedorNome,
                    Time2Nome = partidas[3].VencedorNome,
                    TorneioId = State.Torneio.Id,
                    Rodada = 3,
                    Slot = 2,
                }
                );

            matamataContext.Context.SaveChanges();

            State.Torneio.Rodada = 3;

            matamataContext.Context.SaveChanges();

            return RedirectToAction("Torneio");
        }

        public IActionResult ConcluirRodada3(string t1g, string t2g, string t3g, string t4g)
        {
            List<Partida> partidas = matamataContext.Context.Partida.ToList().Where(x => x.TorneioId == State.Torneio.Id
                                                                                   && x.Rodada == 3).OrderBy(x => x.Slot).ToList();

            partidas[0].Gols1 = Convert.ToInt32(t1g);
            partidas[0].Gols2 = Convert.ToInt32(t2g);
            partidas[0].VencedorNome = partidas[0].Gols1 > partidas[0].Gols2 ? partidas[0].Time1Nome : partidas[0].Time2Nome;
            partidas[1].Gols1 = Convert.ToInt32(t3g);
            partidas[1].Gols2 = Convert.ToInt32(t4g);
            partidas[1].VencedorNome = partidas[1].Gols1 > partidas[1].Gols2 ? partidas[1].Time1Nome : partidas[1].Time2Nome;

            matamataContext.Context.Partida.UpdateRange(partidas[0], partidas[1]);
            matamataContext.Context.SaveChanges();

            matamataContext.Context.Partida.AddRange(
                new Partida()
                {
                    Time1Nome = partidas[0].VencedorNome,
                    Time2Nome = partidas[1].VencedorNome,
                    TorneioId = State.Torneio.Id,
                    Rodada = 4,
                    Slot = 1,
                }
                );

            matamataContext.Context.SaveChanges();

            State.Torneio.Rodada = 4;

            matamataContext.Context.SaveChanges();

            return RedirectToAction("Torneio");
        }

        public IActionResult ConcluirRodada4(string t1g, string t2g)
        {
            List<Partida> partidas = matamataContext.Context.Partida.ToList().Where(x => x.TorneioId == State.Torneio.Id
                                                                                   && x.Rodada == 4).OrderBy(x => x.Slot).ToList();

            partidas[0].Gols1 = Convert.ToInt32(t1g);
            partidas[0].Gols2 = Convert.ToInt32(t2g);
            partidas[0].VencedorNome = partidas[0].Gols1 > partidas[0].Gols2 ? partidas[0].Time1Nome : partidas[0].Time2Nome;

            matamataContext.Context.Partida.UpdateRange(partidas[0]);
            matamataContext.Context.SaveChanges();

            State.Torneio.VencedorNome = partidas[0].VencedorNome;
            State.Torneio.Rodada = 5;

            matamataContext.Context.Torneio.Update(State.Torneio);

            matamataContext.Context.SaveChanges();

            return RedirectToAction("Torneio");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
