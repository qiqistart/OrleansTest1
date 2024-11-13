namespace Weixsu.Orleans.Transactions.AdoNet
{
    public enum TableTransactionActionType
    {
        Add,
        Update,
        Delete,
    }

    public class TableTransactionAction
    {
        public TableTransactionAction(TableTransactionActionType action, StateEntity state)
        {
            ActionType = action;
            State = state;
        }

        public TableTransactionAction(TableTransactionActionType action, KeyEntity key)
        {
            ActionType = action;
            Key = key;
        }


        public TableTransactionActionType ActionType { get; }
        public StateEntity State { get; }
        public KeyEntity Key { get; }

    }
}
