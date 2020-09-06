using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using NegociacaoAPI.ViewModels;
using Newtonsoft.Json;

namespace NegociacaoAPI.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        private string MSG_ERRO = "Erro no servidor. Favor contatar o administrador";

        /// <summary>
        /// Lista os CPFs de todos os tomadores cadastrados
        /// </summary>
        [Route("Index")]
        public IActionResult Index()
        {
            List<CPFsListadosViewModel> cpfsVM = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44398/api/negociacaoapi/");
                //HTTP GET
                var responseTask = client.GetAsync("ListarCPFs");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    cpfsVM = JsonConvert.DeserializeObject<List<CPFsListadosViewModel>>(readTask.Result);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, MSG_ERRO);
                }
            }
            return View(cpfsVM);

        }

        /// <summary>
        /// Consulta dívida do tomador.
        /// Tem em sua view um link a simular a negociação desta dívida.
        /// </summary>
        [Route("ConsultarDivida")]
        public IActionResult ConsultarDivida(string cpf)
        {
            ConsultarDividaViewModel consultarDividaVM = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44398/api/negociacaoapi/");
                //HTTP GET
                var responseTask = client.GetAsync("consultardivida?cpf=" + cpf);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    consultarDividaVM = JsonConvert.DeserializeObject<ConsultarDividaViewModel>(readTask.Result);
                }
                else
                {

                    ModelState.AddModelError(string.Empty, MSG_ERRO);
                }
            }
            return View(consultarDividaVM);

        }

        /// <summary>
        /// Versão GET do método.
        /// Exibe uma view que permite efetivar a simulação via submit (POST)
        /// </summary>
        [Route("SimularNegociacao")]
        public IActionResult SimularNegociacao(string cpf)
        {
            return View(new SimularNegociacaoViewModel() { cpf = cpf });
        }

        /// <summary>
        /// Versão POST do método.
        /// Efetiva a simulação e redireciona para a Action de confirmação.
        /// </summary>
        [HttpPost]
        [Route("SimularNegociacao")]
        public IActionResult SimularNegociacao(SimularNegociacaoViewModel simularNegociacaoVM)
        {
            NegociacaoSimuladaViewModel negociacaoSimuladaVM = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44398/api/negociacaoapi/");
                //HTTP GET
                var postTask = client.PostAsync("simularnegociacao", new StringContent(JsonConvert.SerializeObject(simularNegociacaoVM), Encoding.UTF8, "application/json")  );
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    negociacaoSimuladaVM = JsonConvert.DeserializeObject<NegociacaoSimuladaViewModel>(readTask.Result);

                    TempData["negociacaoSimuladaVM"] = JsonConvert.SerializeObject(negociacaoSimuladaVM);
                    return RedirectToAction("ConfirmarNegociacao");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, MSG_ERRO);
                    return View(simularNegociacaoVM);
                }
            }
        }

        /// <summary>
        /// Versão GET do método.
        /// Exibe uma view que permite efetivar a confirmação via submit (POST)
        /// </summary>
        [Route("ConfirmarNegociacao")]
        public IActionResult ConfirmarNegociacao()
        {
            NegociacaoSimuladaViewModel negociacaoSimuladaVM = null;
            try
            {
                negociacaoSimuladaVM = JsonConvert.DeserializeObject<NegociacaoSimuladaViewModel>((string)TempData["negociacaoSimuladaVM"]);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, MSG_ERRO);
            }
            return View(new ConfirmarSimulacaoViewModel(negociacaoSimuladaVM.cpf, negociacaoSimuladaVM.simulacaoId));
        }

        /// <summary>
        /// Versão POST do método.
        /// Efetiva a confirmação e redireciona para a Action de cancelamento da mesma.
        /// </summary>
        [HttpPost]
        [Route("ConfirmarNegociacao")]
        public IActionResult ConfirmarNegociacao(ConfirmarSimulacaoViewModel confirmarSimulacaoVM)
        {
            NegociacaoConfirmadaViewModel negociacaoConfirmadaVM = null;
            CancelarNegociacaoViewModel cancelarNegociacao = null;

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:44398/api/negociacaoapi/");
                //HTTP GET
                var postTask = client.PostAsync("ConfirmarNegociacao", new StringContent(JsonConvert.SerializeObject(confirmarSimulacaoVM), Encoding.UTF8, "application/json"));
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    negociacaoConfirmadaVM = JsonConvert.DeserializeObject<NegociacaoConfirmadaViewModel>(readTask.Result);

                    cancelarNegociacao = new CancelarNegociacaoViewModel(confirmarSimulacaoVM.cpf, negociacaoConfirmadaVM.acordoId);
                    TempData["cancelarNegociacao"] = JsonConvert.SerializeObject(cancelarNegociacao);
                    return RedirectToAction("CancelarNegociacao");
                }
                else
                {

                    ModelState.AddModelError(string.Empty, MSG_ERRO);
                }
            }
            return View(confirmarSimulacaoVM);
        }

        /// <summary>
        /// Versão GET do método.
        /// Exibe uma view que permite efetivar o cancelamento via submit (POST)
        /// </summary>
        [Route("CancelarNegociacao")]
        public IActionResult CancelarNegociacao()
        {
            CancelarNegociacaoViewModel cancelarNegociacao = null;
            try
            {
                cancelarNegociacao = JsonConvert.DeserializeObject<CancelarNegociacaoViewModel>((string)TempData["cancelarNegociacao"]);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, MSG_ERRO);
            }
            return View(cancelarNegociacao);
        }

        /// <summary>
        /// Versão POST do método.
        /// Efetiva o cancelamento e redireciona para a página inicial (Index).
        /// </summary>
        [HttpPost]
        [Route("CancelarNegociacao")]
        public IActionResult CancelarNegociacao(CancelarNegociacaoViewModel cancelarNegociacaoVM)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44398/api/negociacaoapi/");
                //HTTP GET
                var postTask = client.PostAsync("CancelarNegociacao", new StringContent(JsonConvert.SerializeObject(cancelarNegociacaoVM), Encoding.UTF8, "application/json"));
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    JsonConvert.DeserializeObject<NegociacaoCanceladaViewModel>(readTask.Result);
                    return RedirectToAction("Index");
                }
                else
                {

                    ModelState.AddModelError(string.Empty, MSG_ERRO);
                    return View(cancelarNegociacaoVM);
                }
            }
        }

    }
}
