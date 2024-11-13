using Microsoft.AspNetCore.Mvc;
using Orleans;
using Orleans.Hosting;
using OrleansGrain.GrainService;

namespace OrleansTestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtmController : ControllerBase
    {

         private readonly IGrainFactory _grainFactory;

        public AtmController(ISiloHost silo)
        {
            _grainFactory = silo.Services.GetService<IGrainFactory>();
        }

        [HttpPost("TransferAccounts")]
        public async Task TransferAccountsAsync()
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 100);
            var  data= _grainFactory.GetGrain<IAtmGrain>(randomNumber.ToString()).TransferAccounts(1,2,100);
            Console.WriteLine("查完了");
        }
    }
}
