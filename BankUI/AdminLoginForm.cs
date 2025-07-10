using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bank.Service;
using Bank.DAO;

namespace BankUI
{
    public partial class AdminLoginForm : Form
    {
        public AdminLoginForm()
        {
            InitializeComponent();
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtEmail.Text.Trim();
                string password = txtPassword.Text.Trim();

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please enter email and password.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var adminDAO = new AdminDAOImplementation();
                bool isValid = await adminDAO.GetAdminDetailsByEmailAndPasswordAsync(email, password);

                if (isValid)
                {
                    var unifiedDashboard = new UnifiedDashboardForm(true);
                    unifiedDashboard.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid credentials.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}