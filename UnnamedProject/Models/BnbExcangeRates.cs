using System;

namespace UnnamedProject
{
    public class BnbExcangeRates
    {
        public DateTime ReportDate { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public int Ratio { get; set; }
        public decimal ReverseRate { get; set; }
        public decimal Rate { get; set; }
    }
}

