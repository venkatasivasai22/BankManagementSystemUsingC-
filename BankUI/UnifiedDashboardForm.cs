using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bank.Service;
using Bank.DAO;
using Bank.Model;

namespace BankUI
{
    public partial class UnifiedDashboardForm : Form
    {
        private readonly IAdminService _adminService;
        private readonly IBankUserDAO _bankUserDAO;
        private readonly BankUserDetails _currentUser;
        private readonly bool _isAdmin;
        private readonly int _userPin;
        private readonly string _email;

        public UnifiedDashboardForm(bool isAdmin = false, BankUserDetails user = null, int pin = 0, string email = null)
        {
            InitializeComponent();
            _adminService = new AdminServiceImp();
            _bankUserDAO = new BankUserDAOImplementation();
            _currentUser = user;
            _isAdmin = isAdmin;
            _userPin = pin;
            _email = email;
            
            SetupDashboard();
        }

        private void SetupDashboard()
        {
            if (_isAdmin)
            {
                lblTitle.Text = "Admin Dashboard";
                SetupAdminButtons();
            }
            else
            {
                lblTitle.Text = $"Welcome, {_currentUser?.Name ?? "User"}";
                SetupUserButtons();
            }
        }

        private void SetupAdminButtons()
        {
            btnAction1.Text = "All Users";
            btnAction2.Text = "Pending Requests";
            btnAction3.Visible = false;
            btnAction4.Visible = false;
        }

        private void SetupUserButtons()
        {
            btnAction1.Text = "Check Balance";
            btnAction2.Text = "Debit Money";
            btnAction3.Text = "Credit Money";
            btnAction4.Text = "Transfer Money";
            btnAction4.Visible = true;
            btnChangePin.Visible = true;
        }

        private async void btnAction1_Click(object sender, EventArgs e)
        {
            if (_isAdmin)
                await ShowAllUsers();
            else
                await ShowBalance();
        }

        private async void btnAction2_Click(object sender, EventArgs e)
        {
            if (_isAdmin)
                await ShowPendingRequests();
            else
                ShowTransactionForm("Debit");
        }

        private async void btnAction3_Click(object sender, EventArgs e)
        {
            if (_isAdmin)
                await ShowAccountManagement();
            else
                ShowTransactionForm("Credit");
        }

        private void btnAction4_Click(object sender, EventArgs e)
        {
            ShowTransferForm();
        }

        private async Task ShowAllUsers()
        {
            try
            {
                // Clear any existing buttons from pending requests
                txtDisplay.Controls.Clear();
                txtDisplay.Text = "Loading users...";
                var users = await _bankUserDAO.SelectAllBankUserDetailsAsync();
                
                if (users == null)
                {
                    txtDisplay.Text = "Error: No data returned from database.";
                    return;
                }
                
                if (users.Count == 0)
                {
                    txtDisplay.Text = "No users found in database.";
                    return;
                }

                string content = $"Found {users.Count} users:\r\n\r\n";
                
                foreach (var user in users)
                {
                    content += $"ID: {user.Id} | Name: {user.Name ?? "N/A"}\r\n";
                    content += $"Email: {user.EmailId ?? "N/A"} | Status: {user.Status ?? "N/A"}\r\n";
                    content += $"Balance: ₹{user.Amount:F2}\r\n";
                    content += "------------------------\r\n";
                }
                
                txtDisplay.Text = content;
            }
            catch (Exception ex)
            {
                txtDisplay.Text = $"Error loading users: {ex.Message}\r\n\r\nStack Trace: {ex.StackTrace}";
            }
        }

        private async Task ShowPendingRequests()
        {
            try
            {
                txtDisplay.Text = "Loading pending requests...";
                var users = await _bankUserDAO.SelectAllBankUserDetailsAsync();
                
                if (users == null)
                {
                    txtDisplay.Text = "Error: No data returned from database.";
                    return;
                }
                
                var pendingUsers = users.FindAll(u => u.Status == "pending");
                
                if (pendingUsers.Count == 0)
                {
                    txtDisplay.Text = "No pending requests found.";
                    return;
                }

                // Clear existing controls and text
                txtDisplay.Controls.Clear();
                txtDisplay.Text = "";
                
                int yPosition = 10;
                
                foreach (var user in pendingUsers)
                {
                    // Create user info label
                    var userLabel = new Label
                    {
                        Text = $"Name: {user.Name ?? "N/A"} | Email: {user.EmailId ?? "N/A"} | Mobile: {user.MobileNumber} | Amount: ₹{user.Amount:F2}",
                        Location = new System.Drawing.Point(10, yPosition),
                        Size = new System.Drawing.Size(500, 20),
                        Font = new System.Drawing.Font("Arial", 9)
                    };
                    
                    // Create Accept button
                    var acceptBtn = new Button
                    {
                        Text = "Accept",
                        Location = new System.Drawing.Point(520, yPosition),
                        Size = new System.Drawing.Size(70, 25),
                        BackColor = System.Drawing.Color.LightGreen,
                        Tag = user
                    };
                    acceptBtn.Click += async (s, e) => await AcceptUser((BankUserDetails)acceptBtn.Tag);
                    
                    // Create Reject button
                    var rejectBtn = new Button
                    {
                        Text = "Reject",
                        Location = new System.Drawing.Point(600, yPosition),
                        Size = new System.Drawing.Size(70, 25),
                        BackColor = System.Drawing.Color.LightCoral,
                        Tag = user
                    };
                    rejectBtn.Click += async (s, e) => await RejectUser((BankUserDetails)rejectBtn.Tag);
                    
                    // Add controls to txtDisplay
                    txtDisplay.Controls.Add(userLabel);
                    txtDisplay.Controls.Add(acceptBtn);
                    txtDisplay.Controls.Add(rejectBtn);
                    
                    yPosition += 35;
                }
            }
            catch (Exception ex)
            {
                txtDisplay.Text = $"Error loading pending requests: {ex.Message}\r\n\r\nStack Trace: {ex.StackTrace}";
            }
        }

        private async Task AcceptUser(BankUserDetails user)
        {
            try
            {
                var random = new Random();
                int pin = random.Next(1000, 9999);
                int accountNumber = random.Next(1000000, 9999999);

                int result = await _bankUserDAO.UpdatePinAndAccountNumberAsync(pin, accountNumber, user.Id);
                if (result > 0)
                {
                    MessageBox.Show($"Account Accepted!\nUser: {user.Name}\nAccount Number: {accountNumber}\nPIN: {pin}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await ShowPendingRequests(); // Refresh the list
                }
                else
                {
                    MessageBox.Show("Failed to accept account", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task RejectUser(BankUserDetails user)
        {
            try
            {
                var confirmResult = MessageBox.Show($"Are you sure you want to reject {user.Name}'s account request?", "Confirm Rejection", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (confirmResult == DialogResult.Yes)
                {
                    int result = await _bankUserDAO.DeleteAsync(user.Id);
                    if (result > 0)
                    {
                        MessageBox.Show($"Account request rejected for {user.Name}", "Rejected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await ShowPendingRequests(); // Refresh the list
                    }
                    else
                    {
                        MessageBox.Show("Failed to reject account", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Task ShowAccountManagement()
        {
            string content = "Account Management Options:\r\n\r\n";
            content += "• View all user accounts\r\n";
            content += "• Approve/Reject pending requests\r\n";
            content += "• Monitor transactions\r\n";
            content += "• Generate reports\r\n\r\n";
            content += "Select 'Pending Requests' to manage account approvals.";
            
            DisplayContent(content);
            return Task.CompletedTask;
        }

        private async Task ShowBalance()
        {
            try
            {
                var user = await _bankUserDAO.GetUserDetailsByUsingEmailAndPasswordAsync(_email, _userPin);
                if (user != null)
                {
                    string content = "Account Balance Information:\r\n\r\n";
                    content += $"Account Holder: {user.Name}\r\n";
                    content += $"Account Number: {user.AccountNumber}\r\n";
                    content += $"Current Balance: ₹{user.Amount:F2}\r\n";
                    content += $"Account Status: {user.Status}\r\n";
                    
                    DisplayContent(content);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowTransactionForm(string type)
        {
            string content = $"{type} Transaction:\r\n\r\n";
            content += $"Current Balance: ₹{_currentUser?.Amount:F2}\r\n\n";
            content += "Enter amount in the text box below and click 'Process Transaction'";
            
            DisplayContent(content);
            txtAmount.Visible = true;
            lblAmount.Visible = true;
            btnProcess.Visible = true;
            btnProcess.Text = $"Process {type}";
            btnProcess.Tag = type;
        }

        private void ShowTransferForm()
        {
            string content = "Money Transfer:\r\n\r\n";
            content += $"Your Balance: ₹{_currentUser?.Amount:F2}\r\n\n";
            content += "Enter recipient's phone number and amount below:";
            
            DisplayContent(content);
            txtPhone.Visible = true;
            txtAmount.Visible = true;
            lblPhone.Visible = true;
            lblAmount.Visible = true;
            btnProcess.Visible = true;
            btnProcess.Text = "Transfer Money";
            btnProcess.Tag = "Transfer";
        }

      

        private async void btnProcess_Click(object sender, EventArgs e)
        {
            string action = btnProcess.Tag?.ToString();
            
            if (action == "Transfer")
            {
                await ProcessTransfer();
            }
           
            else
            {
                await ProcessTransaction(action);
            }
        }

       

        private async Task ProcessTransaction(string type)
        {
            try
            {
                if (!double.TryParse(txtAmount.Text, out double amount) || amount <= 0)
                {
                    MessageBox.Show("Please enter a valid amount", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var user = await _bankUserDAO.GetUserDetailsByUsingEmailAndPasswordAsync(_email, _userPin);
                double newBalance;

                if (type == "Debit")
                {
                    if (amount > user.Amount)
                    {
                        MessageBox.Show("Insufficient balance", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    newBalance = user.Amount - amount;
                }
                else
                {
                    newBalance = user.Amount + amount;
                }

                int result = await _bankUserDAO.DebitAsync(_email, newBalance);
                if (result > 0)
                {
                    // Record transaction in statement
                    var bankStatement = new BankStatement
                    {
                        BalanceAmount = newBalance,
                        TransactionAmount = amount,
                        DateOfTransaction = DateTime.Now.Date,
                        TimeOfTransaction = DateTime.Now.TimeOfDay,
                        TransactionType = type,
                        UserId = user.Id
                    };
                    var bankStatementDAO = new BankStatementDAOImplementation();
                    await bankStatementDAO.InsertStatementDetailsAsync(bankStatement);

                    MessageBox.Show($"{type} successful!\nNew Balance: ₹{newBalance:F2}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAmount.Clear();
                    HideTransactionControls();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task ProcessTransfer()
        {
            try
            {
                if (!long.TryParse(txtPhone.Text, out long phoneNumber) || !double.TryParse(txtAmount.Text, out double amount))
                {
                    MessageBox.Show("Please enter valid phone number and amount", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var sender = await _bankUserDAO.GetUserDetailsByUsingEmailAndPasswordAsync(_email, _userPin);
                var receiver = await _bankUserDAO.PhoneNumberDetailsAsync(phoneNumber);

                if (receiver == null)
                {
                    MessageBox.Show("Recipient not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (amount > sender.Amount)
                {
                    MessageBox.Show("Insufficient balance", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                double senderBalance = sender.Amount - amount;
                double receiverBalance = receiver.Amount + amount;

                int debitResult = await _bankUserDAO.DebitAsync(_email, senderBalance);
                int creditResult = await _bankUserDAO.PhoneAmountTransferAsync(phoneNumber, receiverBalance);

                if (debitResult > 0 && creditResult > 0)
                {
                    // Record transfer transaction in statement
                    var bankStatement = new BankStatement
                    {
                        BalanceAmount = senderBalance,
                        TransactionAmount = amount,
                        DateOfTransaction = DateTime.Now.Date,
                        TimeOfTransaction = DateTime.Now.TimeOfDay,
                        TransactionType = "Transfer",
                        UserId = sender.Id
                    };
                    var bankStatementDAO = new BankStatementDAOImplementation();
                    await bankStatementDAO.InsertStatementDetailsAsync(bankStatement);

                    MessageBox.Show($"Transfer successful!\nTransferred: ₹{amount:F2}\nNew Balance: ₹{senderBalance:F2}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPhone.Clear();
                    txtAmount.Clear();
                    HideTransactionControls();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayContent(string content)
        {
            txtDisplay.Text = content;
            HideTransactionControls();
        }

        private void HideTransactionControls()
        {
            txtAmount.Visible = false;
            txtPhone.Visible = false;
            lblAmount.Visible = false;
            lblPhone.Visible = false;
            btnProcess.Visible = false;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChangePin_Click(object sender, EventArgs e)
        {
            var changePinForm = new ChangePinForm(_currentUser);
            changePinForm.ShowDialog();
        }



        
    }
}