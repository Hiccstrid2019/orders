using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrdersAPI.ResultsModel;
using OrdersAPI.Services.Interfaces;

namespace OrdersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProviderController: ControllerBase
    {
        private readonly IProviderService _providerService;

        public ProviderController(IProviderService providerService)
        {
            _providerService = providerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProviderResult>>> GetProviders()
        {
            var providers = await _providerService.GetProviders();
            return Ok(providers);
        }
    }
}