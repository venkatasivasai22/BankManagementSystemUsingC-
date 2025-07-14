using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bank.Service;
using Bank.Model;
using Bank.DAO;

namespace BankUI
{
    public partial class UserLoginForm : Form
    {
        public UserLoginForm()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtEmail.Text.Trim();
                if (!int.TryParse(txtPassword.Text, out int password))
                {
                    MessageBox.Show("Please enter a valid password.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var bankUserDAO = new BankUserDAOImplementation();
                var userDetails = await bankUserDAO.GetUserDetailsByUsingEmailAndPasswordAsync(email, password);

                if (userDetails != null)
                {
                    var unifiedDashboard = new UnifiedDashboardForm(false, userDetails, password,email);
                    unifiedDashboard.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid Email or Password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var registrationForm = new RegistrationForm();
            registrationForm.ShowDialog();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     
    }
}