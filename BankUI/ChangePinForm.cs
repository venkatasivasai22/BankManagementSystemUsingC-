using System;
using System.Windows.Forms;
using Bank.DAO;
using Bank.Model;

namespace BankUI
{
    public partial class ChangePinForm : Form
    {
        private readonly BankUserDetails _currentUser;
        private readonly IBankUserDAO _bankUserDAO;

        public ChangePinForm(BankUserDetails user)
        {
            InitializeComponent();
            _currentUser = user;
            _bankUserDAO = new BankUserDAOImplementation();
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtExistingPin.Text, out int existingPin) ||
                    !int.TryParse(txtNewPin.Text, out int newPin) ||
                    !int.TryParse(txtConfirmPin.Text, out int confirmPin))
                {
                    MessageBox.Show("Please enter valid 4-digit PINs", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (existingPin != _currentUser.Pin)
                {
                    MessageBox.Show("Existing PIN is incorrect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (newPin != confirmPin)
                {
                    MessageBox.Show("New PIN and Confirm PIN do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (newPin.ToString().Length != 4)
                {
                    MessageBox.Show("PIN must be 4 digits", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

               
                  int result =   await _bankUserDAO.UpdatePinNumberByUsingId(newPin, _currentUser.Id);
                    if (result > 0)
                    {
                        MessageBox.Show("PIN updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
               
                
                
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}