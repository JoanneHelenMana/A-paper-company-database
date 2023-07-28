using A_Company_Database_App;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;


namespace Company_Database_App
{
    /// <summary>
    /// Interaction logic for SearchClientWindow.xaml
    /// </summary>
    public partial class ClientSearch : Window
    {
        private string dbconnectionString = "datasource=localhost; port=3306; username=root; password='';";
        private MySqlConnection connection;
        private readonly ViewModel viewModel;

        public ClientSearch()
        {
            InitializeComponent();
            connection = new MySqlConnection(dbconnectionString);
            viewModel = new ViewModel
            {
                Clients = new ObservableCollection<Client>(),
            };
            this.DataContext = viewModel;
        }


        /// <summary>
        /// This function returns a collection of clients that match the name given by the user.
        /// </summary>
        public List<Client> SearchByName()
        {
            List<Client> searchResults = new List<Client>();

            string clientName = textBoxClientName.Text;
            string searchByNameQuery = "SELECT * FROM jhm_ictprg402.clients WHERE client_name = @ClientName;";

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(searchByNameQuery, connection);
                command.Parameters.AddWithValue("@ClientName", clientName);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Client client = new Client();

                    client.ID = Convert.ToInt32(reader["id"]);
                    client.Name = Convert.ToString(reader["client_name"]);
                    client.BranchID = Convert.ToInt32(reader["branch_id"]);
                   
                    searchResults.Add(client);
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
        /// This functions returns a collection of clients that match the branch id provided by the user.
        /// </summary>
        public List<Client> SearchByBranchID()
        {
            List<Client> searchResults = new List<Client>();

            string branchID = textBoxBranchID.Text;
            string searchByBranchIDQuery = "SELECT * FROM jhm_ictprg402.clients WHERE branch_id=@BranchID;";

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(searchByBranchIDQuery, connection);
                command.Parameters.AddWithValue("@BranchID", branchID);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Client client = new Client();

                    client.ID = Convert.ToInt32(reader["id"]);
                    client.Name = Convert.ToString(reader["client_name"]);
                    client.BranchID = Convert.ToInt32(reader["branch_id"]);

                    searchResults.Add(client);
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
            checkBoxSearchByBranchID.IsChecked = false;

            textBoxClientName.IsEnabled = true;
            textBoxBranchID.IsEnabled = false;
        }


        /// <summary>
        /// This is triggered when the 'Search by branch id' checkbox is checked. It unchecks the remaining search categories.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxSearchByBranchID_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxSearchByClientName.IsChecked = false;

            textBoxClientName.IsEnabled = false;
            textBoxBranchID.IsEnabled = true;
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
            checkBoxSearchByClientName.IsChecked = false;
            checkBoxSearchByBranchID.IsChecked = false;

            textBoxClientName.Clear();
            textBoxClientName.IsEnabled = false;
            textBoxBranchID.Clear();
            textBoxBranchID.IsEnabled = false;
        }
    }
}
