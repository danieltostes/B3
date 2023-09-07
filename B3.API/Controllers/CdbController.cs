using B3.Aplicacao.Interfaces;
using B3.Aplicacao.Modelos;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace B3.API.Controllers
{
    [Route("api/[controller]")]
    public class CdbController : Controller
    {
        #region Injeção de dependência
        private readonly ICdbApi cdbApi;
        #endregion

        #region Construtor
        public CdbController(ICdbApi cdbApi)
        {
            this.cdbApi = cdbApi;
        }
        #endregion

        #region Serviços
        [HttpGet]
        public ActionResult CalcularRendimento(CalculoRendimentoDto dto)
        {
            #region Validações
            if (dto == null)
                return BadRequest("Os parâmetros de valor inicial e número de meses devem ser informados");

            var listaErros = new StringBuilder();

            if (dto.ValorInicial <= 0)
                listaErros.AppendFormat("Valor inicial deve ser maior que zero.{0}", Environment.NewLine);

            if (dto.NumeroMeses <= 1)
                listaErros.AppendFormat("Número de meses deve ser maior que um.{0}", Environment.NewLine);

            if (listaErros.Length > 0)
                return BadRequest(listaErros.ToString());
            #endregion

            var rentabilidade = cdbApi.CalcularInvestimento(dto.ValorInicial, dto.NumeroMeses);

            return Ok(rentabilidade);
        }
        #endregion
    }
}
