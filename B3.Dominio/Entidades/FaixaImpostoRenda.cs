namespace B3.Dominio.Entidades
{
    /// <summary>
    /// Faixa do imposto de renda sobre o rendimento
    /// </summary>
    public class FaixaImpostoRenda
    {
        /// <summary>
        /// Prazo em meses da aplicação
        /// </summary>
        public int PrazoMeses { get; set; }

        /// <summary>
        /// Percentual de imposto cobrado sobre o rendimento
        /// </summary>
        public decimal PercentualImposto { get; set; }
    }
}
