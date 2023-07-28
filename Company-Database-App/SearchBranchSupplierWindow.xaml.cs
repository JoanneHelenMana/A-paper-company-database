using A_Company_Database_App;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;


namespace Company_Database_App
{
    /// <summary>
    /// Interaction logic for SearchBranchSupplierWindow.xaml
    /// </summary>
    public partial class BranchSupplierSearch : Window
    {
        private string dbconnectionString = "datasource=localhost; port=3306; username=root; password='';";
        private MySqlConnection connection;
        private readonly ViewModel viewModel;

        public BranchSupplierSearch()
        {
            InitializeComponent();
            connection = new MySqlConnection(dbconnectionString);
            viewModel = new ViewModel
            {
                BranchSupplier = new ObservableCollection<BranchSupplier>(),
            };
            this.DataContext = viewModel;
        }


        /// <summary>
        /// This function returns a collection of branch supplier records that match the branch ID input by the user.
        /// </summary>
        /// <returns></returns>
        public List<BranchSupplier> SearchByBranchID()
        {
            List<BranchSupplier> searchResults = new List<BranchSupplier>();
            string branchID = textBoxBranchID.Text;
            string searchByBranchIDQuery = "SELECT * FROM jhm_ictprg402.branch_supplier WHERE branch_id = @BranchID;";

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(searchByBranchIDQuery, connection);
                command.Parameters.AddWithValue("@BranchID", branchID);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    BranchSupplier branchSupplier = new BranchSupplier();

                    branchSupplier.ID = Convert.ToInt32(reader["id"]);
                    branchSupplier.BranchID = Convert.ToInt32(reader["branch_id"]);
                    branchSupplier.SupplierName = Convert.ToString(reader["supplier_name"]);
                    branchSupplier.ProductSupplied = Convert.ToString(reader["product_supplied"]);
                    branchSupplier.ManagerName = Convert.ToString(reader["manager_name"]);

                    searchResults.Add(branchSupplier);
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
        /// This function returns a collection of branch supplier records that match the supplier name input by the user.
        /// </summary>
        /// <returns></returns>
        public List<BranchSupplier> SearchBySupplierName()
        {
            List<BranchSupplier> searchResults = new List<BranchSupplier>();
            string supplierName = textBoxSupplierName.Text;
            string searchBySupplierNameQuery = "SELECT * FROM jhm_ictprg402.branch_supplier WHERE supplier_name = @SupplierName;";

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(searchBySupplierNameQuery, connection);
                command.Parameters.AddWithValue("@SupplierName", supplierName);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    BranchSupplier branchSupplier = new BranchSupplier();

                    branchSupplier.ID = Convert.ToInt32(reader["id"]);
                    branchSupplier.BranchID = Convert.ToInt32(reader["branch_id"]);
                    branchSupplier.SupplierName = Convert.ToString(reader["supplier_name"]);
                    branchSupplier.ProductSupplied = Convert.ToString(reader["product_supplied"]);
                    branchSupplier.ManagerName = Convert.ToString(reader["manager_name"]);

                    searchResults.Add(branchSupplier);
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
        /// This function returns a collection of branch supplier records that match the product input by the user.
        /// </summary>
        /// <returns></returns>
        public List<BranchSupplier> SearchByProductSupplied()
        {
            List<BranchSupplier> searchResults = new List<BranchSupplier>();
            string productSupplied = textBoxProductSupplied.Text;
            string searchByProductSuppliedQuery = "SELECT * FROM jhm_ictprg402.branch_supplier WHERE product_supplied = @ProductSupplied;";

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(searchByProductSuppliedQuery, connection);
                command.Parameters.AddWithValue("@ProductSupplied", productSupplied);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    BranchSupplier branchSupplier = new BranchSupplier();

                    branchSupplier.ID = Convert.ToInt32(reader["id"]);
                    branchSupplier.BranchID = Convert.ToInt32(reader["branch_id"]);
                    branchSupplier.SupplierName = Convert.ToString(reader["supplier_name"]);
                    branchSupplier.ProductSupplied = Convert.ToString(reader["product_supplied"]);
                    branchSupplier.ManagerName = Convert.ToString(reader["manager_name"]);

                    searchResults.Add(branchSupplier);
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
        /// This function returns a collection of branch supplier records that match the manager name input by the user.
        /// </summary>
        /// <returns></returns>
        public List<BranchSupplier> SearchByManagerName()
        {
            List<BranchSupplier> searchResults = new List<BranchSupplier>();
            string managerName = textBoxManagerName.Text;
            string searchByManagerNameQuery = "SELECT * FROM jhm_ictprg402.branch_supplier WHERE manager_name = @ManagerName;";

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(searchByManagerNameQuery, connection);
                command.Parameters.AddWithValue("@ManagerName", managerName);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    BranchSupplier branchSupplier = new BranchSupplier();

                    branchSupplier.ID = Convert.ToInt32(reader["id"]);
                    branchSupplier.BranchID = Convert.ToInt32(reader["branch_id"]);
                    branchSupplier.SupplierName = Convert.ToString(reader["supplier_name"]);
                    branchSupplier.ProductSupplied = Convert.ToString(reader["product_supplied"]);
                    branchSupplier.ManagerName = Convert.ToString(reader["manager_name"]);

                    searchResults.Add(branchSupplier);
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
        /// This is triggered when the 'Search by Branch ID' checkbox is checked. It unchecks the remaining search categories.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxSearchByBranchID_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxSearchBySupplierName.IsChecked = false;
            checkBoxSearchByProductSupplied.IsChecked = false;
            checkBoxSearchByManagerName.IsChecked = false;

            textBoxBranchID.IsEnabled = true;
            textBoxSupplierName.IsEnabled = false;
            textBoxProductSupplied.IsEnabled = false;
            textBoxManagerName.IsEnabled = false;
        }


        /// <summary>
        /// This is triggered when the 'Search by Supplier Name' checkbox is checked. It unchecks the remaining search categories.
        /// </summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxSearchBySupplierName_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxSearchByBranchID.IsChecked = false;
            checkBoxSearchByProductSupplied.IsChecked = false;
            checkBoxSearchByManagerName.IsChecked = false;

            textBoxBranchID.IsEnabled = false;
            textBoxSupplierName.IsEnabled = true;
            textBoxProductSupplied.IsEnabled = false;
            textBoxManagerName.IsEnabled = false;
        }


        /// <summary>
        /// This is triggered when the 'Search by Product Supplied' checkbox is checked. It unchecks the remaining search categories.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxSearchByProductSupplied_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxSearchByBranchID.IsChecked = false;
            checkBoxSearchBySupplierName.IsChecked = false;
            checkBoxSearchByManagerName.IsChecked = false;

            textBoxBranchID.IsEnabled = false;
            textBoxSupplierName.IsEnabled = false;
            textBoxProductSupplied.IsEnabled = true;
            textBoxManagerName.IsEnabled = false;
        }


        /// <summary>
        /// This is triggered when the 'Search by Manager Name' checkbox is checked. It unchecks the remaining search categories.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxSearchByManagerName_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxSearchByBranchID.IsChecked = false;
            checkBoxSearchBySupplierName.IsChecked = false;
            checkBoxSearchByProductSupplied.IsChecked = false;

            textBoxBranchID.IsEnabled = false;
            textBoxSupplierName.IsEnabled = false;
            textBoxProductSupplied.IsEnabled = false;
            textBoxManagerName.IsEnabled = true;
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
            checkBoxSearchByBranchID.IsChecked = false;
            textBoxBranchID.Clear();
            textBoxBranchID.IsEnabled = false;

            checkBoxSearchBySupplierName.IsChecked = false;
            textBoxSupplierName.Clear();
            textBoxSupplierName.IsEnabled = false;

            checkBoxSearchByProductSupplied.IsChecked = false;
            textBoxProductSupplied.Clear();
            textBoxProductSupplied.IsEnabled = false;

            checkBoxSearchByManagerName.IsChecked = false;
            textBoxManagerName.Clear();
            textBoxManagerName.IsEnabled = false;
        }
    }
}
