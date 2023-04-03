using System.Collections.Generic;
using System.Threading.Tasks;
using OrdersAPI.ResultsModel;

namespace OrdersAPI.Services.Interfaces
{
    public interface IProviderService
    {
        Task<List<ProviderResult>> GetProviders();
    }
}