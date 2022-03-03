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

    [Serializable]
    public class PPriceGroup
    {
        public Int32 PriceGroupID { get; set; }
        public string PriceGroupCode { get; set; }
        public string Description { get; set; }
    }

    [Serializable]
    public class PIncoterms
    {
        public Int32 IncoTermID { get; set; }
        public string IncoTerm { get; set; }
        public string Description { get; set; }
    }
    [Serializable]
    public class PPaymentTerms
    {
        public Int32 PaymentTermID { get; set; }
        public string PaymentTerm { get; set; }
        public string Description { get; set; }
    }
}
