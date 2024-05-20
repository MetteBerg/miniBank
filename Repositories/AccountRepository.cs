using Mette_miniBank.Types;

// repository hvor der defineres metoder til account, som henter data.

namespace Mette_miniBank.Repositories  //så vidt muligt kun hente data gennem metoder
{
    internal class AccountRepository  //konti oprettes internt og kan kun tilgås via metoder i reposoitory: encapsulation
    {
        private List<CheckingAccount> checkingsAccount = new List<CheckingAccount>();  //Initialisering af liste. Listen er kun kendt inden for klassen/objektet


        // Oprettelse af konti. Disse konti skal senere - når database introduceres, - hentes fra database.
        public AccountRepository()   //construtoren er public, det skal den altid være ellers kan andre dele af koden ikke oprette )
        {
            // Da data for konti ikke ligger i fil eller i en database (endnu), oprettes her konti

            CheckingAccount mette1 = new CheckingAccount(10000, 1001, DateTimeOffset.UtcNow, 2, Currency.DKK, 5000);
            checkingsAccount.Add(mette1);

            CheckingAccount mette2 = new CheckingAccount(20000, 1002, DateTimeOffset.UtcNow, 2, Currency.DKK, 3000);
            checkingsAccount.Add(mette2);

            CheckingAccount freja1 = new CheckingAccount(10000, 1003, DateTimeOffset.UtcNow, 3, Currency.EUR, 6000);
            checkingsAccount.Add(freja1);

            CheckingAccount freja2 = new CheckingAccount(20000, 1004, DateTimeOffset.UtcNow, 3, Currency.EUR, 2000);
            checkingsAccount.Add(freja2);
        }

        // Metode til at hente konti
        public List<CheckingAccount> GetCheckingAccounts()
        {
            return checkingsAccount;
        }

        // Metode til at hente konti for en specifik konto
        public List<CheckingAccount> GetCheckingAccounts(int customerId)    // overload betyder at flere metoder har samme returværdi og samme navn , men forskellige paramtere
        {
            List<CheckingAccount> customerAccounts = new List<CheckingAccount>();

            foreach (var account in checkingsAccount)
            {
                if (account.CustomerID == customerId)
                {
                    customerAccounts.Add(account);  // Hvis kontoen tilhører kunden vi beder om, så tilføj til listen som vi ender med at returnere
                }
            }

            return customerAccounts;
        }

        public CheckingAccount? GetCheckingAccount(int accountId)
        {
            foreach(var account in checkingsAccount)
            {
                if(account.AccountID == accountId)
                {
                    return account;  //find det objekt der indeholder konto id og returner hele objektet med alle værdier
                }
            }

            return null;  // hvis vi ikke kan finde den konto, returner null
        }
    }
}
