using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3.Aplicacao.Modelos
{
    /// <summary>
    /// Dto para o serviço de cálculo de rendimento cdb
    /// </summary>
    public class CalculoRendimentoDto
    {
        /// <summary>
        /// Valor inicial da aplicação
        /// </summary>
        public decimal ValorInicial { get; set; }

        /// <summary>
        /// Número de meses da aplicação
        /// </summary>
        public int NumeroMeses { get; set; }
    }
}
