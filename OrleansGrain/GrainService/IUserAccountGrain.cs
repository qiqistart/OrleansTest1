using Orleans;

namespace OrleansGrain.GrainService
{
    public interface IUserAccountGrain: IGrainWithIntegerKey
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<OrleansGrain.Model.UserAccout> GetUserAccout(int UserId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="money"></param>
        /// <returns></returns>

        [Transaction(TransactionOption.Join)]
        Task DeductMoney(int UserId, int money);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        [Transaction(TransactionOption.Join)]
        Task AddMoney(int UserId, int money);
    }
}
