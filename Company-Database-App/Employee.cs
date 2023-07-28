using System;


namespace A_Company_Database_App
{
    public class Employee
    {
        public int ID { get; set; }

        public string GivenName { get; set; }

        public string FamilyName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string GenderIdentity { get; set; }

        public int GrossSalary { get; set; }

        public int SupervisorID { get; set; }

        public int BranchID { get; set; }

        public int TotalSales { get; set; }


        public Employee() 
        { 

        }

        public Employee(int id, DateTime dob, string genderIdentity, int grossSalary, int supervisorID, int branchID, string givenName = null, string familyName = null)
        {
            ID = id;
            GivenName = givenName;
            FamilyName = familyName;
            DateOfBirth = dob.Date;
            GenderIdentity = genderIdentity;
            GrossSalary = grossSalary;
            SupervisorID = supervisorID;
            BranchID = branchID;
        }


        public Employee(int id, DateTime dob, string genderIdentity, int grossSalary, int supervisorID, int branchID, int totalSales, string givenName=null, string familyName=null)
        {
            ID = id;
            GivenName = givenName;
            FamilyName = familyName;
            DateOfBirth = dob.Date;
            GenderIdentity = genderIdentity;
            GrossSalary = grossSalary;
            SupervisorID = supervisorID;
            BranchID = branchID;
            TotalSales = totalSales;
        }
    }
}
