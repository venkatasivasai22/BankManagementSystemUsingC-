using System;
using System.Threading;
using System.Threading.Tasks;
using Bank.DAO;
using Bank.Model;

namespace Bank.Service
{
    public class ThreadSafeBankService
    {
        private readonly IBankUserDAO _bankUserDAO;
        private readonly TransactionManager _transactionManager;
        private readonly SemaphoreSlim _balanceUpdateSemaphore;
        private readonly ReaderWriterLockSlim _userDataLock;

        public ThreadSafeBankService()
        {
            _bankUserDAO = new BankUserDAOImplementation();
            _transactionManager = new TransactionManager();
            _balanceUpdateSemaphore = new SemaphoreSlim(1, 1);
            _userDataLock = new ReaderWriterLockSlim();
        }

        public async Task<bool> TransferMoneyAsync(string senderEmail, long receiverPhone, double amount)
        {
            return await _transactionManager.ExecuteInTransactionAsync(async (connection, transaction) =>
            {
                await _balanceUpdateSemaphore.WaitAsync().ConfigureAwait(false);
                
                try
                {
                    // Get sender details with read lock
                    _userDataLock.EnterReadLock();
                    BankUserDetails sender, receiver;
                    
                    try
                    {
                        sender = await _bankUserDAO.GetUserDetailsByUsingEmailAndPasswordAsync(senderEmail, 0).ConfigureAwait(false);
                        receiver = await _bankUserDAO.PhoneNumberDetailsAsync(receiverPhone).ConfigureAwait(false);
                    }
                    finally
                    {
                        _userDataLock.ExitReadLock();
                    }

                    if (sender == null || receiver == null || sender.Amount < amount)
                        return false;

                    // Update balances with write lock
                    _userDataLock.EnterWriteLock();
                    
                    try
                    {
                        double senderBalance = sender.Amount - amount;
                        double receiverBalance = receiver.Amount + amount;

                        int debitResult = await _bankUserDAO.DebitAsync(senderEmail, senderBalance).ConfigureAwait(false);
                        int creditResult = await _bankUserDAO.PhoneAmountTransferAsync(receiverPhone, receiverBalance).ConfigureAwait(false);

                        return debitResult > 0 && creditResult > 0;
                    }
                    finally
                    {
                        _userDataLock.ExitWriteLock();
                    }
                }
                finally
                {
                    _balanceUpdateSemaphore.Release();
                }
            }).ConfigureAwait(false);
        }

        public void Dispose()
        {
            _balanceUpdateSemaphore?.Dispose();
            _userDataLock?.Dispose();
        }
    }
}