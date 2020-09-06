using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NegociacaoAPI.EF;
using NegociacaoAPI.EF.EntityConfig;
using NegociacaoAPI.Models;
using NegociacaoAPI.ViewModels;

namespace NegociacaoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NegociacaoApiController : ControllerBase
    {
        private NegociacaoContext _context;
        public NegociacaoApiController(IServiceProvider serviceProvider)
        {
            _context = new NegociacaoContext(serviceProvider.GetRequiredService<DbContextOptions<NegociacaoContext>>());
        }

        [HttpGet]
        [Route("ListarCPFs")]
        public IActionResult ListarCPFs()
        {
            List<CPFsListadosViewModel> cpfsListadosVM;
            try 
            {
                cpfsListadosVM = new List<CPFsListadosViewModel>();

                var cpfs = _context.Set<Tomador>().Select(x => x.CPF).ToList();
                foreach(var c in cpfs)
                {
                    cpfsListadosVM.Add(new CPFsListadosViewModel() { cpf = c });
                }
            }
            catch (Exception)
            {

                return NotFound();
            }

            return Ok(cpfsListadosVM);
        }

        [HttpGet]
        [Route("ConsultarDivida")]
        public IActionResult ConsultarDivida(string cpf)
        {
            ConsultarDividaViewModel consultarDividaVM;
            try
            {
                var tomador = _context.Set<Tomador>().Where(x => x.CPF == cpf).FirstOrDefault();
                var divida = tomador.Dividas.FirstOrDefault();

                consultarDividaVM = new ConsultarDividaViewModel(cpf, divida.Valor, divida.DataAtualizacao);
            }
            catch (Exception)
            {

                return NotFound();
            }

            return Ok(consultarDividaVM);
        }

        [HttpPost]
        [Route("SimularNegociacao")]
        public IActionResult SimularNegociacao(SimularNegociacaoViewModel cpfParcelasVM)
        {
            NegociacaoSimuladaViewModel simularNegociacaoVM;
            try
            {
                /*
                 * Manipulação dos dados com Entities
                 */
                var tomador = _context.Set<Tomador>().Where(x => x.CPF == cpfParcelasVM.cpf).FirstOrDefault();
                var divida = tomador.Dividas.OrderBy(x => x.DividaId).LastOrDefault();
                //Para simplificação, apenas a primeira parcela foi calculada e registrada
                var parcela = new Parcela()
                {
                    NumeroParcela = 0,
                    VencimentoParcela = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)), //Último dia do mês (mês em que isto é executado)
                    ValorParcela = Decimal.Round(divida.Valor / cpfParcelasVM.qtdParcelas, 2)
                };
                var simulacao = new Simulacao() { Parcelas = new List<Parcela>() { parcela } };

                tomador.Dividas.FirstOrDefault().Simulacoes.Add(simulacao);
                _context.Set<Tomador>().Update(tomador);
                _context.SaveChanges();


                /*
                 * Montagem da ViewModel para retorn de dados
                 */
                var simulacaoId = tomador.Dividas.OrderBy(x => x.DividaId).LastOrDefault().Simulacoes.OrderBy(x => x.SimulacaoId).LastOrDefault().SimulacaoId;
                var parcelamentoVM = new ParcelamentoViewModel(parcela.NumeroParcela, parcela.VencimentoParcela, parcela.ValorParcela);
                simularNegociacaoVM = new NegociacaoSimuladaViewModel(cpfParcelasVM.cpf, simulacaoId, parcelamentoVM);
            }
            catch (Exception)
            {
                return NotFound();
            }
            return Ok(simularNegociacaoVM);
        }


        [HttpPost]
        [Route("ConfirmarNegociacao")]
        public IActionResult ConfirmarNegociacao(ConfirmarSimulacaoViewModel cpfSimulacaoVM)
        {
            NegociacaoConfirmadaViewModel negociacaoConfirmadaVM;
            try
            {
                //var simulacao = _context.Set<Simulacao>().AsNoTracking().Where(x => x.SimulacaoId == cpfSimulacaoVM.simulacaoId).FirstOrDefault();

                var tomador = _context.Set<Tomador>().AsNoTracking();

                var simulacao = tomador.SelectMany(a => a.Dividas)
                                .SelectMany(b => b.Simulacoes)
                                .Where(c => c.SimulacaoId == cpfSimulacaoVM.simulacaoId).FirstOrDefault();

                simulacao.Acordos.Add(new Acordo() { Ativo = true });
                _context.Set<Simulacao>().Update(simulacao);
                _context.SaveChanges();

                var acordoId = simulacao.Acordos.OrderBy(x => x.AcordoId).LastOrDefault().AcordoId;
                negociacaoConfirmadaVM = new NegociacaoConfirmadaViewModel(acordoId, "OK");
            }
            catch(Exception ex)
            {
                return NotFound();

            }
            return Ok(negociacaoConfirmadaVM);
        }

        [HttpPost]
        [Route("CancelarNegociacao")]
        public IActionResult CancelarNegociacao(CancelarNegociacaoViewModel cpfAcordoVM)
        {
            NegociacaoCanceladaViewModel negociacaoCanceladaVM;
            try
            {
                var tomador = _context.Set<Tomador>().AsNoTracking();

                var acordo = tomador.SelectMany(a => a.Dividas)
                                    .SelectMany(b => b.Simulacoes)
                                    .SelectMany(c => c.Acordos)
                                    .Where(c => c.AcordoId == cpfAcordoVM.acordoId).FirstOrDefault();
                acordo.Ativo = false;

                _context.Set<Acordo>().Update(acordo);
                _context.SaveChanges();

                negociacaoCanceladaVM = new NegociacaoCanceladaViewModel("OK");
            }
            catch
            {
                return NotFound();

            }
            return Ok(negociacaoCanceladaVM);
        }

    }


}

