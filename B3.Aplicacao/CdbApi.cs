using AutoMapper;
using B3.Aplicacao.Interfaces;
using B3.Aplicacao.Modelos;
using B3.Dominio.Entidades;
using B3.Dominio.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3.Aplicacao
{
    /// <summary>
    /// Api de Serviços CDB
    /// </summary>
    public class CdbApi : ICdbApi
    {
        #region Injeção de dependência
        private readonly IServicoCdb servicoCdb;
        private readonly IMapper mapper;
        #endregion

        #region Construtor
        public CdbApi(IServicoCdb servicoCdb, IMapper mapper)
        {
            this.servicoCdb = servicoCdb;
            this.mapper = mapper;
        }
        #endregion

        #region API
        /// <summary>
        /// Cálculo do investimento CDB
        /// </summary>
        /// <param name="valorInicial">Valor inicial da aplicação</param>
        /// <param name="prazoMeses">Prazo em meses da aplicação</param>
        /// <returns>Rentabilidade do investimento</returns>
        public RentabilidadeCdbDto CalcularInvestimento(decimal valorInicial, int prazoMeses)
        {
            if (valorInicial <= 0 || prazoMeses <= 0)
            {
                return new RentabilidadeCdbDto
                {
                    ResultadoBruto = 0,
                    ResultadoLiquido = 0
                };
            }

            var rentabilidade = servicoCdb.CalcularInvestimento(valorInicial, prazoMeses);
            var retornoDto = mapper.Map<RentabilidadeCdb, RentabilidadeCdbDto>(rentabilidade);

            return retornoDto;
        }
        #endregion
    }
}
