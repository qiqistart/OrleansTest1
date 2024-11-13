using Orleans;
using Orleans.Runtime;
using Orleans.Transactions;
using Orleans.Transactions.Abstractions;
using OrleansGrain.Model;
using OrleansGrain.State;
using SqlSugar;

namespace OrleansGrain.GrainService
{

    public class UserAccountGrain : Grain, IUserAccountGrain
    {
        private readonly ISqlSugarClient sqlSugarClient;

        private readonly IPersistentState<UserAccoutState> persistentState;

        private readonly ITransactionalState<UserAccoutState> UserAccout;
        public UserAccountGrain(ISqlSugarClient sqlSugarClient, [PersistentState("UserAccout", "OrleansStorage")]
            IPersistentState<UserAccoutState> persistentState,
             [TransactionalState("UserAccout", "TransactionStore")]
        ITransactionalState<UserAccoutState> UserAccout)
        {
            this.sqlSugarClient = sqlSugarClient;
            this.persistentState = persistentState;
            this.UserAccout = UserAccout;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<UserAccout> GetUserAccout(int UserId)
        {
            var state = persistentState.State;
            if (state.userAccout == null)
            {
                var userAccout = await sqlSugarClient.Queryable<UserAccout>()
                   .Where(x => x.UserId == UserId)
                   .FirstAsync();

                persistentState.State.userAccout = userAccout;
                await this.persistentState.WriteStateAsync();
            }
            return state.userAccout;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<UserAccout> GetTransactionalUserAccout(int UserId)
        {
            var userdata = await UserAccout.PerformRead(state => state.userAccout);
            if (userdata == null)
            {
                userdata = await GetUserAccout(UserId);
            }
            var isok = await UserAccout.PerformUpdate(state => state.userAccout = userdata);
            return userdata;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public async Task DeductMoney(int UserId, int money)
        {

            var userdata = await GetTransactionalUserAccout(UserId);
            userdata.Balance = userdata.Balance - money;
            if (userdata.Balance < 0)
            {
                throw new InvalidOperationException("余额不足");
            }
            await this.UserAccout.PerformUpdate(state =>
            {
                state.userAccout = userdata;
                sqlSugarClient.Ado.UseTran(() =>
                {
                    sqlSugarClient.Updateable(userdata).ExecuteCommand();


                });
            });
            await this.persistentState.WriteStateAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public async Task AddMoney(int UserId, int money)
        {
            var userdata = await GetTransactionalUserAccout(UserId);
            userdata.Balance = userdata.Balance + money;

            await this.UserAccout.PerformUpdate(state =>
            {
                state.userAccout = userdata;
                sqlSugarClient.Ado.UseTran(() =>
                {
                    sqlSugarClient.Updateable(userdata).ExecuteCommand();


                });
            });
            await this.persistentState.WriteStateAsync();
        }

    }
}
