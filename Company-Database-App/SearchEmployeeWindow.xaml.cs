using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;


namespace A_Company_Database_App
{
    /// <summary>
    /// Interaction logic for EmployeeSearch.xaml
    /// </summary>
    public partial class EmployeeSearch : Window
    {
        private string dbconnectionString = "datasource=localhost; port=3306; username=root; password='';";
        private MySqlConnection connection;
        private readonly ViewModel viewModel;

        public EmployeeSearch()
        {
            InitializeComponent();
            comboBoxBranchID.ItemsSource = new int[] {1, 2, 3};
            connection = new MySqlConnection(dbconnectionString);
            viewModel = new ViewModel
            {
                Employees = new ObservableCollection<Employee>(),
                EmployeeTotalSales = new ObservableCollection<Employee>(),
            };
            this.DataContext = viewModel;
        }


        /// <summary>
        /// This function returns a collection of employess that match the given name and/or family name input by the user.
        /// The function returns specific information on the employee if any of the checkboxes are checked.
        /// </summary>
        public List<Employee> SearchByName()
        {
            List<Employee> searchResults = new List<Employee>();
            string givenName = textBoxGivenName.Text;
            string familyName = textBoxFamilyName.Text;
            string searchByNameQuery = "SELECT * FROM jhm_ictprg402.employees WHERE given_name = @GivenName OR family_name = @FamilyName;";

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(searchByNameQuery, connection);
                command.Parameters.AddWithValue("@GivenName", givenName);
                command.Parameters.AddWithValue("@FamilyName", familyName);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Employee employee = new Employee();

                    employee.GivenName = Convert.ToString(reader["given_name"]);
                    employee.FamilyName = Convert.ToString(reader["family_name"]);                    

                    if (checkBoxDOB.IsChecked == true)
                        employee.DateOfBirth = DateTime.Parse(Convert.ToString(reader["date_of_birth"]));
                    if (checkBoxGenderIdentity.IsChecked == true)
                        employee.GenderIdentity = Convert.ToString(reader["gender_identity"]);
                    if (checkBoxGrossSalary.IsChecked == true)
                        employee.GrossSalary = Convert.ToInt32(reader["gross_salary"]);
                    if (checkBoxSupervisorID.IsChecked == true)
                        employee.SupervisorID = Convert.ToInt32(reader["supervisor_id"]);
                    if (checkBoxBranchID.IsChecked == true)
                        employee.BranchID = Convert.ToInt32(reader["branch_id"]);
                    if (checkBoxID.IsChecked == true)
                        employee.ID = Convert.ToInt32(reader["ID"]);

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
        /// This function returns a collection of employess and their total sales that match the given name and/or family name input by the user.
        /// </summary>
        public List<Employee> ShowTotalSales()
        {
            List<Employee> searchResults = new List<Employee>();
            string givenName = textBoxGivenName.Text;
            string familyName = textBoxFamilyName.Text;
            string searchTotalSalesQuery = "SELECT jhm_ictprg402.employees.given_name, jhm_ictprg402.employees.family_name, jhm_ictprg402.working_with.total_sales " +
                "FROM jhm_ictprg402.employees INNER JOIN jhm_ictprg402.working_with ON jhm_ictprg402.employees.id=working_with.employee_id " +
                "WHERE employees.given_name = @GivenName OR employees.family_name = @FamilyName;";
          
            if (checkBoxTotalSales.IsChecked == true)
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(searchTotalSalesQuery, connection);
                    command.Parameters.AddWithValue("@GivenName", givenName);
                    command.Parameters.AddWithValue("FamilyName", familyName);
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee employee = new Employee();

                        employee.GivenName = Convert.ToString(reader["given_name"]);
                        employee.FamilyName = Convert.ToString(reader["family_name"]);
                        employee.TotalSales = Convert.ToInt32(reader["total_sales"]);

                        searchResults.Add(employee);
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                connection.Close();
            }
            
            return searchResults;
        }

        
        /// <summary>
        /// This functions returns a collection of employees that match the gross salary range input by the user.
        /// </summary>
        public List<Employee> SearchByGrossSalary()
        {
            List<Employee> searchResults = new List<Employee>();
            string minimum;
            string maximum;            
            string searchByGrossSalaryQuery = "SELECT * FROM jhm_ictprg402.employees WHERE gross_salary >= @Minimum AND gross_salary <= @Maximum;";

            try
            {
                if (textBoxMinimum.Text == "") { minimum = "0"; }
                else minimum = textBoxMinimum.Text;

                if (textBoxMaximum.Text == "") { maximum = int.MaxValue.ToString(); }
                else maximum = textBoxMaximum.Text;

                connection.Open();
                MySqlCommand command = new MySqlCommand(searchByGrossSalaryQuery, connection);
                command.Parameters.AddWithValue("@Minimum", minimum);
                command.Parameters.AddWithValue("@Maximum", maximum);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Employee employee = new Employee();

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
        /// This functions returns a collection of employees that match the branch id selected by the user.
        /// </summary>
        public List<Employee> SearchByBranchID()
        {
            List<Employee> searchResults = new List<Employee>();
            string branchID = comboBoxBranchID.Text;
            string searchByBranchIDQuery = "SELECT * FROM jhm_ictprg402.employees WHERE branch_id=@BranchID;";

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(searchByBranchIDQuery, connection);
                command.Parameters.AddWithValue("@BranchID", branchID);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Employee employee = new Employee();

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
        /// This is triggered when the 'Search' button is clicked. It sets 'DialogResult' to true and returns to MainWindow function buttonSearch_Click().
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }


        /// <summary>
        /// This is triggered when the 'Search by Name' checkbox is checked. It unchecks the other two search categories.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxSearchByName_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxSearchByBranchID.IsChecked = false;
            checkBoxSearchByGrossSalary.IsChecked = false;
        }


        /// <summary>
        /// This is triggered when the 'Search by Gross Salary' checkbox is checked. It unchecks the other two search categories.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxSearchByGrossSalary_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxSearchByName.IsChecked = false;
            checkBoxSearchByBranchID.IsChecked = false;
            checkBoxDOB.IsChecked = false;
            checkBoxBranchID.IsChecked = false;
            checkBoxGenderIdentity.IsChecked = false;
            checkBoxGrossSalary.IsChecked = false;
            checkBoxID.IsChecked = false;
            checkBoxSupervisorID.IsChecked = false;
        }


        /// <summary>
        /// This is triggered when the 'Search by Branch ID' checkbox is checked. It unchecks the other two search categories.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxSearchByBranchID_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxSearchByName.IsChecked = false;
            checkBoxSearchByGrossSalary.IsChecked = false;
            checkBoxDOB.IsChecked = false;
            checkBoxBranchID.IsChecked = false;
            checkBoxGenderIdentity.IsChecked = false;
            checkBoxGrossSalary.IsChecked = false;
            checkBoxID.IsChecked = false;
            checkBoxSupervisorID.IsChecked = false;
        }


        /// <summary>
        /// This is triggered when the 'Clear' button is clicked. It sets all fields back to default.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            checkBoxSearchByName.IsChecked = false;
            checkBoxSearchByGrossSalary.IsChecked = false;
            checkBoxSearchByBranchID.IsChecked = false;

            textBoxGivenName.Clear();
            textBoxFamilyName.Clear();
            checkBoxDOB.IsChecked = false;
            checkBoxGenderIdentity.IsChecked = false;
            checkBoxGrossSalary.IsChecked = false;
            checkBoxSupervisorID.IsChecked = false;
            checkBoxBranchID.IsChecked = false;
            checkBoxID.IsChecked = false;

            textBoxMinimum.Clear();
            textBoxMaximum.Clear();

            comboBoxBranchID.SelectedItem = null;
        }
    }
}
