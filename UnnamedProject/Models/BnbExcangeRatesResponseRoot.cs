using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace UnnamedProject
{
    [Serializable()]
    [XmlRoot(ElementName = "ROWSET")]
    public class BnbExcangeRatesResponseRoot
    {
        [XmlElement("ROW")]
        public List<BnbExcangeRatesResponse> ExcangeRates { get; set; }
    }

    [Serializable()]
    [XmlRoot(ElementName = "ROW")]
    public class BnbExcangeRatesResponse
    {
        [XmlElement("GOLD")]
        public string Indicator { get; set; }

        [XmlElement("CURR_DATE")]
        public string Date { get; set; }

        [XmlElement("NAME_")]
        public string Name { get; set; }

        [XmlElement("CODE")]
        public string Code { get; set; }

        [XmlElement("RATIO")]
        public string Ratio { get; set; }

        [XmlElement("REVERSERATE")]
        public string ReverseRate { get; set; }

        [XmlElement("RATE")]
        public string Rate { get; set; }
    }
}

