using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;

namespace UnnamedProject
{
    public class RatesGetAll
    {
        private readonly IBnbClientService _bnbClient;
        private readonly IMapper _mapper;
        private readonly ITableStorageService _tableStorageService;

        public RatesGetAll(IBnbClientService bnbClient,
            IMapper mapper,
            ITableStorageService tableStorageService)
        {
            _bnbClient = bnbClient;
            _mapper = mapper;
            _tableStorageService = tableStorageService;
        }

        [FunctionName("RatesGetAll")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "rates/{date}")] HttpRequest req, DateTime date)
        {
            var response = await _bnbClient.GetExchangeRates(date);
            if (!XElementExtensions.TryParse(response, out XElement xml))
                return new OkObjectResult($"No rates for {date}");

            var bnbExcangeRatesResponseDto = XElementExtensions.Deserialize<BnbExcangeRatesResponseRoot>(xml.ToString());
            var ratesDto = bnbExcangeRatesResponseDto.ExcangeRates
                .Where(x => x.Indicator == "1")
                .Where(x => x.Rate != "n/a")
                .ToList();

            var rates = _mapper.Map<IEnumerable<BnbExcangeRates>>(ratesDto);
            foreach (var rate in rates)
                await _tableStorageService.UpsertEntityAsync(rate);

            return new OkObjectResult(rates);
        }
    }
}

