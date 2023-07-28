using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Globalization;
using System.Xml.Linq;
using Azure;
using System.Linq;
using AutoMapper;

namespace UnnamedProject
{
    public class RatesGetAll
    {
        private readonly IBnbClient _bnbClient;
        private readonly IMapper _mapper;

        public RatesGetAll(IBnbClient bnbClient, IMapper mapper)
        {
            _bnbClient = bnbClient;
            _mapper = mapper;
        }

        [FunctionName("RatesGetAll")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "rates/{date}")] HttpRequest req, DateTime date)
        {
            var response = await _bnbClient.GetExchangeRates(date);
            if (XElementExtensions.TryParse(response, out XElement xml))
            {
                var bnbExcangeRatesRespon = XElementExtensions.Deserialize<BnbExcangeRatesResponseRoot>(xml.ToString());
                var rates = bnbExcangeRatesRespon.ExcangeRates
                    .Where(x => x.Indicator == "1")
                    .Where(x => x.Rate != "n/a")
                    .Select(x => _mapper.Map<BnbExcangeRates>(x))
                    .ToList();

                return new OkObjectResult(rates);
            }

            return new OkObjectResult($"No rates for {date}");

        }
    }
}

