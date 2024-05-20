using Mette_miniBank.Interfaces;
using Mette_miniBank.Types;

namespace Mette_miniBank.Types
{
    internal class CheckingAccount : BankAccountBase, ITransaction  //klassen defineres som at den nedarver fra BankAccountBase og imlementerer interfacet IAccount
    {
        //konstruktør
        public CheckingAccount(decimal overdraftLimit, 
            int accountID, 
            DateTimeOffset registrationDate, 
            int customerID, 
            Currency accountCurrency, 
            decimal accountBalance) : base(accountID, registrationDate, customerID, accountCurrency, accountBalance)
        {
            OverdraftLimit = overdraftLimit;
        }

        // Her implementerer jeg deposit metoden som er defineret i IAccount interfacet.
        // Jeg ønsker på sigt at der måske skal pålægges et specielt gebyr for chekking accounts ved deposit,
        // og denne særlige regel for chekkingaccount vil så afspejle sige nedenfor, udover at saldoen naturligvis tillægges det indsatte beløb.
        public void Deposit(decimal amount)
        {
            // læg indtastet beløb til saldo (eng: amount added to balance)
            AccountBalance += amount;  //properti, - en property er en værdi fra min klasse som jeg viser omverdenen,  og her kan den også ændres, da "set"
        }

        public void Withdraw(decimal amount)
        {
            AccountBalance -= amount;
        }

        public int GetAccountId()
        {
            return AccountID;
        }

        //
        public decimal OverdraftLimit { get; } //properti på objektet. Der er et minimumsbeløb der skal indsættes på en savingsaccount. 
    }

}
