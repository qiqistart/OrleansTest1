using Orleans;

namespace OrleansGrain.GrainService
{
    public interface IAtmGrain: IGrainWithStringKey
    {

        [Transaction(TransactionOption.Create)]
        Task TransferAccounts(int fromAccountId, int toAccountId, int amount);


        Task Test();
    }
}
