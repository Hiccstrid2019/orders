using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrdersAPI.Data;
using OrdersAPI.ResultsModel;
using OrdersAPI.Services.Interfaces;

namespace OrdersAPI.Services
{
    public class ProviderService : IProviderService
    {
        private readonly AppDbContext _context;

        public ProviderService(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<ProviderResult>> GetProviders()
        {
            var providers = await _context.Providers
                .Select(provider => new ProviderResult()
                {
                    Id = provider.Id,
                    Name = provider.Name
                }).ToListAsync();
            return providers;
        }
    }
}