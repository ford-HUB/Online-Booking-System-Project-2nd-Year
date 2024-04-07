using Login_page.Resources.MAIN_PAGE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace Login_page
{
    public partial class DisplayLoginPage : Form
    {
        
        public DisplayLoginPage()
        {
            InitializeComponent();
        }
        

        private void btnLogin_Click(object sender, EventArgs e) // EVENT HANDLER FOR LOGIN BUTTON
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            // Validate username and password
            List<string> errorMessages = new List<string>();

            if (string.IsNullOrWhiteSpace(email))
            {
                errorMessages.Add("Username is required.");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                errorMessages.Add("Password is required.");
            }

            // If there are any errors, display them and return
            if (errorMessages.Any())
            {
                MessageBox.Show(string.Join("\n", errorMessages));
                return;
            }

            //Condition for email and password matched from the register database
            if (authenthicationUser(email, password))
            {
                Weather weather = new Weather();
                weather.Show();
                this.Hide();
            }
            else 
            {
                MessageBox.Show("Invalid, please double check your email and password!");
            }
        }

        public bool authenthicationUser(string username, string password) 
            // This authenthication is tp retrieve the data registered by the user from the database
            // Match it to login page to open
        {
            string connectionString = "Data Source=LAPTOP-4MKBH0NF\\SQLEXPRESS;Initial Catalog=EasyTrip_db;Integrated Security=True";

            string query = "SELECT COUNT(*) FROM Registration WHERE Username = @Username AND Password = @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    try
                    {
                        connection.Open();
                        int count = (int)command.ExecuteScalar(); 

                        return count > 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error authenticating user: {ex.Message}");
                        return false;
                    }
                }
            }
        }

        private void btnRegistrationForm_Click(object sender, EventArgs e) // Portal to RegistrationPage
        {

            RegistrationPage registrationPage = new RegistrationPage();
            registrationPage.Show();
            this.Hide();
       

        }
    }
}
