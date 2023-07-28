using System;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using Company_Database_App;


namespace A_Company_Database_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ViewModel viewModel;

        private string dbName = "jhm_ictprg402";
        private string dbUser = "root";
        private string dbPassword = "";
        private int dbPort = 3306;
        private string dbServer = "localhost";
        
        private string dbConnectionString = "";
        private MySqlConnection connection;


        public MainWindow()
        {
            InitializeComponent();
            comboBoxDB.ItemsSource = new string[] { "Branches", "Branch supplier", "Clients", "Employees", "Working with" };
            dbConnectionString = $"server={dbServer}; user={dbUser}; database={dbName}; port={dbPort}; password={dbPassword}";
            connection = new MySqlConnection(dbConnectionString);
            
            viewModel = new ViewModel
            {
                Employees = new ObservableCollection<Employee>(),
                EmployeeTotalSales = new ObservableCollection<Employee>(),
                Branches = new ObservableCollection<Branch>(),
                Clients = new ObservableCollection<Client>(),
                WorkingWith = new ObservableCollection<WorkingWith>(),
                BranchSupplier = new ObservableCollection<BranchSupplier>(),
            };
            
            this.DataContext = viewModel;

            try
            {
                connection.Open();
                MessageBox.Show("Database connection successful.");
                connection.Close();
            }

            catch (MySqlException ex)
            {
                MessageBox.Show("Database connection unsuccessful.");
            }
        }


        /// <summary>
        /// Sets the visibility property of all items on the main window in accordance with the database selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxDB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HideAllItems();

            if (comboBoxDB.SelectedItem.ToString() == "Employees")
            {
                buttonEmployeesShowAll.Visibility = Visibility.Visible;
                buttonEmployeesAdd.Visibility = Visibility.Visible;
                buttonEmployeesDelete.Visibility = Visibility.Visible;
                buttonEmployeesUpdate.Visibility = Visibility.Visible;
                buttonEmployeesSearch.Visibility = Visibility.Visible;
                buttonEmployeesClearAll.Visibility = Visibility.Visible;
                dataGridEmployees.Visibility = Visibility.Visible;
                dataGridEmployeesTotalSales.Visibility = Visibility.Hidden;
            }

            else if (comboBoxDB.SelectedItem.ToString() == "Branches")
            {
                buttonBranchesShowAll.Visibility = Visibility.Visible;
                buttonBranchesAdd.Visibility = Visibility.Visible;
                buttonBranchesDelete.Visibility = Visibility.Visible;
                buttonBranchesUpdate.Visibility = Visibility.Visible;
                buttonBranchesSearch.Visibility = Visibility.Visible;
                buttonBranchesClearAll.Visibility = Visibility.Visible;
                dataGridBranches.Visibility = Visibility.Visible;
                dataGridBranchesEmployees.Visibility = Visibility.Hidden;
            }

            else if (comboBoxDB.SelectedItem.ToString() == "Clients")
            {
                buttonClientsShowAll.Visibility = Visibility.Visible;
                buttonClientsAdd.Visibility = Visibility.Visible;
                buttonClientsDelete.Visibility = Visibility.Visible;
                buttonClientsUpdate.Visibility = Visibility.Visible;
                buttonClientsSearch.Visibility = Visibility.Visible;
                buttonClientsClearAll.Visibility = Visibility.Visible;
                dataGridClients.Visibility = Visibility.Visible;
            }

            else if (comboBoxDB.SelectedItem.ToString() == "Working with")
            {
                buttonWorkingWithShowAll.Visibility = Visibility.Visible;
                buttonWorkingWithDelete.Visibility = Visibility.Visible;
                buttonWorkingWithUpdate.Visibility = Visibility.Visible;
                buttonWorkingWithSearch.Visibility = Visibility.Visible;
                buttonWorkingWithClearAll.Visibility = Visibility.Visible;
                dataGridWorkingWith.Visibility = Visibility.Visible;
            }

            else if (comboBoxDB.SelectedItem.ToString() == "Branch supplier")
            {
                buttonBranchSupplierShowAll.Visibility = Visibility.Visible;
                buttonBranchSupplierAdd.Visibility = Visibility.Visible;
                buttonBranchSupplierDelete.Visibility = Visibility.Visible;
                buttonBranchSupplierUpdate.Visibility = Visibility.Visible;
                buttonBranchSupplierSearch.Visibility = Visibility.Visible;
                buttonBranchSupplierClearAll.Visibility = Visibility.Visible;
                dataGridBranchSupplier.Visibility = Visibility.Visible;
            }
        }


        /// <summary>
        /// This function hides all items on the main window, except for the database selector combobox.
        /// </summary>
        public void HideAllItems()
        {
            buttonEmployeesShowAll.Visibility = Visibility.Hidden;
            buttonEmployeesAdd.Visibility = Visibility.Hidden;
            buttonEmployeesDelete.Visibility = Visibility.Hidden;
            buttonEmployeesUpdate.Visibility = Visibility.Hidden;
            buttonEmployeesSearch.Visibility = Visibility.Hidden;
            buttonEmployeesClearAll.Visibility = Visibility.Hidden;
            dataGridEmployees.Visibility = Visibility.Hidden;
            dataGridEmployeesTotalSales.Visibility = Visibility.Hidden;

            buttonBranchesShowAll.Visibility = Visibility.Hidden;
            buttonBranchesAdd.Visibility = Visibility.Hidden;
            buttonBranchesDelete.Visibility = Visibility.Hidden;
            buttonBranchesUpdate.Visibility = Visibility.Hidden;
            buttonBranchesSearch.Visibility = Visibility.Hidden;
            buttonBranchesClearAll.Visibility = Visibility.Hidden;
            dataGridBranches.Visibility = Visibility.Hidden;
            dataGridBranchesEmployees.Visibility = Visibility.Hidden;

            buttonClientsShowAll.Visibility = Visibility.Hidden;
            buttonClientsAdd.Visibility = Visibility.Hidden;
            buttonClientsDelete.Visibility = Visibility.Hidden;
            buttonClientsUpdate.Visibility = Visibility.Hidden;
            buttonClientsSearch.Visibility = Visibility.Hidden;
            buttonClientsClearAll.Visibility = Visibility.Hidden;
            dataGridClients.Visibility = Visibility.Hidden;

            buttonWorkingWithShowAll.Visibility = Visibility.Hidden;
            buttonWorkingWithDelete.Visibility = Visibility.Hidden;
            buttonWorkingWithUpdate.Visibility = Visibility.Hidden;
            buttonWorkingWithSearch.Visibility = Visibility.Hidden;
            buttonWorkingWithClearAll.Visibility = Visibility.Hidden;
            dataGridWorkingWith.Visibility = Visibility.Hidden;

            buttonBranchSupplierShowAll.Visibility = Visibility.Hidden;
            buttonBranchSupplierAdd.Visibility = Visibility.Hidden;
            buttonBranchSupplierDelete.Visibility = Visibility.Hidden;
            buttonBranchSupplierUpdate.Visibility = Visibility.Hidden;
            buttonBranchSupplierSearch.Visibility = Visibility.Hidden;
            buttonBranchSupplierClearAll.Visibility = Visibility.Hidden;
            dataGridBranchSupplier.Visibility = Visibility.Hidden;
        }


        // EMPLOYEE starts

        /// <summary>
        /// This is triggered when the 'Show All' button is clicked. It retrieves all fields for all employees in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEmployeesShowAll_Click(object sender, RoutedEventArgs e)
        {
            string selectAllQuery = "SELECT * FROM employees;";
            viewModel.Employees.Clear();

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(selectAllQuery, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Employee employee = new Employee();

                    employee.ID = Convert.ToInt32(reader["id"]);
                    employee.GivenName = Convert.ToString(reader["given_name"]);
                    employee.FamilyName = Convert.ToString(reader["family_name"]);
                    employee.DateOfBirth = DateTime.Parse(Convert.ToString(reader["date_of_birth"]));
                    employee.GenderIdentity = Convert.ToString(reader["gender_identity"]);
                    employee.GrossSalary = Convert.ToInt32(reader["gross_salary"]);
                    employee.SupervisorID = Convert.ToInt32(reader["supervisor_id"]);
                    employee.BranchID = Convert.ToInt32(reader["branch_id"]);

                    viewModel.Employees.Add(employee);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            connection.Close();
        }


        /// <summary>
        /// This is triggered when the 'Delete' button is clicked. It deletes the selected employee from the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEmployeesDelete_Click(object sender, RoutedEventArgs e)
        {
            string deleteEmployeeQuery = $"DELETE FROM jhm_ictprg402.employees WHERE id = @ID;";
            
            Employee employee = new Employee();
            IList<DataGridCellInfo> cellsCollection;

            if (dataGridEmployees.SelectedCells != null)
            {
                cellsCollection = dataGridEmployees.SelectedCells;
                if (cellsCollection.Count != 0)
                    employee = cellsCollection[0].Item as Employee;
            }

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(deleteEmployeeQuery, connection);

                string id = employee.ID.ToString();
                command.Parameters.AddWithValue("id", id);

                if (command.ExecuteNonQuery() == 1)
                {
                    viewModel.Employees.Remove(employee);
                    MessageBox.Show("Employee deleted.", "", MessageBoxButton.OK);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            connection.Close();
        }


        /// <summary>
        /// This is triggered when the 'Update' button is clicked.
        /// It updates the existing employee's record in the database with the information introduced by the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEmployeesUpdate_Click(object sender, RoutedEventArgs e)
        {
            string updateSalaryQuery = $"UPDATE jhm_ictprg402.employees SET given_name = @GivenName, family_name = @FamilyName, " +
                $"date_of_birth = @DOB, gender_identity = @GenderIdentity, gross_salary = @GrossSalary, supervisor_id = @SupervisorID, " +
                $"branch_id = @BranchID WHERE id = @ID;";

            Employee employee = new Employee();
            IList<DataGridCellInfo> cellsCollection;

            if (dataGridEmployees.SelectedCells != null)
            {
                cellsCollection = dataGridEmployees.SelectedCells;
                if (cellsCollection.Count != 0)
                    employee = cellsCollection[0].Item as Employee;
            }           

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(updateSalaryQuery, connection);

                string id = employee.ID.ToString();
                string newGivenName = employee.GivenName;
                string newFamilyName = employee.FamilyName;
                string newDOB = employee.DateOfBirth.Date.ToString("yyyy-MM-dd");
                string newGenderIdentity = employee.GenderIdentity;
                string newSalary = employee.GrossSalary.ToString();
                string newSupervisorID = employee.SupervisorID.ToString();
                string newBranchID = employee.BranchID.ToString();

                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@GivenName", newGivenName);
                command.Parameters.AddWithValue("@FamilyName", newFamilyName);
                command.Parameters.AddWithValue("@DOB", newDOB);
                command.Parameters.AddWithValue("@GenderIdentity", newGenderIdentity);
                command.Parameters.AddWithValue("@GrossSalary", newSalary);
                command.Parameters.AddWithValue("@SupervisorID", newSupervisorID);
                command.Parameters.AddWithValue("@BranchID", newBranchID);

                if (command.ExecuteNonQuery() == 1)
                {
                    viewModel.Employees.Clear();
                    viewModel.Employees.Add(employee);
                    MessageBox.Show("Record updated.", "", MessageBoxButton.OK);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            connection.Close();
        }


        /// <summary>
        /// This function is triggered when the 'Add' button is clicked. 
        /// It adds the employee to the database. The employee's fields are retrieved from the user's input.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEmployeesAdd_Click(object sender, RoutedEventArgs e)
        {
            string addEmployeeQuery = $"INSERT INTO jhm_ictprg402.employees (given_name, family_name, date_of_birth, gender_identity, gross_salary, supervisor_id, branch_id) " +
                $"VALUES (@GivenName,@FamilyName,@DOB,@GenderIdentity,@GrossSalary,@SupervisorID,@BranchID);";

            Employee employee = new Employee();
            IList<DataGridCellInfo> cellsCollection;

            if (dataGridEmployees.SelectedCells != null)
            {
                cellsCollection = dataGridEmployees.SelectedCells;
                if (cellsCollection.Count != 0)
                    employee = cellsCollection[0].Item as Employee;
            }

            string givenName = employee.GivenName;
            string familyName = employee.FamilyName;
            string DOB = employee.DateOfBirth.Date.ToString("yyyy-MM-dd");
            string genderIdentity = employee.GenderIdentity;
            string grossSalary = employee.GrossSalary.ToString();
            string supervisorID = employee.SupervisorID.ToString();
            string branchID = employee.BranchID.ToString();

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(addEmployeeQuery, connection);

                command.Parameters.AddWithValue("@GivenName", givenName);
                command.Parameters.AddWithValue("@FamilyName", familyName);
                command.Parameters.AddWithValue("@DOB", DOB);
                command.Parameters.AddWithValue("@GenderIdentity", genderIdentity);
                command.Parameters.AddWithValue("@GrossSalary", grossSalary);
                command.Parameters.AddWithValue("@SupervisorID", supervisorID);
                command.Parameters.AddWithValue("@BranchID", branchID);

                if (command.ExecuteNonQuery() == 1)
                {
                    viewModel.Employees.Clear();
                    viewModel.Employees.Add(employee);
                    MessageBox.Show("Employee added.", "", MessageBoxButton.OK);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            connection.Close();
        }


        /// <summary>
        /// This function is triggered when the 'Search' button is clicked. It opens the Search Employee window, where the user
        /// enters the fields to search. If the dialog returns true ('Search'), the search is performed and results displayed.
        /// Otherwise, if the dialog returns false (search window closed), no action is performed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEmployeesSearch_Click(object sender, RoutedEventArgs e)
        {
            EmployeeSearch searchWindow = new EmployeeSearch();
            searchWindow.Owner = this;
            bool? result = searchWindow.ShowDialog();

            if (result == true)
            {
                viewModel.Employees.Clear();
                viewModel.EmployeeTotalSales.Clear();
                dataGridEmployeesTotalSales.Visibility = Visibility.Hidden;

                if (searchWindow.SearchByName().Count != 0)
                {
                    foreach (Employee employee in searchWindow.SearchByName())
                    {
                        viewModel.Employees.Add(employee);                       
                    }

                    if (searchWindow.ShowTotalSales().Count != 0)
                    {
                        dataGridEmployeesTotalSales.Visibility = Visibility.Visible;
                        foreach (Employee employee in searchWindow.ShowTotalSales())
                        {
                            viewModel.EmployeeTotalSales.Add(employee);
                        }
                    }
                }

                else if (searchWindow.SearchByGrossSalary().Count != 0)
                {
                    foreach (Employee employee in searchWindow.SearchByGrossSalary())
                    {
                        viewModel.Employees.Add(employee);
                    }
                }

                else if (searchWindow.SearchByBranchID().Count != 0)
                {
                    foreach (Employee employee in searchWindow.SearchByBranchID())
                    {
                        viewModel.Employees.Add(employee);
                    }
                }
            }

            else
            {
                MessageBox.Show("No results found.", "", MessageBoxButton.OK);
            }
        }


        /// <summary>
        /// This is triggered when the 'Clear All' button is clicked. It sets the database views back to default.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEmployeesClearAll_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Employees.Clear();
            viewModel.EmployeeTotalSales.Clear();
        }

        // EMPLOYEE ends


        // BRANCHES begins

        /// <summary>
        /// This is triggered when the 'Show All' button is clicked. It retrieves all fields for all branches in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBranchesShowAll_Click(object sender, RoutedEventArgs e)
        {
            string selectAllQuery = "SELECT * FROM branches;";
            viewModel.Branches.Clear();

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(selectAllQuery, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Branch branch = new Branch();

                    branch.ID = Convert.ToInt32(reader["id"]);
                    branch.Name = Convert.ToString(reader["branch_name"]);
                    branch.ManagerID = Convert.ToInt32(reader["manager_id"]);
                    branch.ManagerStartDate = DateTime.Parse(Convert.ToString(reader["manager_started_at"]));

                    viewModel.Branches.Add(branch);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            connection.Close();
        }


        /// <summary>
        /// This function is triggered when the 'Add' button is clicked. 
        /// It adds the branch to the database. The fields for the new branch are retrieved from the user's input.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBranchesAdd_Click(object sender, RoutedEventArgs e)
        {
            string addBranchQuery = $"INSERT INTO jhm_ictprg402.branches (branch_name, manager_id, manager_started_at) " +
                $"VALUES (@BranchName,@ManagerID,@ManagerStartDate);";

            Branch branch = new Branch();
            IList<DataGridCellInfo> cellsCollection;

            if (dataGridBranches.SelectedCells != null)
            {
                cellsCollection = dataGridBranches.SelectedCells;
                if (cellsCollection.Count != 0)
                    branch = cellsCollection[0].Item as Branch;
            }

            string branchName = branch.Name;
            string managerID = branch.ManagerID.ToString();
            string managerStartDate = branch.ManagerStartDate.Date.ToString("yyyy-MM-dd");

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(addBranchQuery, connection);

                command.Parameters.AddWithValue("@BranchName", branchName);
                command.Parameters.AddWithValue("@ManagerID", managerID);
                command.Parameters.AddWithValue("@ManagerStartDate", managerStartDate);

                if (command.ExecuteNonQuery() == 1)
                {
                    viewModel.Branches.Clear();
                    viewModel.Branches.Add(branch);
                    MessageBox.Show("Branch added.", "", MessageBoxButton.OK);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            connection.Close();
        }


        /// <summary>
        /// This is triggered when the 'Delete' button is clicked. It deletes the selected branch from the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBranchesDelete_Click(object sender, RoutedEventArgs e)
        {
            string deleteBranchQuery = $"DELETE FROM jhm_ictprg402.branches WHERE id = @ID;";

            Branch branch = new Branch();
            IList<DataGridCellInfo> cellsCollection;

            if (dataGridBranches.SelectedCells != null)
            {
                cellsCollection = dataGridBranches.SelectedCells;
                if (cellsCollection.Count != 0)
                    branch = cellsCollection[0].Item as Branch;
            }

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(deleteBranchQuery, connection);

                string id = branch.ID.ToString();
                command.Parameters.AddWithValue("@ID", id);

                if (command.ExecuteNonQuery() == 1)
                {
                    viewModel.Branches.Remove(branch);
                    MessageBox.Show("Branch deleted.", "", MessageBoxButton.OK);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            connection.Close();
        }


        /// <summary>
        /// This is triggered when the 'Update' button is clicked. It updates the existing branch record in the database with the user's input.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBranchesUpdate_Click(object sender, RoutedEventArgs e)
        {
            string updateRecordQuery = $"UPDATE jhm_ictprg402.branches SET branch_name = @BranchName, manager_id = @ManagerID, manager_started_at = @ManagerStartDate WHERE id = @ID;";

            Branch branch = new Branch();
            IList<DataGridCellInfo> cellsCollection;

            if (dataGridBranches.SelectedCells != null)
            {
                cellsCollection = dataGridBranches.SelectedCells;
                if (cellsCollection.Count != 0)
                    branch = cellsCollection[0].Item as Branch;
            }

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(updateRecordQuery, connection);

                string id = branch.ID.ToString();
                string newBranchName = branch.Name;
                string newManagerID = branch.ManagerID.ToString();
                string newManagerStartDate = branch.ManagerStartDate.Date.ToString("yyyy-MM-dd");

                command.Parameters.AddWithValue("ID", id);
                command.Parameters.AddWithValue("@BranchName", newBranchName);
                command.Parameters.AddWithValue("@ManagerID", newManagerID);
                command.Parameters.AddWithValue("ManagerStartDate", newManagerStartDate);

                if (command.ExecuteNonQuery() == 1)
                {
                    viewModel.Branches.Clear();
                    viewModel.Branches.Add(branch);
                    MessageBox.Show("Record updated.", "", MessageBoxButton.OK);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            connection.Close();
        }


        /// <summary>
        /// This function is triggered when the 'Search' button is clicked. It opens the Search Branch window, where the user
        /// enters the fields to search. If the dialog returns true ('Search'), the search is performed and results displayed.
        /// Otherwise, if the dialog returns false (search window closed), no action is performed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBranchesSearch_Click(object sender, RoutedEventArgs e)
        {
            BranchSearch searchWindow = new BranchSearch();
            searchWindow.Owner = this;
            bool? result = searchWindow.ShowDialog();
            
            if (result == true)
            {
                viewModel.Branches.Clear();
                viewModel.Employees.Clear();
                dataGridBranchesEmployees.Visibility = Visibility.Hidden;

                if (searchWindow.SearchByName().Count != 0)
                {
                    foreach (Branch branch in searchWindow.SearchByName())
                    {
                        viewModel.Branches.Add(branch);
                    }

                    if (searchWindow.ShowEmployeesInBranch().Count != 0)
                    {
                        dataGridBranchesEmployees.Visibility = Visibility.Visible;
                        foreach (Employee employee in searchWindow.ShowEmployeesInBranch())
                        {
                            viewModel.Employees.Add(employee);
                        }
                    }
                }

                else if (searchWindow.SearchByManagerID().Count != 0)
                {
                    foreach (Branch branch in searchWindow.SearchByManagerID())
                    {
                        viewModel.Branches.Add(branch);
                    }

                    if (searchWindow.ShowManagerDetails().Count != 0)
                    {
                        dataGridBranchesEmployees.Visibility = Visibility.Visible;
                        foreach (Employee employee in searchWindow.ShowManagerDetails())
                        {
                            viewModel.Employees.Add(employee);
                        }
                    }
                }

                else if (searchWindow.SearchByManagerStartDate().Count != 0)
                {
                    foreach (Branch branch in searchWindow.SearchByManagerStartDate())
                    {
                        viewModel.Branches.Add(branch);
                    }
                }
            }

            else
            {
                MessageBox.Show("No results found.", "", MessageBoxButton.OK);
            }
        }


        /// <summary>
        /// This is triggered when the 'Clear All' button is clicked. It sets the database views back to default.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBranchesClearAll_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Branches.Clear();
            viewModel.Employees.Clear();
        }

        // BRANCHES ends


        // CLIENTS begins

        /// <summary>
        /// This is triggered when the 'Show All' button is clicked. It retrieves all fields for all clients in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClientsShowAll_Click(object sender, RoutedEventArgs e)
        {
            string selectAllQuery = "SELECT * FROM clients;";
            viewModel.Clients.Clear();

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(selectAllQuery, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Client client = new Client();

                    client.ID = Convert.ToInt32(reader["id"]);
                    client.Name = Convert.ToString(reader["client_name"]);
                    client.BranchID = Convert.ToInt32(reader["branch_id"]);

                    viewModel.Clients.Add(client);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            connection.Close();
        }


        /// <summary>
        /// This function is triggered when the 'Add' button is clicked. 
        /// It adds the client to the database. The fields for the new client are retrieved from the user's input.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClientsAdd_Click(object sender, RoutedEventArgs e)
        {
            string addClientQuery = $"INSERT INTO jhm_ictprg402.clients (client_name, branch_id) VALUES (@ClientName,@BranchID);";

            Client client = new Client();
            IList<DataGridCellInfo> cellsCollection;

            if (dataGridClients.SelectedCells != null)
            {
                cellsCollection = dataGridClients.SelectedCells;
                if (cellsCollection.Count != 0)
                    client = cellsCollection[0].Item as Client;
            }

            string clientName = client.Name;
            string branchID = client.BranchID.ToString();

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(addClientQuery, connection);

                command.Parameters.AddWithValue("@ClientName", clientName);
                command.Parameters.AddWithValue("@BranchID", branchID);

                if (command.ExecuteNonQuery() == 1)
                {
                    viewModel.Clients.Clear();
                    viewModel.Clients.Add(client);
                    MessageBox.Show("Client added.", "", MessageBoxButton.OK);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            connection.Close();
        }


        /// <summary>
        /// This is triggered when the 'Delete' button is clicked. It deletes the selected client from the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClientsDelete_Click(object sender, RoutedEventArgs e)
        {
            string deleteClientQuery = $"DELETE FROM jhm_ictprg402.clients WHERE id = @ID;";

            Client client = new Client();
            IList<DataGridCellInfo> cellsCollection;

            if (dataGridClients.SelectedCells != null)
            {
                cellsCollection = dataGridClients.SelectedCells;
                if (cellsCollection.Count != 0)
                    client = cellsCollection[0].Item as Client;
            }

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(deleteClientQuery, connection);

                string id = client.ID.ToString();
                command.Parameters.AddWithValue("@ID", id);

                if (command.ExecuteNonQuery() == 1)
                {
                    viewModel.Clients.Remove(client);
                    MessageBox.Show("Client deleted.", "", MessageBoxButton.OK);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            connection.Close();
        }


        /// <summary>
        /// This is triggered when the 'Update' button is clicked. It updates the existing client record in the database with the user's input.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClientsUpdate_Click(object sender, RoutedEventArgs e)
        {
            string updateRecordQuery = $"UPDATE jhm_ictprg402.clients SET client_name = @ClientName, branch_id = @BranchID WHERE id = @ID;";

            Client client = new Client();
            IList<DataGridCellInfo> cellsCollection;

            if (dataGridClients.SelectedCells != null)
            {
                cellsCollection = dataGridClients.SelectedCells;
                if (cellsCollection.Count != 0)
                    client = cellsCollection[0].Item as Client;
            }

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(updateRecordQuery, connection);

                string id = client.ID.ToString();
                string newClientName = client.Name;
                string newBranchID = client.BranchID.ToString();

                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@ClientName", newClientName);
                command.Parameters.AddWithValue("@BranchID", newBranchID);

                if (command.ExecuteNonQuery() == 1)
                {
                    viewModel.Clients.Clear();
                    viewModel.Clients.Add(client);
                    MessageBox.Show("Record updated.", "", MessageBoxButton.OK);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            connection.Close();
        }


        /// <summary>
        /// This function is triggered when the 'Search' button is clicked. It opens the Search Client window, where the user
        /// enters the fields to search. If the dialog returns true ('Search'), the search is performed and results displayed.
        /// Otherwise, if the dialog returns false (search window closed), no action is performed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClientsSearch_Click(object sender, RoutedEventArgs e)
        {
            ClientSearch searchWindow = new ClientSearch();
            searchWindow.Owner = this;
            bool? result = searchWindow.ShowDialog();

            if (result == true)
            {
                viewModel.Clients.Clear();

                if (searchWindow.SearchByName().Count != 0)
                {
                    foreach (Client client in searchWindow.SearchByName())
                    {
                        viewModel.Clients.Add(client);                   
                    }
                }

                else if (searchWindow.SearchByBranchID().Count != 0)
                {
                    foreach (Client client in searchWindow.SearchByBranchID())
                    {
                        viewModel.Clients.Add(client);
                    }
                }
            }

            else
            {
                MessageBox.Show("No results found.", "", MessageBoxButton.OK);
            }
        }


        /// <summary>
        /// This is triggered when the 'Clear All' button is clicked. It sets the database views back to default.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClientsClearAll_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Clients.Clear();
        }

        // CLIENTS ends


        // WORKING WITH begins

        /// <summary>
        /// This is triggered when the 'Show All' button is clicked. It retrieves all employees in the database working with each client and their total sales.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonWorkingWithShowAll_Click(object sender, RoutedEventArgs e)
        {
            string selectAllQuery = "SELECT * FROM working_with;";
            viewModel.WorkingWith.Clear();

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(selectAllQuery, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    WorkingWith workingWith = new WorkingWith();

                    workingWith.EmployeeID = Convert.ToInt32(reader["employee_id"]);
                    workingWith.ClientID = Convert.ToInt32(reader["client_id"]);
                    workingWith.TotalSales = Convert.ToInt32(reader["total_sales"]);

                    viewModel.WorkingWith.Add(workingWith);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            connection.Close();
        }


        /// <summary>
        /// This is triggered when the 'Delete' button is clicked. It deletes the selected record from the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonWorkingWithDelete_Click(object sender, RoutedEventArgs e)
        {
            string deleteRecordQuery = $"DELETE FROM jhm_ictprg402.working_with WHERE employee_id = @EmployeeID AND client_ID = @ClientID;";

            WorkingWith workingWith = new WorkingWith();
            IList<DataGridCellInfo> cellsCollection;

            if (dataGridWorkingWith.SelectedCells != null)
            {
                cellsCollection = dataGridWorkingWith.SelectedCells;
                if (cellsCollection.Count != 0)
                    workingWith = cellsCollection[0].Item as WorkingWith;
            }

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(deleteRecordQuery, connection);

                string employeeID = workingWith.EmployeeID.ToString();
                string clientID = workingWith.ClientID.ToString();

                command.Parameters.AddWithValue("@EmployeeID", employeeID);
                command.Parameters.AddWithValue("@ClientID", clientID);

                if (command.ExecuteNonQuery() == 1)
                {
                    viewModel.WorkingWith.Remove(workingWith);
                    MessageBox.Show("Record deleted.", "", MessageBoxButton.OK);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            connection.Close();
        }


        /// <summary>
        /// This is triggered when the 'Update' button is clicked. It updates the existing record in the database with the user's total sales input.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonWorkingWithUpdate_Click(object sender, RoutedEventArgs e)
        {
            string updateRecordQuery = $"UPDATE jhm_ictprg402.working_with SET total_sales = @TotalSales WHERE employee_id = @EmployeeID AND client_id = @ClientID;";

            WorkingWith workingWith = new WorkingWith();
            IList<DataGridCellInfo> cellsCollection;

            if (dataGridWorkingWith.SelectedCells != null)
            {
                cellsCollection = dataGridWorkingWith.SelectedCells;
                if (cellsCollection.Count != 0)
                    workingWith = cellsCollection[0].Item as WorkingWith;
            }

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(updateRecordQuery, connection);

                string employeeID = workingWith.EmployeeID.ToString();
                string clientID = workingWith.ClientID.ToString();
                string newTotalSales = workingWith.TotalSales.ToString();

                command.Parameters.AddWithValue("@EmployeeID", employeeID);
                command.Parameters.AddWithValue("@ClientID", clientID);
                command.Parameters.AddWithValue("@TotalSales", newTotalSales);

                if (command.ExecuteNonQuery() == 1)
                {
                    viewModel.WorkingWith.Clear();
                    viewModel.WorkingWith.Add(workingWith);
                    MessageBox.Show("Record updated.", "", MessageBoxButton.OK);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            connection.Close();
        }


        /// <summary>
        /// This function is triggered when the 'Search' button is clicked. It opens the Search Working With window, where the user
        /// enters the fields to search. If the dialog returns true ('Search'), the search is performed and results displayed.
        /// Otherwise, if the dialog returns false (search window closed), no action is performed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonWorkingWithSearch_Click(object sender, RoutedEventArgs e)
        {
            WorkingWithSearch searchWindow = new WorkingWithSearch();
            searchWindow.Owner = this;
            bool? result = searchWindow.ShowDialog();

            if (result == true)
            {
                viewModel.WorkingWith.Clear();

                if (searchWindow.SearchByEmployeeID().Count != 0)
                {
                    foreach (WorkingWith workingWith in searchWindow.SearchByEmployeeID())
                    {
                        viewModel.WorkingWith.Add(workingWith);
                    }
                }

                else if (searchWindow.SearchByClientID().Count != 0)
                {
                    foreach (WorkingWith workingWith in searchWindow.SearchByClientID())
                    {
                        viewModel.WorkingWith.Add(workingWith);
                    }
                }

                else if (searchWindow.SearchByTotalSales().Count != 0)
                {
                    foreach (WorkingWith workingWith in searchWindow.SearchByTotalSales())
                    {
                        viewModel.WorkingWith.Add(workingWith);
                    }
                }
            }

            else
            {
                MessageBox.Show("No results found.", "", MessageBoxButton.OK);
            }
        }


        /// <summary>
        /// This is triggered when the 'Clear All' button is clicked. It sets the database view back to default.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonWorkingWithClearAll_Click(object sender, RoutedEventArgs e)
        {
            viewModel.WorkingWith.Clear();
        }

        // WORKING WITH ends


        // BRANCH SUPPLIER begins

        /// <summary>
        /// This is triggered when the 'Show All' button is clicked. It retrieves all branch supplier records in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBranchSupplierShowAll_Click(object sender, RoutedEventArgs e)
        {
            string selectAllQuery = "SELECT * FROM branch_supplier;";
            viewModel.BranchSupplier.Clear();

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(selectAllQuery, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    BranchSupplier branchSupplier = new BranchSupplier();

                    branchSupplier.ID = Convert.ToInt32(reader["id"]);
                    branchSupplier.BranchID = Convert.ToInt32(reader["branch_id"]);
                    branchSupplier.SupplierName = Convert.ToString(reader["supplier_name"]);
                    branchSupplier.ProductSupplied = Convert.ToString(reader["product_supplied"]);
                    branchSupplier.ManagerName = Convert.ToString(reader["manager_name"]);

                    viewModel.BranchSupplier.Add(branchSupplier);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            connection.Close();
        }


        /// <summary>
        /// This is triggered when the 'Add' button is clicked. 
        /// It adds a branch supplier record to the database. The fields for the new record are retrieved from the user's input.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBranchSupplierAdd_Click(object sender, RoutedEventArgs e)
        {
            string addRecordQuery = $"INSERT INTO jhm_ictprg402.branch_supplier (branch_id, supplier_name, product_supplied, manager_name) " +
                $"VALUES (@BranchID,@SupplierName,@ProductSupplied,@ManagerName);";

            BranchSupplier branchSupplier = new BranchSupplier();
            IList<DataGridCellInfo> cellsCollection;

            if (dataGridBranchSupplier.SelectedCells != null)
            {
                cellsCollection = dataGridBranchSupplier.SelectedCells;
                if (cellsCollection.Count != 0)
                    branchSupplier = cellsCollection[0].Item as BranchSupplier;
            }

            string branchID = branchSupplier.BranchID.ToString();
            string supplierName = branchSupplier.SupplierName.ToString();
            string productSupplied = branchSupplier.ProductSupplied.ToString();
            string managerName = branchSupplier.ManagerName.ToString();

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(addRecordQuery, connection);

                command.Parameters.AddWithValue("@BranchID", branchID);
                command.Parameters.AddWithValue("@SupplierName", supplierName);
                command.Parameters.AddWithValue("@ProductSupplied", productSupplied);
                command.Parameters.AddWithValue("@ManagerName", managerName);

                if (command.ExecuteNonQuery() == 1)
                {
                    viewModel.BranchSupplier.Clear();
                    viewModel.BranchSupplier.Add(branchSupplier);
                    MessageBox.Show("Record added.", "", MessageBoxButton.OK);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            connection.Close();
        }


        /// <summary>
        /// This is triggered when the 'Delete' button is clicked. It deletes the selected record from the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBranchSupplierDelete_Click(object sender, RoutedEventArgs e)
        {
            string deleteRecordQuery = $"DELETE FROM jhm_ictprg402.branch_supplier WHERE id = @ID;";

            BranchSupplier branchSupplier = new BranchSupplier();
            IList<DataGridCellInfo> cellsCollection;

            if (dataGridBranchSupplier.SelectedCells != null)
            {
                cellsCollection = dataGridBranchSupplier.SelectedCells;
                if (cellsCollection.Count != 0)
                    branchSupplier = cellsCollection[0].Item as BranchSupplier;
            }

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(deleteRecordQuery, connection);
                string id = branchSupplier.ID.ToString();
                command.Parameters.AddWithValue("@ID", id);

                if (command.ExecuteNonQuery() == 1)
                {
                    viewModel.BranchSupplier.Remove(branchSupplier);
                    MessageBox.Show("Record deleted.", "", MessageBoxButton.OK);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            connection.Close();
        }


        /// <summary>
        /// This is triggered when the 'Update' button is clicked. It updates the existing record in the database that matches the user's input.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBranchSupplierUpdate_Click(object sender, RoutedEventArgs e)
        {
            string updateRecordQuery = $"UPDATE jhm_ictprg402.branch_supplier SET branch_id = @BranchID, supplier_name = @SupplierName, product_supplied = @ProductSupplied, manager_name = @ManagerName WHERE id = @ID;";

            BranchSupplier branchSupplier = new BranchSupplier();
            IList<DataGridCellInfo> cellsCollection;

            if (dataGridBranchSupplier.SelectedCells != null)
            {
                cellsCollection = dataGridBranchSupplier.SelectedCells;
                if (cellsCollection.Count != 0)
                    branchSupplier = cellsCollection[0].Item as BranchSupplier;
            }

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(updateRecordQuery, connection);

                string id = branchSupplier.ID.ToString();
                string branchID = branchSupplier.BranchID.ToString();
                string supplierName = branchSupplier.SupplierName.ToString();
                string productSupplied = branchSupplier.ProductSupplied.ToString();
                string managerName = branchSupplier.ManagerName.ToString();

                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@BranchID", branchID);
                command.Parameters.AddWithValue("@SupplierName", supplierName);
                command.Parameters.AddWithValue("@ProductSupplied", productSupplied);
                command.Parameters.AddWithValue("@ManagerName", managerName);

                if (command.ExecuteNonQuery() == 1)
                {
                    viewModel.BranchSupplier.Clear();
                    viewModel.BranchSupplier.Add(branchSupplier);
                    MessageBox.Show("Record updated.", "", MessageBoxButton.OK);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            connection.Close();
        }


        /// <summary>
        /// This function is triggered when the 'Search' button is clicked. It opens the Search Branch Supplier window, where the user
        /// enters the fields to search. If the dialog returns true ('Search'), the search is performed and results displayed.
        /// Otherwise, if the dialog returns false (search window closed), no action is performed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBranchSupplierSearch_Click(object sender, RoutedEventArgs e)
        {
            BranchSupplierSearch searchWindow = new BranchSupplierSearch();
            searchWindow.Owner = this;
            bool? result = searchWindow.ShowDialog();

            if (result == true)
            {
                viewModel.BranchSupplier.Clear();

                if (searchWindow.SearchByBranchID().Count != 0)
                {
                    foreach (BranchSupplier branchSupplier in searchWindow.SearchByBranchID())
                    {
                        viewModel.BranchSupplier.Add(branchSupplier);
                    }
                }

                else if (searchWindow.SearchBySupplierName().Count != 0)
                {
                    foreach (BranchSupplier branchSupplier in searchWindow.SearchBySupplierName())
                    {
                        viewModel.BranchSupplier.Add(branchSupplier);
                    }
                }

                else if (searchWindow.SearchByProductSupplied().Count != 0)
                {
                    foreach (BranchSupplier branchSupplier in searchWindow.SearchByProductSupplied())
                    {
                        viewModel.BranchSupplier.Add(branchSupplier);
                    }
                }

                else if (searchWindow.SearchByManagerName().Count != 0)
                {
                    foreach (BranchSupplier branchSupplier in searchWindow.SearchByManagerName())
                    {
                        viewModel.BranchSupplier.Add(branchSupplier);
                    }
                }
            }

            else
            {
                MessageBox.Show("No results found.", "", MessageBoxButton.OK);
            }
        }


        /// <summary>
        /// This is triggered when the 'Clear All' button is clicked. It sets the database view back to default.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBranchSupplierClearAll_Click(object sender, RoutedEventArgs e)
        {
            viewModel.BranchSupplier.Clear();
        }

        // BRANCH SUPPLIER ends
    }
}