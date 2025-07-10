using System;
using Xunit;
using Bank.Exception;

namespace Bank.Test
{
    public class ExceptionTests
    {
        [Fact]
        public void BankUserException_WithMessage_CreatesCorrectly()
        {
            // Arrange
            string expectedMessage = "Invalid user data";

            // Act
            var exception = new BankUserException(expectedMessage);

            // Assert
            Assert.Equal(expectedMessage, exception.Message);
            Assert.IsType<BankUserException>(exception);
        }

        [Fact]
        public void AdminException_WithMessage_CreatesCorrectly()
        {
            // Arrange
            string expectedMessage = "Admin access denied";

            // Act
            var exception = new AdminException(expectedMessage);

            // Assert
            Assert.Equal(expectedMessage, exception.Message);
            Assert.IsType<AdminException>(exception);
        }

        [Fact]
        public void BankUserException_HasMsgProperty_SetsCorrectly()
        {
            // Arrange
            string expectedMessage = "Test message";

            // Act
            var exception = new BankUserException(expectedMessage);

            // Assert
            Assert.Equal(expectedMessage, exception.Msg);
        }

        [Fact]
        public void AdminException_HasMsgProperty_SetsCorrectly()
        {
            // Arrange
            string expectedMessage = "Admin test message";

            // Act
            var exception = new AdminException(expectedMessage);

            // Assert
            Assert.Equal(expectedMessage, exception.Msg);
        }

        [Theory]
        [InlineData("Insufficient balance")]
        [InlineData("Invalid email format")]
        [InlineData("User not found")]
        public void BankUserException_DifferentMessages_CreatesCorrectly(string message)
        {
            // Act
            var exception = new BankUserException(message);

            // Assert
            Assert.Equal(message, exception.Message);
        }

        [Theory]
        [InlineData("Invalid credentials")]
        [InlineData("Access denied")]
        [InlineData("Admin not found")]
        public void AdminException_DifferentMessages_CreatesCorrectly(string message)
        {
            // Act
            var exception = new AdminException(message);

            // Assert
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void BankUserException_InheritsFromFormatException()
        {
            // Act
            var exception = new BankUserException("Test message");

            // Assert
            Assert.IsAssignableFrom<FormatException>(exception);
            Assert.IsAssignableFrom<System.Exception>(exception);
        }

        [Fact]
        public void AdminException_InheritsFromFormatException()
        {
            // Act
            var exception = new AdminException("Test message");

            // Assert
            Assert.IsAssignableFrom<FormatException>(exception);
            Assert.IsAssignableFrom<System.Exception>(exception);
        }
    }
}