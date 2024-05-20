using Mette_miniBank.Interfaces;
using Mette_miniBank.Types;
using Mette_miniBank.Repositories;

namespace Mette_miniBank.Engines
{
    internal class TransactionEngine(TransactionRepository transactionRepository)
    {

        public void DepositToAccount(ITransaction account, decimal amount)  // metoden tager en konto som implemeneter interfacet ITransaction
        {
            account.Deposit(amount);

            // Gem transaktionen i listen af transakationer
            Transaction transaction = new(amount, transactionRepository.GetNextTransactionId(), DateTime.UtcNow, null, account.GetAccountId());
            transactionRepository.AddTransaction(transaction);
        }

        public void WithdrawFromAccount(ITransaction account, decimal amount)  // metoden tager en konto som implemeneter interfacet ITransaction
        {
            account.Withdraw(amount);

            // Gem transaktionen i listen af transkationer
            Transaction transaction = new(amount * -1, transactionRepository.GetNextTransactionId(), DateTime.UtcNow, null, account.GetAccountId());
            transactionRepository.AddTransaction(transaction);
        }

        public void TransferToAccount(ITransaction fromAccount, ITransaction toAccount, decimal amount)
        {
            fromAccount.Withdraw(amount);
            toAccount.Deposit(amount);

            // Gem transaktionen i listen af transaktioner
            Transaction transaction = new(amount, transactionRepository.GetNextTransactionId(), DateTime.UtcNow, fromAccount.GetAccountId(), toAccount.GetAccountId());
            transactionRepository.AddTransaction(transaction);
        }

        // Implementer metoder til at 1. Hente alle transaktioner, 2. hente alle transaktioner for en kunde, 3. hente alle transkationer for en konto
    }
}
