using System;
using System.Threading.Tasks;

namespace UnnamedProject
{
    public interface IBnbClientService
    {
        Task<string> GetExchangeRates(DateTime dateTime);
    }
}