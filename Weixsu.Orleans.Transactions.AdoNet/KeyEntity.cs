namespace Weixsu.Orleans.Transactions.AdoNet
{
    public class KeyEntity
    {
        public string StateId { get; set; }
        public string ETag { get; set; }
        public long CommittedSequenceId { get; set; }
        public string MetaDataJson { get; set; }
    }

}
