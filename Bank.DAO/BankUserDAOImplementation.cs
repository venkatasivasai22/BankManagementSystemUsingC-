using Bank.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.DAO
{
    public class BankUserDAOImplementation : IBankUserDAO
    {
        private static readonly string connectionString = "Server=INHYNVMUSAM01;Database=BankManagement;Trusted_Connection=True;";
        private static readonly string insertQuery = "INSERT INTO bank_user_details(User_Name, User_EmailId, User_Aadher_Number, User_Pan_Number, User_Mobile_Number, User_Address, User_Gender, User_Status, User_Amount) VALUES (@Name, @Email, @Aadhar, @Pan, @Mobile, @Address, @Gender, @Status, @Amount)";
        private static readonly string selectAllQuery = "SELECT * FROM bank_user_details";
        private static readonly string updateQuery = "UPDATE bank_user_details SET User_Pin = @Pin, User_Account_Number = @AccountNumber, User_Status = 'Accepted' WHERE User_ID = @Id";
        private static readonly string deleteQuery = "DELETE FROM bank_user_details WHERE User_ID = @Id";
        private static readonly string userLoginQuery = "SELECT * FROM bank_user_details WHERE User_EmailId = @Email AND User_Pin = @Pin";
        private static readonly string updateAmountQuery = "UPDATE bank_user_details SET User_Amount = @Amount WHERE User_Account_Number = @Account";
        private static readonly string debitQuery = "UPDATE bank_user_details SET User_Amount = @Amount WHERE User_EmailId = @Email";
        private static readonly string phoneDetailsQuery = "SELECT * FROM bank_user_details WHERE User_Mobile_Number = @Mobile";
        private static readonly string phoneAmountTransferQuery = "UPDATE bank_user_details SET User_Amount = @Amount WHERE User_Mobile_Number = @Mobile";
        private static readonly string updatepin = "update bank_user_details set User_Pin=@pin where User_ID=@pin";

        public async Task InsertBankUserDetailsAsync(BankUserDetails bankUserDetails)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", bankUserDetails.Name);
                    command.Parameters.AddWithValue("@Email", bankUserDetails.EmailId);
                    command.Parameters.AddWithValue("@Aadhar", bankUserDetails.AadharNumber);
                    command.Parameters.AddWithValue("@Pan", bankUserDetails.PanNumber);
                    command.Parameters.AddWithValue("@Mobile", bankUserDetails.MobileNumber);
                    command.Parameters.AddWithValue("@Address", bankUserDetails.Address);
                    command.Parameters.AddWithValue("@Gender", bankUserDetails.Gender);
                    command.Parameters.AddWithValue("@Status", "pending");
                    command.Parameters.AddWithValue("@Amount", bankUserDetails.Amount);

                    int result = await command.ExecuteNonQueryAsync();
                    Console.WriteLine(result > 0 ? "Registration successful!" : "Registration failed.");
                }
            }
        }

        public async Task<List<BankUserDetails>> SelectAllBankUserDetailsAsync()
        {
            var userList = new List<BankUserDetails>();
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(selectAllQuery, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            userList.Add(new BankUserDetails
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.IsDBNull(1) ? "N/A" : reader.GetString(1),
                                EmailId = reader.IsDBNull(2) ? "N/A" : reader.GetString(2),
                                AadharNumber = reader.IsDBNull(3) ? 0 : reader.GetInt64(3),
                                PanNumber = reader.IsDBNull(4) ? "N/A" : reader.GetString(4),
                                MobileNumber = reader.IsDBNull(5) ? 0 : reader.GetInt64(5),
                                Address = reader.IsDBNull(6) ? "N/A" : reader.GetString(6),
                                Gender = reader.IsDBNull(7) ? "N/A" : reader.GetString(7),
                                Status = reader.IsDBNull(8) ? "N/A" : reader.GetString(8),
                                Amount = reader.IsDBNull(9) ? 0.0 : reader.GetDouble(9),
                                Pin = reader.IsDBNull(10) ? 0 : reader.GetInt32(10),
                                AccountNumber = reader.IsDBNull(11) ? 0 : reader.GetInt32(11)
                            });
                        }
                    }
                }
            }
            return userList;
        }

        public async Task<int> UpdatePinAndAccountNumberAsync(int pin, int accountNumber, int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Pin", pin);
                    command.Parameters.AddWithValue("@AccountNumber", accountNumber);
                    command.Parameters.AddWithValue("@Id", id);
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<BankUserDetails> GetUserDetailsByUsingEmailAndPasswordAsync(string emailId, int pin)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(userLoginQuery, connection))
                {
                    command.Parameters.AddWithValue("@Email", emailId);
                    command.Parameters.AddWithValue("@Pin", pin);
                    
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new BankUserDetails
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Gender = reader.GetString(7),
                                Amount = reader.GetDouble(9),
                                Pin = reader.GetInt32(10),
                                AccountNumber = reader.GetInt32(11),
                                EmailId = reader.GetString(2),
                                AadharNumber = reader.GetInt64(3),
                                PanNumber = reader.GetString(4),
                                MobileNumber = reader.GetInt64(5),
                                Address = reader.GetString(6),
                                Status = reader.GetString(8)
                            };
                        }
                    }
                }
            }
            return null;
        }

        public async Task<int> UpdateAmountByUsingAccountNumberAsync(double amount, int account)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(updateAmountQuery, connection))
                {
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@Account", account);
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<int> DebitAsync(string email, double amount)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(debitQuery, connection))
                {
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@Email", email);
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<BankUserDetails> PhoneNumberDetailsAsync(long number)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(phoneDetailsQuery, connection))
                {
                    command.Parameters.AddWithValue("@Mobile", number);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new BankUserDetails
                            {
                                Id = reader.GetInt32(0),
                                MobileNumber = reader.GetInt64(5),
                                Amount = reader.GetDouble(9),
                            };
                        }
                    }
                }
            }
            return null;
        }

        public async Task<int> PhoneAmountTransferAsync(long number, double amount)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(phoneAmountTransferQuery, connection))
                {
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@Mobile", number);
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<int> UpdatePinNumberByUsingId(int pin, int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("UPDATE bank_user_details SET User_Pin = @pin WHERE User_ID = @id", connection))
                {
                    command.Parameters.AddWithValue("@pin", pin);
                    command.Parameters.AddWithValue("@id", id);
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
