using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Xml.Linq;
using AutoMapper;

namespace UnnamedProject
{
    public class RatesGetByCode
    {
        private readonly IBnbClientService _bnbClient;
        private readonly IMapper _mapper;

        public RatesGetByCode(IBnbClientService bnbClient, IMapper mapper)
        {
            _bnbClient = bnbClient;
            _mapper = mapper;
        }

        [FunctionName("RatesGetByCode")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "rates/{date}/{code:alpha:maxlength(3):minlength(3)}")] HttpRequest req, DateTime date, string code)
        {
            var response = await _bnbClient.GetExchangeRates(date);
            if (!XElementExtensions.TryParse(response, out XElement xml))
                return new OkObjectResult($"No {code} rate for {date}");

            var bnbExcangeRatesRespon = XElementExtensions.Deserialize<BnbExcangeRatesResponseRoot>(xml.ToString());
            var rate = bnbExcangeRatesRespon.ExcangeRates
                .Where(x => x.Indicator == "1")
                .Where(x => x.Rate != "n/a")
                .FirstOrDefault(x => x.Code.ToLowerInvariant() == code.ToLowerInvariant());

            return new OkObjectResult(_mapper.Map<BnbExcangeRates>(rate));
        }
    }
}
