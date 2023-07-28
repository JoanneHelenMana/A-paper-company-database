
namespace Company_Database_App
{
    public class Client
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int BranchID { get; set; }


        public Client()
        {

        }


        public Client(int id, string name, int branchID)
        {
            ID = id;
            Name = name;
            BranchID = branchID;
        }
    }
}
