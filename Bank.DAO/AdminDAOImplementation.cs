using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.DAO
{
    public class AdminDAOImplementation : IAdminDAO
    {
        // hello 
        /// <summary>
        /// hello i changed the code.
        /// </summary>
        private static readonly string connectionString = "Server=INHYNVMUSAM01;Database=BankManagement;Trusted_Connection=True;";
        private static readonly string adminLoginQuery = "SELECT * FROM admin_details WHERE Admin_Emailid = @Email AND Admin_Password = @Password";

        public async Task<bool> GetAdminDetailsByEmailAndPasswordAsync(string email, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand(adminLoginQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            return reader.HasRows;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Exception: {ex.Message}");
                return false;
            }
        }
    }
}
