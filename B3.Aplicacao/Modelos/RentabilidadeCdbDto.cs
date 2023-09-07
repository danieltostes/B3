using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3.Aplicacao.Modelos
{
    /// <summary>
    /// Dto para a entidade de RentabilidadeCdb
    /// </summary>
    public class RentabilidadeCdbDto
    {
        /// <summary>
        /// Resultado bruto do investimento
        /// </summary>
        public decimal ResultadoBruto { get; set; }

        /// <summary>
        /// Resultado líquido do investimento
        /// </summary>
        public decimal ResultadoLiquido { get; set; }
    }
}
