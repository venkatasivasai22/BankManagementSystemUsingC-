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
    public class AdminServiceTests
    {
        private readonly Mock<IAdminDAO> _mockAdminDAO;
        private readonly Mock<IBankUserDAO> _mockBankUserDAO;
        private readonly AdminServiceImp _adminService;

        public AdminServiceTests()
        {
            _mockAdminDAO = new Mock<IAdminDAO>();
            _mockBankUserDAO = new Mock<IBankUserDAO>();
            _adminService = new AdminServiceImp(_mockAdminDAO.Object, _mockBankUserDAO.Object);
        }

        [Fact]
        public async Task AcceptUserRequest_ValidUser_UpdatesSuccessfully()
        {
            var bankUser = new BankUserDetails
            {
                Id = 1,
                Name = "John Doe",
                EmailId = "john@gmail.com",
                Status = "pending"
            };

            _mockBankUserDAO.Setup(x => x.UpdatePinAndAccountNumberAsync(It.IsAny<int>(), It.IsAny<int>(), bankUser.Id))
                           .ReturnsAsync(1);

            await _adminService.AcceptUserRequestAsync(bankUser);

            _mockBankUserDAO.Verify(x => x.UpdatePinAndAccountNumberAsync(It.IsAny<int>(), It.IsAny<int>(), bankUser.Id), Times.Once);
        }

        [Fact]
        public async Task RejectUserRequest_ValidUser_DeletesSuccessfully()
        {
            var bankUser = new BankUserDetails
            {
                Id = 1,
                Name = "John Doe",
                EmailId = "john@gmail.com",
                Status = "pending"
            };

            _mockBankUserDAO.Setup(x => x.DeleteAsync(bankUser.Id)).ReturnsAsync(1);

            await _adminService.RejectUserRequestAsync(bankUser);

            
            _mockBankUserDAO.Verify(x => x.DeleteAsync(bankUser.Id), Times.Once);
        }

        [Theory]
        [InlineData("admin@bank.com", "admin123", true)]
        [InlineData("invalid@bank.com", "wrong", false)]
        [InlineData("", "", false)]
        public async Task AdminLogin_ValidatesCredentials(string email, string password, bool expectedResult)
        {
            
            _mockAdminDAO.Setup(x => x.GetAdminDetailsByEmailAndPasswordAsync(email, password))
                        .ReturnsAsync(expectedResult);

            var result = await _mockAdminDAO.Object.GetAdminDetailsByEmailAndPasswordAsync(email, password);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task AllUserDetails_ReturnsAllUsers()
        {
            var userList = new List<BankUserDetails>
            {
                new BankUserDetails { Id = 1, Name = "User1", EmailId = "user1@gmail.com", Status = "active" },
                new BankUserDetails { Id = 2, Name = "User2", EmailId = "user2@gmail.com", Status = "pending" }
            };

            _mockBankUserDAO.Setup(x => x.SelectAllBankUserDetailsAsync()).ReturnsAsync(userList);

            var result = await _mockBankUserDAO.Object.SelectAllBankUserDetailsAsync();

            Assert.Equal(2, result.Count);
            Assert.Equal("User1", result[0].Name);
            Assert.Equal("User2", result[1].Name);
        }

        [Fact]
        public async Task AcceptUserRequest_FailedUpdate_ThrowsException()
        {
            var bankUser = new BankUserDetails { Id = 1, Name = "John Doe" };
            _mockBankUserDAO.Setup(x => x.UpdatePinAndAccountNumberAsync(It.IsAny<int>(), It.IsAny<int>(), bankUser.Id))
                           .ReturnsAsync(0);

            await Assert.ThrowsAsync<AdminException>(() => _adminService.AcceptUserRequestAsync(bankUser));
        }

        [Fact]
        public async Task RejectUserRequest_FailedDelete_ThrowsException()
        {
            var bankUser = new BankUserDetails { Id = 1, Name = "John Doe" };
            _mockBankUserDAO.Setup(x => x.DeleteAsync(bankUser.Id)).ReturnsAsync(0);

            // Act & Assert
            await Assert.ThrowsAsync<AdminException>(() => _adminService.RejectUserRequestAsync(bankUser));
        }
    }
}