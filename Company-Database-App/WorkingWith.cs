
namespace Company_Database_App
{
    public class WorkingWith
    {
        public int EmployeeID { get; set; }

        public int ClientID { get; set; }

        public int TotalSales { get; set; }


        public WorkingWith() 
        {
        
        }

        public WorkingWith(int employeeID, int clientID, int totalSales)
        {
            EmployeeID = employeeID;
            ClientID = clientID;
            TotalSales = totalSales;
        }
    }
}
