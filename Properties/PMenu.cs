using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    public class PMenu
    {
        public PMenuMaster Master { get; set; }
        public PMenuProcurement Procurement { get; set; }
        public PMenuSales Sales { get; set; }
        public PMenuService Service { get; set; }
        public PMenuStock Stock { get; set; }
        public Boolean DMS_AF_Account { get; set; }
        public PMenuMonthlySummary Category { get; set; } 
    }

    public class PMenuMaster
    {
        public Boolean Material { get; set; }
        public Boolean Customer { get; set; }
        public Boolean Equipment { get; set; }
        public Boolean BIN { get; set; }
        public Boolean Price { get; set; }
        public Boolean Supercede { get; set; }
        public Boolean ROQ_DOQ { get; set; }
    }
    public class PMenuProcurement
    {
        public Boolean POList { get; set; }
        public Boolean ASNList { get; set; }
        public Boolean GRList { get; set; }
        public Boolean POReturn { get; set; }
        public Boolean PendingSAPPO { get; set; }
        public Boolean PendingASN { get; set; }
        public Boolean PendingGR { get; set; }
        public Boolean POPerfomance { get; set; }
    }
    public class PMenuSales
    {

        public Boolean Quotation { get; set; }
        public Boolean SaleOrderHeader { get; set; }
        public Boolean SaleOrderItem { get; set; }
        public Boolean Delivery { get; set; }
        public Boolean Invoice { get; set; }
        public Boolean SalesReturn { get; set; }
        public Boolean PendingDelivery { get; set; }
        public Boolean PendingInvoice { get; set; }
        public Boolean SalesPerfomance { get; set; }
    }
    public class PMenuService
    {
        public Boolean MTTR { get; set; }
        public Boolean PaidService { get; set; }
        public Boolean WarrantyClaim { get; set; }
        public Boolean ApprovalStatus { get; set; }
        public Boolean FailedMaterialReturn { get; set; }
        public Boolean NEPI_CommInvoice { get; set; }
        public Boolean NEPI_CommCon { get; set; }
        public Boolean NEPI_CommConAnnexure { get; set; }
        public Boolean ServicePerfomance { get; set; }
    }
    public class PMenuStock
    {
        public Boolean ConvertedStock { get; set; }
        public Boolean StockOverview { get; set; }
        public Boolean StockSummary { get; set; } 
    }
    public class PMenuMonthlySummary
    {
        public Boolean Procurement { get; set; }
        public Boolean Sales { get; set; }
        public Boolean Service { get; set; }
        public Boolean Stock { get; set; } 
    }
}