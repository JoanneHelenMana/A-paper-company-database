using A_Company_Database_App;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;


namespace Company_Database_App
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class BranchSearch : Window
    {
        private string dbconnectionString = "datasource=localhost; port=3306; username=root; password='';";
        private MySqlConnection connection;
        private readonly ViewModel viewModel;

        public BranchSearch()
        {
            InitializeComponent();
            connection = new MySqlConnection(dbconnectionString);
            viewModel = new ViewModel
            {
                Branches = new ObservableCollection<Branch>(),
                Employees = new ObservableCollection<Employee>(),
            };
            this.DataContext = viewModel;
        }


        /// <summary>
        /// This function returns a collection of branches that match the name given by the user.
        /// The function returns all details of employees working at the given branch if the appropriate checkbox is checked.
        /// </summary>
        public List<Branch> SearchByName()
        {
            List<Branch> searchResults = new List<Branch>();

            string branchName = textBoxBranchName.Text;
            string searchByNameQuery = "SELECT * FROM jhm_ictprg402.branches WHERE branch_name = @BranchName;";

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(searchByNameQuery, connection);
                command.Parameters.AddWithValue("@BranchName", branchName);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Branch branch = new Branch();

                    branch.ID = Convert.ToInt32(reader["id"]);
                    branch.Name = Convert.ToString(reader["branch_name"]);
                    branch.ManagerID = Convert.ToInt32(reader["manager_id"]);
                    branch.ManagerStartDate = DateTime.Parse(Convert.ToString(reader["manager_started_at"]));
                                        
                    searchResults.Add(branch);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            connection.Close();
            return searchResults;
        }


        /// <summary>
        /// This function returns a collection of employees that work in the branch provided by the user.
        /// </summary>
        public List<Employee> ShowEmployeesInBranch()
        {
            List<Employee> searchResults = new List<Employee>();

            string branchName = textBoxBranchName.Text;
            string searchEmployeesInBranchQuery = "SELECT jhm_ictprg402.employees.id, jhm_ictprg402.employees.given_name, jhm_ictprg402.employees.family_name, " +
                "jhm_ictprg402.employees.date_of_birth, jhm_ictprg402.employees.gender_identity, jhm_ictprg402.employees.gross_salary, jhm_ictprg402.employees.supervisor_id, " +
                "jhm_ictprg402.employees.branch_id FROM jhm_ictprg402.employees INNER JOIN jhm_ictprg402.branches ON jhm_ictprg402.branches.id=employees.branch_id " +
                "WHERE branches.branch_name=@BranchName;";

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(searchEmployeesInBranchQuery, connection);
                command.Parameters.AddWithValue("@BranchName", branchName);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Employee employee = new Employee();

                    employee.ID = Convert.ToInt32(reader["ID"]);
                    employee.GivenName = Convert.ToString(reader["given_name"]);
                    employee.FamilyName = Convert.ToString(reader["family_name"]);
                    employee.DateOfBirth = DateTime.Parse(Convert.ToString(reader["date_of_birth"]));
                    employee.GenderIdentity = Convert.ToString(reader["gender_identity"]);
                    employee.GrossSalary = Convert.ToInt32(reader["gross_salary"]);
                    employee.SupervisorID = Convert.ToInt32(reader["supervisor_id"]);
                    employee.BranchID = Convert.ToInt32(reader["branch_id"]);

                    searchResults.Add(employee);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            connection.Close();
            return searchResults;
        }


        /// <summary>
        /// This functions returns a collection of branches that match the manager id provided by the user.
        /// </summary>
        public List<Branch> SearchByManagerID()
        {
            List<Branch> searchResults = new List<Branch>();

            string managerID = textBoxManagerID.Text;
            string searchByManagerIDQuery = "SELECT * FROM jhm_ictprg402.branches WHERE manager_id=@ManagerID;";

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(searchByManagerIDQuery, connection);
                command.Parameters.AddWithValue("@ManagerID", managerID);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Branch branch = new Branch();

                    branch.ID = Convert.ToInt32(reader["id"]);
                    branch.Name = Convert.ToString(reader["branch_name"]);
                    branch.ManagerID = Convert.ToInt32(reader["manager_id"]);
                    branch.ManagerStartDate = DateTime.Parse(Convert.ToString(reader["manager_started_at"]));

                    searchResults.Add(branch);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            connection.Close();
            return searchResults;
        }


        /// <summary>
        /// This function returns a collection of employess that manage the branch provided by the user.
        /// </summary>
        public List<Employee> ShowManagerDetails()
        {
            List<Employee> searchResults = new List<Employee>();

            string managerID = textBoxManagerID.Text;
            string searchEmployeesInBranchQuery = "SELECT jhm_ictprg402.employees.id, jhm_ictprg402.employees.given_name, jhm_ictprg402.employees.family_name, " +
                "jhm_ictprg402.employees.date_of_birth, jhm_ictprg402.employees.gender_identity, jhm_ictprg402.employees.gross_salary, jhm_ictprg402.employees.supervisor_id, " +
                "jhm_ictprg402.employees.branch_id FROM jhm_ictprg402.employees INNER JOIN jhm_ictprg402.branches ON jhm_ictprg402.branches.manager_id=employees.id " +
                "WHERE branches.manager_id=@ManagerID;";

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(searchEmployeesInBranchQuery, connection);
                command.Parameters.AddWithValue("@ManagerID", managerID);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Employee employee = new Employee();

                    employee.ID = Convert.ToInt32(reader["ID"]);
                    employee.GivenName = Convert.ToString(reader["given_name"]);
                    employee.FamilyName = Convert.ToString(reader["family_name"]);
                    employee.DateOfBirth = DateTime.Parse(Convert.ToString(reader["date_of_birth"]));
                    employee.GenderIdentity = Convert.ToString(reader["gender_identity"]);
                    employee.GrossSalary = Convert.ToInt32(reader["gross_salary"]);
                    employee.SupervisorID = Convert.ToInt32(reader["supervisor_id"]);
                    employee.BranchID = Convert.ToInt32(reader["branch_id"]);

                    searchResults.Add(employee);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            connection.Close();
            return searchResults;
        }


        /// <summary>
        /// This functions returns a collection of branches that match the manager start date provided by the user.
        /// </summary>
        public List<Branch> SearchByManagerStartDate()
        {
            List<Branch> searchResults = new List<Branch>();

            DateTime startDate = datePickerManagerStartDate.SelectedDate.Value;
            string startDateString = startDate.Date.ToString("yyyy-MM-dd");
            string searchByManagerStartDateQuery = "SELECT * FROM jhm_ictprg402.branches WHERE manager_started_at=@ManagerStartDate;";

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(searchByManagerStartDateQuery, connection);
                command.Parameters.AddWithValue("@ManagerStartDate", startDateString);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Branch branch = new Branch();

                    branch.ID = Convert.ToInt32(reader["id"]);
                    branch.Name = Convert.ToString(reader["branch_name"]);
                    branch.ManagerID = Convert.ToInt32(reader["manager_id"]);
                    branch.ManagerStartDate = DateTime.Parse(Convert.ToString(reader["manager_started_at"]));

                    searchResults.Add(branch);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            connection.Close();
            return searchResults;
        }


        /// <summary>
        /// This is triggered when the 'Search by Name' checkbox is checked. It unchecks the remaining search categories.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxSearchByName_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxSearchByManagerID.IsChecked = false;
            checkBoxSearchByManagerStartDate.IsChecked = false;
            checkBoxShowManagerDetails.IsChecked = false;

            textBoxBranchName.IsEnabled = true;
            checkBoxShowEmployees.IsEnabled = true;
            textBoxManagerID.IsEnabled = false;
            checkBoxShowManagerDetails.IsEnabled = false;           
            datePickerManagerStartDate.IsEnabled = false;
        }


        /// <summary>
        /// This is triggered when the 'Search by Manager ID' checkbox is checked. It unchecks the remaining search categories.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxSearchByManagerID_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxSearchByBranchName.IsChecked = false;
            checkBoxSearchByManagerStartDate.IsChecked= false;
            checkBoxShowEmployees.IsChecked = false;

            textBoxBranchName.IsEnabled= false;
            checkBoxShowEmployees.IsEnabled= false;
            textBoxManagerID.IsEnabled = true;
            checkBoxShowManagerDetails.IsEnabled = true;
            datePickerManagerStartDate.IsEnabled = false;
        }


        /// <summary>
        /// This is triggered when the 'Search by Manager start date' checkbox is checked. It unchecks the remaining search categories.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxSearchByManagerStartDate_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxSearchByBranchName.IsChecked = false;
            checkBoxSearchByManagerID.IsChecked = false;
            checkBoxShowEmployees.IsChecked = false;
            checkBoxShowManagerDetails.IsChecked = false;

            textBoxBranchName.IsEnabled = false;
            checkBoxShowEmployees.IsEnabled = false;
            textBoxManagerID.IsEnabled = false;
            checkBoxShowManagerDetails.IsEnabled = false;
            datePickerManagerStartDate.IsEnabled = true;
        }


        /// <summary>
        /// This is triggered when the 'Search' button is clicked. It sets 'DialogResult' to true and returns to MainWindow function buttonSearch_Click().
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }


        /// <summary>
        /// This is triggered when the 'Clear' button is clicked. It sets all fields back to default.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            checkBoxSearchByBranchName.IsChecked = false;
            textBoxBranchName.Clear();
            textBoxBranchName.IsEnabled = false;
            checkBoxShowEmployees.IsChecked = false;
            checkBoxShowEmployees.IsEnabled = false;

            checkBoxSearchByManagerID.IsChecked = false;
            textBoxManagerID.Clear();
            textBoxManagerID.IsEnabled = false;
            checkBoxShowManagerDetails.IsChecked = false;
            checkBoxShowManagerDetails.IsEnabled = false;

            checkBoxSearchByManagerStartDate.IsChecked = false;
            datePickerManagerStartDate.SelectedDate = null;
            datePickerManagerStartDate.IsEnabled = false;
        }
    }
}
