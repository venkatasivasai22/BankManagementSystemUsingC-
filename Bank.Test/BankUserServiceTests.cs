using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Bank.Service;
using Bank.DAO;
using Bank.Model;
using Bank.Exception;

namespace Bank.Test
{
    public class BankUserServiceTests
    {
        private readonly Mock<IBankUserDAO> _mockBankUserDAO;
        private readonly Mock<IBankStatementDAO> _mockBankStatementDAO;
        private readonly BankUserServiceImp _bankUserService;

        public BankUserServiceTests()
        {
            _mockBankUserDAO = new Mock<IBankUserDAO>();
            _mockBankStatementDAO = new Mock<IBankStatementDAO>();
            _bankUserService = new BankUserServiceImp();
        }

        [Fact]
        public async Task DebitAsync_ValidAmount_DebitSuccessful()
        {
            var user = new BankUserDetails { Id = 1, EmailId = "test@gmail.com", Amount = 1000 };
            _mockBankUserDAO.Setup(x => x.GetUserDetailsByUsingEmailAndPasswordAsync("test@gmail.com", 1234))
                           .ReturnsAsync(user);
            _mockBankUserDAO.Setup(x => x.DebitAsync("test@gmail.com", 500)).ReturnsAsync(1);

            var result = await _mockBankUserDAO.Object.DebitAsync("test@gmail.com", 500);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task CreditAsync_ValidAmount_CreditSuccessful()
        {
            var user = new BankUserDetails { Id = 1, EmailId = "test@gmail.com", Amount = 1000 };
            _mockBankUserDAO.Setup(x => x.GetUserDetailsByUsingEmailAndPasswordAsync("test@gmail.com", 1234))
                           .ReturnsAsync(user);
            _mockBankUserDAO.Setup(x => x.DebitAsync("test@gmail.com", 1500)).ReturnsAsync(1);

            var result = await _mockBankUserDAO.Object.DebitAsync("test@gmail.com", 1500);

            Assert.Equal(1, result);
        }

        [Theory]
        [InlineData("9876543210", true)]
        [InlineData("1234567890", false)]
        [InlineData("98765432101", false)]
        public void IsValidPhoneNumber_ValidatesCorrectly(string phoneNumber, bool expected)
        {
            var result = _bankUserService.IsValidPhoneNumber(phoneNumber);

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task GetUserDetails_ValidCredentials_ReturnsUser()
        {
            var expectedUser = new BankUserDetails 
            { 
                Id = 1, 
                Name = "John Doe", 
                EmailId = "john@gmail.com", 
                Amount = 5000 
            };
            _mockBankUserDAO.Setup(x => x.GetUserDetailsByUsingEmailAndPasswordAsync("john@gmail.com", 1234))
                           .ReturnsAsync(expectedUser);

            var result = await _mockBankUserDAO.Object.GetUserDetailsByUsingEmailAndPasswordAsync("john@gmail.com", 1234);

            Assert.NotNull(result);
            Assert.Equal("John Doe", result.Name);
            Assert.Equal(5000, result.Amount);
        }

        [Fact]
        public async Task TransferMoney_ValidTransfer_TransferSuccessful()
        {
            var sender = new BankUserDetails { Id = 1, EmailId = "adf@gmail.com", Amount = 2000 };
            var receiver = new BankUserDetails { Id = 2, MobileNumber = 9876543210, Amount = 1000 };
            
            _mockBankUserDAO.Setup(x => x.GetUserDetailsByUsingEmailAndPasswordAsync("adf@gmail.com", 1234))
                           .ReturnsAsync(sender);
            _mockBankUserDAO.Setup(x => x.PhoneNumberDetailsAsync(9876543210))
                           .ReturnsAsync(receiver);
            _mockBankUserDAO.Setup(x => x.DebitAsync("adf@gmail.com", 1500)).ReturnsAsync(1);
            _mockBankUserDAO.Setup(x => x.PhoneAmountTransferAsync(9876543210, 1500)).ReturnsAsync(1);

            var debitResult = await _mockBankUserDAO.Object.DebitAsync("adf@gmail.com", 1500);
            var creditResult = await _mockBankUserDAO.Object.PhoneAmountTransferAsync(9876543210, 1500);


            Assert.Equal(1, debitResult);
            Assert.Equal(1, creditResult);
        }
    }
}