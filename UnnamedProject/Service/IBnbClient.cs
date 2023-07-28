using System;
using System.Threading.Tasks;

namespace UnnamedProject
{
    public interface IBnbClient
    {
        Task<string> GetExchangeRates(DateTime dateTime);
    }
}

