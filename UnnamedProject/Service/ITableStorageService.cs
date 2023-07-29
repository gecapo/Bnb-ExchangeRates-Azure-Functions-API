using System;
using System.Threading.Tasks;

namespace UnnamedProject
{
    public interface ITableStorageService
    {
        Task<BnbExcangeRates> GetEntityAsync(DateTime date, string code);
        Task<BnbExcangeRates> UpsertEntityAsync(BnbExcangeRates entity);
        Task DeleteEntityAsync(DateTime date, string code);
    }
}