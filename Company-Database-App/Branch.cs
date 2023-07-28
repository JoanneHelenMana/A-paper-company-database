using System;


namespace Company_Database_App
{
    public class Branch
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int ManagerID { get; set; }

        public DateTime ManagerStartDate { get; set; }


        public Branch()
        {

        }

        public Branch (int id, string name, int managerID, DateTime managerStartDate)
        {
            ID = id;
            Name = name;
            ManagerID = managerID;
            ManagerStartDate = managerStartDate.Date;
        }
    }
}
