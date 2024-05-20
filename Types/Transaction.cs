

using static System.TimeZoneInfo;

namespace Mette_miniBank.Types
{
    internal class Transaction
    {
        // Alle vores properties har kun getters, da vi ønsker at en transaction er immutable, altså ikke kan ændres efter oprettelse
        public int TransactionID { get; }
        public decimal Amount { get; }  // beløb der skal overføres
        public DateTime TransactionTime { get; }
        public int? FromAccountID { get; }   // ** nb - hvis manuel indbetaling - så skal der være en markering
        public int ToAccountID { get; }      // ** nb - hvis manuel udbetaling, - så skal der være en markering.....
        //Currency Currency { get; } // currency er ikk eunderstøttet endnu

        public Transaction
                (decimal amount,
                 int transactionID,
                 DateTime transactionTime,
                 int? fromAccountID,  //nullabale, hvis ikke nogen konto påskrevet, så er det penge der er sat ind. 
                 int toAccountID
                 //Currency currency //- valutatypen på kontoen skal være input - og ved manuel indsætning skal valuta indgives udover beløb
                 )

        {

            TransactionID = transactionID;
            Amount = amount;  // beløb der skal overføres
            TransactionTime = transactionTime;
            FromAccountID = fromAccountID;   // ** nb - hvis manuel indbetaling - så skal der være en markering
            ToAccountID = toAccountID;      // ** nb - hvis manuel udbetaling, - så skal der være en markering......
        }
    }
}

