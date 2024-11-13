using Orleans;
using Orleans.Concurrency;
using OrleansGrain.Model;
using SqlSugar;

namespace OrleansGrain.GrainService
{
    /// <summary>
    /// 
    /// </summary>
    [Reentrant]
    public class AtmGrain : Grain, IAtmGrain
    {
        private readonly IGrainFactory _grainFactory;

        private readonly ISqlSugarClient sqlSugarClient;
        public AtmGrain(IGrainFactory _grainFactory, ISqlSugarClient sqlSugarClient)
        {
            this._grainFactory = _grainFactory;
            this.sqlSugarClient = sqlSugarClient;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromAccountId"></param>
        /// <param name="toAccountId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public async Task TransferAccounts(int fromAccountId, int toAccountId, int amount)
        {
           
            await _grainFactory.GetGrain<IUserAccountGrain>(toAccountId).AddMoney(toAccountId, amount);
            await _grainFactory.GetGrain<IUserAccountGrain>(fromAccountId).DeductMoney(fromAccountId, amount);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task Test()
        {

            Console.WriteLine($"键:{this.GetPrimaryKeyString()}");
            Console.WriteLine($"开始时间：{DateTime.Now}");
            Thread.Sleep(10000);
            Console.WriteLine($"结束：{DateTime.Now}");
            //Console.WriteLine("执行了");
        }
    }
}
