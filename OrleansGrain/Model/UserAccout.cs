using SqlSugar;

namespace OrleansGrain.Model
{
    public class UserAccout
    {

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "UserId", IsPrimaryKey = true)]
        public int UserId { get; set; }
        /// <summary>
        /// 余额
        /// </summary>
        public int Balance { get; set; }
    }
}
