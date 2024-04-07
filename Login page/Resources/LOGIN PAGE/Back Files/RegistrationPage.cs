using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 
//Password Privacy database;
using BCrypt;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;


namespace Login_page
{
    public partial class RegistrationPage : Form
    {
   
        public RegistrationPage()
        {
            InitializeComponent();
        }



        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Check if all required fields are filled
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastname.Text) ||
                string.IsNullOrWhiteSpace(txtNumber.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtUserPassword.Text))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }
            // insert MySql Query
            DBConnector();

            
            

        }

        public void DBConnector()
        {
            //DATA BINDING
            string Firstname = txtFirstName.Text;
            string Lastname = txtLastname.Text;
            DateTime birthdate = BirthPicker.Value;
            int genderSelection = -1;

            string phoneNumber = txtNumber.Text;
            string email = txtEmail.Text;
            string Username = txtUsername.Text;
            //Data Privacy for customer password
            string UserPassword = txtUserPassword.Text;

            // Gender selection
            if (CheckerMale.Checked)
            {
                genderSelection = 1;
            }
            else if (FemaleChecker.Checked)
            {
                genderSelection = 0;
            }

            string connectionS = "Data Source=LAPTOP-4MKBH0NF\\SQLEXPRESS;Initial Catalog=EasyTrip_db;Integrated Security=True";
            //INSERT SQL STATREMENT
            string query = "INSERT INTO Registration(firstname, lastname, birthdate, Gender, Phone, Email, Username, Password) VALUES(@firstname,@lastname,@birthdate,@Gender,@Phone,@Email,@Username,@Password)";
            // Establish connection and command objects
            using (SqlConnection conn = new SqlConnection(connectionS))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("@firstName", Firstname);
                    cmd.Parameters.AddWithValue("@lastName", Lastname);
                    cmd.Parameters.AddWithValue("@birthdate", birthdate);
                    cmd.Parameters.AddWithValue("@Gender", genderSelection);
                    cmd.Parameters.AddWithValue("@Phone", phoneNumber);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Username", Username);
                    cmd.Parameters.AddWithValue("@Password", UserPassword);

                    //try catch
                    try
                    {
                        conn.Open();
                        int checker = cmd.ExecuteNonQuery();

                        if (checker > 0)
                        {
                            MessageBox.Show("Your account has sucessfully registered");
                        }
                        else
                        {
                            MessageBox.Show("Captured error!");
                        }
                    }
                    catch(Exception m)
                    {
                        MessageBox.Show($"Error: {m.Message}");
                    }

                }
            }


        }
        private void btnRegistrationForm_Click(object sender, EventArgs e)
        {
            DisplayLoginPage displayLoginPage = new DisplayLoginPage();
            displayLoginPage.Show();
            this.Hide();
        }
    }
}
