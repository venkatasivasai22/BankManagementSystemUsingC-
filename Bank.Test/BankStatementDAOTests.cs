using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Bank.DAO;
using Bank.Model;

namespace Bank.Test
{
    public class BankStatementDAOTests
    {
        private readonly Mock<IBankStatementDAO> _mockBankStatementDAO;

        public BankStatementDAOTests()
        {
            _mockBankStatementDAO = new Mock<IBankStatementDAO>();
        }

        [Fact]
        public async Task InsertStatementDetails_ValidStatement_ReturnsSuccess()
        {
            var bankStatement = new BankStatement
            {
                UserId = 1,
                TransactionAmount = 500,
                BalanceAmount = 4500,
                TransactionType = "Debit",
                DateOfTransaction = DateTime.Now.Date,
                TimeOfTransaction = DateTime.Now.TimeOfDay
            };

            _mockBankStatementDAO.Setup(x => x.InsertStatementDetailsAsync(bankStatement))
                                .ReturnsAsync(1);

            var result = await _mockBankStatementDAO.Object.InsertStatementDetailsAsync(bankStatement);

            Assert.Equal(1, result);
            _mockBankStatementDAO.Verify(x => x.InsertStatementDetailsAsync(bankStatement), Times.Once);
        }

        [Theory]
        [InlineData("Credit", 1000, 5000)]
        [InlineData("Debit", 500, 4500)]
        [InlineData("Transfer", 200, 4800)]
        public async Task InsertStatement_DifferentTransactionTypes_InsertsCorrectly(string transactionType, double transactionAmount, double balanceAmount)
        {
            var bankStatement = new BankStatement
            {
                UserId = 1,
                TransactionAmount = transactionAmount,
                BalanceAmount = balanceAmount,
                TransactionType = transactionType,
                DateOfTransaction = DateTime.Now.Date,
                TimeOfTransaction = DateTime.Now.TimeOfDay
            };

            _mockBankStatementDAO.Setup(x => x.InsertStatementDetailsAsync(It.Is<BankStatement>(
                s => s.TransactionType == transactionType && 
                     s.TransactionAmount == transactionAmount &&
                     s.BalanceAmount == balanceAmount)))
                                .ReturnsAsync(1);

            var result = await _mockBankStatementDAO.Object.InsertStatementDetailsAsync(bankStatement);

            Assert.Equal(1, result);
            _mockBankStatementDAO.Verify(x => x.InsertStatementDetailsAsync(It.Is<BankStatement>(
                s => s.TransactionType == transactionType)), Times.Once);
        }

        [Fact]
        public async Task InsertStatementDetails_InvalidStatement_ReturnsFailure()
        {
            
            var bankStatement = new BankStatement
            {
                UserId = 0, 
                TransactionAmount = -100, 
                TransactionType = "Invalid"
            };

            _mockBankStatementDAO.Setup(x => x.InsertStatementDetailsAsync(bankStatement))
                                .ReturnsAsync(0);

            var result = await _mockBankStatementDAO.Object.InsertStatementDetailsAsync(bankStatement);

            Assert.Equal(0, result);
        }
    }
}