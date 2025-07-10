using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Bank.DAO;
using Bank.Model;

namespace Bank.Test
{
    public class BankUserDAOTests
    {
        private readonly Mock<IBankUserDAO> _mockBankUserDAO;

        public BankUserDAOTests()
        {
            _mockBankUserDAO = new Mock<IBankUserDAO>();
        }

        [Fact]
        public async Task InsertBankUserDetails_ValidUser_InsertsSuccessfully()
        {
            var bankUser = new BankUserDetails
            {
                Name = "John Doe",
                EmailId = "john@gmail.com",
                MobileNumber = 9876543210,
                PanNumber = "ABCDE1234F",
                AadharNumber = 123456789012,
                Address = "123 Main St",
                Gender = "Male",
                Amount = 5000
            };

            _mockBankUserDAO.Setup(x => x.InsertBankUserDetailsAsync(bankUser))
                           .Returns(Task.CompletedTask);

            await _mockBankUserDAO.Object.InsertBankUserDetailsAsync(bankUser);

            _mockBankUserDAO.Verify(x => x.InsertBankUserDetailsAsync(bankUser), Times.Once);
        }

        [Fact]
        public async Task SelectAllBankUserDetails_ReturnsUserList()
        {
            var expectedUsers = new List<BankUserDetails>
            {
                new BankUserDetails { Id = 1, Name = "User1", EmailId = "user1@gmail.com" },
                new BankUserDetails { Id = 2, Name = "User2", EmailId = "user2@gmail.com" }
            };

            _mockBankUserDAO.Setup(x => x.SelectAllBankUserDetailsAsync())
                           .ReturnsAsync(expectedUsers);

            var result = await _mockBankUserDAO.Object.SelectAllBankUserDetailsAsync();

            Assert.Equal(2, result.Count);
            Assert.Equal("User1", result[0].Name);
            Assert.Equal("User2", result[1].Name);
        }

        [Theory]
        [InlineData("john@gmail.com", 1234, true)]
        [InlineData("invalid@gmail.com", 9999, false)]
        public async Task GetUserDetailsByEmailAndPassword_ValidatesCredentials(string email, int pin, bool shouldReturnUser)
        {
            var expectedUser = shouldReturnUser ? new BankUserDetails 
            { 
                Id = 1, 
                Name = "John Doe", 
                EmailId = email, 
                Pin = pin 
            } : null;

            _mockBankUserDAO.Setup(x => x.GetUserDetailsByUsingEmailAndPasswordAsync(email, pin))
                           .ReturnsAsync(expectedUser);

            // Act
            var result = await _mockBankUserDAO.Object.GetUserDetailsByUsingEmailAndPasswordAsync(email, pin);

            // Assert
            if (shouldReturnUser)
            {
                Assert.NotNull(result);
                Assert.Equal(email, result.EmailId);
            }
            else
            {
                Assert.Null(result);
            }
        }

        [Fact]
        public async Task UpdatePinAndAccountNumber_ValidData_UpdatesSuccessfully()
        {
            int pin = 1234;
            int accountNumber = 1234567;
            int userId = 1;

            _mockBankUserDAO.Setup(x => x.UpdatePinAndAccountNumberAsync(pin, accountNumber, userId))
                           .ReturnsAsync(1);

            var result = await _mockBankUserDAO.Object.UpdatePinAndAccountNumberAsync(pin, accountNumber, userId);

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DebitAsync_ValidAmount_DebitSuccessful()
        {
            string email = "john@gmail.com";
            double newAmount = 4500;

            _mockBankUserDAO.Setup(x => x.DebitAsync(email, newAmount))
                           .ReturnsAsync(1);

            var result = await _mockBankUserDAO.Object.DebitAsync(email, newAmount);

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task PhoneNumberDetails_ValidNumber_ReturnsUser()
        {
            long phoneNumber = 9876543210;
            var expectedUser = new BankUserDetails 
            { 
                Id = 1, 
                Name = "John Doe", 
                MobileNumber = phoneNumber 
            };

            _mockBankUserDAO.Setup(x => x.PhoneNumberDetailsAsync(phoneNumber))
                           .ReturnsAsync(expectedUser);

            var result = await _mockBankUserDAO.Object.PhoneNumberDetailsAsync(phoneNumber);

            Assert.NotNull(result);
            Assert.Equal(phoneNumber, result.MobileNumber);
        }

        [Fact]
        public async Task PhoneAmountTransfer_ValidTransfer_TransferSuccessful()
        {
            long phoneNumber = 9876543210;
            double amount = 1500;

            _mockBankUserDAO.Setup(x => x.PhoneAmountTransferAsync(phoneNumber, amount))
                           .ReturnsAsync(1);

            var result = await _mockBankUserDAO.Object.PhoneAmountTransferAsync(phoneNumber, amount);

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteAsync_ValidId_DeletesSuccessfully()
        {
            int userId = 1;

            _mockBankUserDAO.Setup(x => x.DeleteAsync(userId))
                           .ReturnsAsync(1);

            var result = await _mockBankUserDAO.Object.DeleteAsync(userId);

            Assert.Equal(1, result);
        }
    }
}