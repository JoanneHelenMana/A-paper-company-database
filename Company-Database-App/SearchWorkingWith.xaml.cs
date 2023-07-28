using A_Company_Database_App;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Windows;


namespace Company_Database_App
{
    /// <summary>
    /// Interaction logic for SearchWorkingWith.xaml
    /// </summary>
    public partial class WorkingWithSearch : Window
    {
        private string dbconnectionString = "datasource=localhost; port=3306; username=root; password='';";
        private MySqlConnection connection;
        private readonly ViewModel viewModel;

        public WorkingWithSearch()
        {
            InitializeComponent();
            connection = new MySqlConnection(dbconnectionString);
            viewModel = new ViewModel
            {
                WorkingWith = new ObservableCollection<WorkingWith>(),
            };
            this.DataContext = viewModel;
        }


        /// <summary>
        /// This function returns a collection of working with pairs that match the employee ID given by the user.
        /// </summary>
        /// <returns></returns>
        public List<WorkingWith> SearchByEmployeeID()
        {
            List<WorkingWith> searchResults = new List<WorkingWith>();

            string employeeID = textBoxEmployeeID.Text;
            string searchByEmployeeIDQuery = "SELECT * FROM jhm_ictprg402.working_with WHERE employee_id = @EmployeeID;";

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(searchByEmployeeIDQuery, connection);
                command.Parameters.AddWithValue("@EmployeeID", employeeID);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    WorkingWith workingWith = new WorkingWith();

                    workingWith.EmployeeID = Convert.ToInt32(reader["employee_id"]);
                    workingWith.ClientID = Convert.ToInt32(reader["client_id"]);
                    workingWith.TotalSales = Convert.ToInt32(reader["total_sales"]);

                    searchResults.Add(workingWith);
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
        /// This functions returns a collection of working with pairs that match the client ID provided by the user.
        /// </summary>
        /// <returns></returns>
        public List<WorkingWith> SearchByClientID()
        {
            List<WorkingWith> searchResults = new List<WorkingWith>();

            string clientID = textBoxClientID.Text;
            string searchByClientIDQuery = "SELECT * FROM jhm_ictprg402.working_with WHERE client_id = @ClientID;";

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(searchByClientIDQuery, connection);
                command.Parameters.AddWithValue("@ClientID", clientID);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    WorkingWith workingWith = new WorkingWith();

                    workingWith.EmployeeID = Convert.ToInt32(reader["employee_id"]);
                    workingWith.ClientID = Convert.ToInt32(reader["client_id"]);
                    workingWith.TotalSales = Convert.ToInt32(reader["total_sales"]);

                    searchResults.Add(workingWith);
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
        /// This functions returns a collection of working with pairs that match the total sales provided by the user.
        /// </summary>
        /// <returns></returns>
        public List<WorkingWith> SearchByTotalSales()
        {
            List<WorkingWith> searchResults = new List<WorkingWith>();

            string totalSales = textBoxTotalSales.Text;
            string searchByTotalSalesQuery = "SELECT * FROM jhm_ictprg402.working_with WHERE total_sales = @TotalSales;";

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(searchByTotalSalesQuery, connection);
                command.Parameters.AddWithValue("@TotalSales", totalSales);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    WorkingWith workingWith = new WorkingWith();

                    workingWith.EmployeeID = Convert.ToInt32(reader["employee_id"]);
                    workingWith.ClientID = Convert.ToInt32(reader["client_id"]);
                    workingWith.TotalSales = Convert.ToInt32(reader["total_sales"]);

                    searchResults.Add(workingWith);
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
        /// This is triggered when the 'Search by Employee ID' checkbox is checked. It unchecks the remaining search categories.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxSearchByEmployeeID_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxSearchByClientID.IsChecked = false;
            checkBoxSearchByTotalSales.IsChecked = false;

            textBoxEmployeeID.IsEnabled = true;
            textBoxClientID.IsEnabled = false;
            textBoxTotalSales.IsEnabled = false;
        }


        /// <summary>
        /// This is triggered when the 'Search by Client ID' checkbox is checked. It unchecks the remaining search categories.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxSearchByClientID_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxSearchByEmployeeID.IsChecked = false;
            checkBoxSearchByTotalSales.IsChecked = false;

            textBoxEmployeeID.IsEnabled = false;
            textBoxClientID.IsEnabled = true;
            textBoxTotalSales.IsEnabled = false;
        }


        /// <summary>
        /// This is triggered when the 'Search by Total Sales' checkbox is checked. It unchecks the remaining search categories.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxSearchByTotalSales_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxSearchByEmployeeID.IsChecked = false;
            checkBoxSearchByClientID.IsChecked = false;

            textBoxEmployeeID.IsEnabled = false;
            textBoxClientID.IsEnabled = false;
            textBoxTotalSales.IsEnabled = true;
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
            checkBoxSearchByEmployeeID.IsChecked = false;
            checkBoxSearchByClientID.IsChecked = false;
            checkBoxSearchByTotalSales.IsChecked = false;

            textBoxEmployeeID.Clear();
            textBoxEmployeeID.IsEnabled = false;
            textBoxClientID.Clear();
            textBoxClientID.IsEnabled = false;
            textBoxTotalSales.Clear();
            textBoxTotalSales.IsEnabled = false;
        }
    }
}
