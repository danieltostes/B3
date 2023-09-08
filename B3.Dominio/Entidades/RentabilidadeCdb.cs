namespace B3.Dominio.Entidades
{
    /// <summary>
    /// Rentabilidade do investimento CDB
    /// </summary>
    public class RentabilidadeCdb
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
