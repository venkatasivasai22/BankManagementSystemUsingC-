using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bank.Service;
using Bank.Model;
using Bank.DAO;
using Bank.Exception;

namespace BankUI
{
    public partial class RegistrationForm : Form
    {
        private readonly IBankUserDAO _bankUserDAO;

        public RegistrationForm()
        {
            InitializeComponent();
            _bankUserDAO = new BankUserDAOImplementation();
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                var allUsers = await _bankUserDAO.SelectAllBankUserDetailsAsync();
                var bankUserDetails = new BankUserDetails();

                // Validate Name
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Please enter your name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                bankUserDetails.Name = txtName.Text.Trim();

                // Validate Email
                string email = txtEmail.Text.Trim();
                if (!email.EndsWith("@gmail.com"))
                {
                    MessageBox.Show("Please enter a valid Gmail address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (allUsers.Any(user => user.EmailId == email))
                {
                    MessageBox.Show("Email ID already exists.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                bankUserDetails.EmailId = email;

                // Validate Mobile Number
                if (!long.TryParse(txtMobile.Text, out long mobile) || !IsValidPhoneNumber(mobile.ToString()))
                {
                    MessageBox.Show("Please enter a valid 10-digit mobile number starting with 6-9.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (allUsers.Any(user => user.MobileNumber == mobile))
                {
                    MessageBox.Show("Mobile number already exists.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                bankUserDetails.MobileNumber = mobile;

                // Validate PAN
                string pan = txtPAN.Text.Trim().ToUpper();
                if (pan.Length != 10 || !pan.Take(5).All(char.IsLetter) || !pan.Skip(5).Take(4).All(char.IsDigit) || !char.IsLetter(pan[9]))
                {
                    MessageBox.Show("Please enter a valid PAN number (5 letters + 4 digits + 1 letter).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (allUsers.Any(user => user.PanNumber == pan))
                {
                    MessageBox.Show("PAN number already exists.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                bankUserDetails.PanNumber = pan;

                // Validate Aadhar
                if (!long.TryParse(txtAadhar.Text, out long aadhar) || aadhar < 100000000000 || aadhar > 999999999999)
                {
                    MessageBox.Show("Please enter a valid 12-digit Aadhar number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (allUsers.Any(user => user.AadharNumber == aadhar))
                {
                    MessageBox.Show("Aadhar number already exists.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                bankUserDetails.AadharNumber = aadhar;

                // Validate Address
                if (string.IsNullOrWhiteSpace(txtAddress.Text))
                {
                    MessageBox.Show("Please enter your address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                bankUserDetails.Address = txtAddress.Text.Trim();

                // Validate Gender
                if (cmbGender.SelectedItem == null)
                {
                    MessageBox.Show("Please select your gender.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                bankUserDetails.Gender = cmbGender.SelectedItem.ToString();

                // Validate Amount
                if (!double.TryParse(txtAmount.Text, out double amount) || amount < 0)
                {
                    MessageBox.Show("Please enter a valid initial amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                bankUserDetails.Amount = amount;

                await _bankUserDAO.InsertBankUserDetailsAsync(bankUserDetails);
                MessageBox.Show("User registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Registration failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return phoneNumber.Length == 10 && phoneNumber[0] >= '6' && phoneNumber[0] <= '9' && phoneNumber.All(char.IsDigit);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}