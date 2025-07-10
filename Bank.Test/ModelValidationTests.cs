using System;
using Xunit;
using Bank.Model;

namespace Bank.Test
{
    public class ModelValidationTests
    {
        [Fact]
        public void BankUserDetails_ValidData_CreatesSuccessfully()
        {
            // Arrange & Act
            var bankUser = new BankUserDetails
            {
                Id = 1,
                Name = "John Doe",
                EmailId = "john@gmail.com",
                MobileNumber = 9876543210,
                PanNumber = "ABCDE1234F",
                AadharNumber = 123456789012,
                Address = "123 Main St",
                Gender = "Male",
                Amount = 5000,
                AccountNumber = 1234567,
                Pin = 1234,
                Status = "active"
            };

            // Assert
            Assert.Equal(1, bankUser.Id);
            Assert.Equal("John Doe", bankUser.Name);
            Assert.Equal("john@gmail.com", bankUser.EmailId);
            Assert.Equal(9876543210, bankUser.MobileNumber);
            Assert.Equal("ABCDE1234F", bankUser.PanNumber);
            Assert.Equal(123456789012, bankUser.AadharNumber);
            Assert.Equal("123 Main St", bankUser.Address);
            Assert.Equal("Male", bankUser.Gender);
            Assert.Equal(5000, bankUser.Amount);
            Assert.Equal(1234567, bankUser.AccountNumber);
            Assert.Equal(1234, bankUser.Pin);
            Assert.Equal("active", bankUser.Status);
        }

        [Fact]
        public void BankStatement_ValidData_CreatesSuccessfully()
        {
            // Arrange & Act
            var statement = new BankStatement
            {
               
                UserId = 1,
                TransactionAmount = 500,
                BalanceAmount = 4500,
                TransactionType = "Debit",
                DateOfTransaction = DateTime.Now.Date,
                TimeOfTransaction = DateTime.Now.TimeOfDay
            };

            // Assert
           
            Assert.Equal(1, statement.UserId);
            Assert.Equal(500, statement.TransactionAmount);
            Assert.Equal(4500, statement.BalanceAmount);
            Assert.Equal("Debit", statement.TransactionType);
            Assert.Equal(DateTime.Now.Date, statement.DateOfTransaction);
        }

        [Theory]
        [InlineData("Male")]
        [InlineData("Female")]
        [InlineData("Others")]
        public void BankUserDetails_ValidGender_AcceptsCorrectValues(string gender)
        {
            // Arrange & Act
            var bankUser = new BankUserDetails { Gender = gender };

            // Assert
            Assert.Equal(gender, bankUser.Gender);
        }

        [Theory]
        [InlineData("Credit")]
        [InlineData("Debit")]
        [InlineData("Transfer")]
        public void BankStatement_ValidTransactionType_AcceptsCorrectValues(string transactionType)
        {
            // Arrange & Act
            var statement = new BankStatement { TransactionType = transactionType };

            // Assert
            Assert.Equal(transactionType, statement.TransactionType);
        }

        [Theory]
        [InlineData("pending")]
        [InlineData("active")]
        [InlineData("inactive")]
        public void BankUserDetails_ValidStatus_AcceptsCorrectValues(string status)
        {
            // Arrange & Act
            var bankUser = new BankUserDetails { Status = status };

            // Assert
            Assert.Equal(status, bankUser.Status);
        }

        [Fact]
        public void BankUserDetails_DefaultValues_AreSetCorrectly()
        {
            // Arrange & Act
            var bankUser = new BankUserDetails();

            // Assert
            Assert.Equal(0, bankUser.Id);
            Assert.Null(bankUser.Name);
            Assert.Null(bankUser.EmailId);
            Assert.Equal(0, bankUser.MobileNumber);
            Assert.Equal(0, bankUser.Amount);
        }

        [Fact]
        public void BankStatement_DefaultValues_AreSetCorrectly()
        {
            // Arrange & Act
            var statement = new BankStatement();

            // Assert
            
            Assert.Equal(0, statement.UserId);
            Assert.Equal(0, statement.TransactionAmount);
            Assert.Equal(0, statement.BalanceAmount);
            Assert.Null(statement.TransactionType);
        }
    }
}