using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{
    public class PMaster
    {
    }
    [Serializable]
    public class PBankName
    {
        public int BankNameID { get; set; }
        public string BankName { get; set; }
    }
    [Serializable]
    public class PEffortType
    {
        public int EffortTypeID { get; set; }
        public string EffortType { get; set; }
    }
    [Serializable]
    public class PExpenseType
    {
        public int ExpenseTypeID { get; set; }
        public string ExpenseType { get; set; }
    }
    public class PRelation
    {
        public int RelationID { get; set; }
        public string Relation { get; set; }
    }
    [Serializable]
    public class PMarketSegment
    {
        public int MarketSegmentID { get; set; }
        public string MarketSegment { get; set; }
    }
}
