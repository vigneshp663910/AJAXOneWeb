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


    [Serializable]
    public class PRelation
    {
        public int RelationID { get; set; }
        public string Relation { get; set; }
    } 
    [Serializable]
    public class PMake
    {
        public int MakeID { get; set; }
        public string Make { get; set; }
    }
    [Serializable]
    public class PProductType
    {
        public int ProductTypeID { get; set; }
        public string ProductType { get; set; }
    }
    [Serializable]
    public class PProduct
    {
        public int ProductID { get; set; }
        public string Product { get; set; }
    }
    [Serializable]
    public class PImportance
    {
        public int ImportanceID { get; set; }
        public string Importance { get; set; }
    }
    [Serializable]
    public class PPreSaleStatus
    {
        public int StatusID { get; set; }
        public string Status { get; set; }
    }
}
