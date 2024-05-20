using Mette_miniBank.Types;


// repository hvor der defineres metoder til account, som henter data.

namespace Mette_miniBank.Repositories  //så vidt muligt kun hente data gennem metoder, og der for 
{
    internal class TransactionRepository  //konti oprettes internt og kan kun tilgås via metode: encapsulation
    {
        private List<Transaction> transactions = new List<Transaction>();
        private int latestTransactionId = 0; // med en database løsning vil dette id skabes i databasen

        public void AddTransaction(Transaction transaction)
        {
            transactions.Add(transaction);
        }

        public List<Transaction> GetTransactions(int accountId)
        {
            return transactions.Where(t => t.FromAccountID == accountId || t.ToAccountID == accountId).ToList();
        }

        public int GetNextTransactionId() 
        {
            return latestTransactionId++;
        }

    }
}
