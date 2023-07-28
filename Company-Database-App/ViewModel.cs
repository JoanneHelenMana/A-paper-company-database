using Company_Database_App;
using System.Collections.Generic;


namespace A_Company_Database_App
{
    internal class ViewModel
    {
        public IList<Employee> Employees { get; set; }

        public IList<Employee> EmployeeTotalSales { get; set; }

        public IList<Branch> Branches { get; set; }

        public IList<Client> Clients { get; set; }

        public IList<WorkingWith> WorkingWith { get; set; }

        public IList<BranchSupplier> BranchSupplier { get; set; }
    }
}
