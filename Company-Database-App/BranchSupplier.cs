
namespace Company_Database_App
{
    public class BranchSupplier
    {
        public int ID { get; set; }
        
        public int BranchID { get; set; }

        public string SupplierName { get; set; }

        public string ProductSupplied { get; set;}

        public string ManagerName { get; set; }


        public BranchSupplier()
        {

        }


        public BranchSupplier(int id, int branchID, string supplierName, string productSupplied, string managerName)
        {
            ID = id;
            BranchID = branchID;
            SupplierName = supplierName;
            ProductSupplied = productSupplied;
            ManagerName = managerName;
        }
    }
}
