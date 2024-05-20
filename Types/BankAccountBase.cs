namespace Mette_miniBank.Types
{
    // balance = saldo og bruges på konto-klassen her
    // amount  = beløb og bruges på transaktionen, - dvs skal bruges i transaktionsklassen

    // En base klasse for konti defineres
    // Værdier der vil arves af alle sub-kunde-klasser
    // Konto id
    // Oprettelsesdato
    // Valutatype
    // subclasses til denne base: SavingsAccount og  CheckingAccount (- og senere en investeringskonto.) 

    //SavingsAccount has an additional attribute interest_rate, representing the interest rate applicable to the savings account.
    //CheckingAccount has an additional attribute overdraft_limit, representing the maximum negative balance allowed (overdraft limit).


    // ** husk at sætte en Balance = initialBalance når jeg hardkoder en account
    // *? * hvordan laver jeg forskellige valutakurser - e.g. 1 Euro = 7,15 DKK

    internal abstract class BankAccountBase
    {
        //Kontruktør: 
        protected BankAccountBase
                        (int        accountID, 
                         DateTimeOffset   registrationDate, 
                         int     customerID, 
                         Currency   accountCurrency, 
                         decimal    accountBalance)
        {
            // **  NB - AccountholderID skal defineres og sættes lig med CustomerId, som hentes fra enten klassen Private eller Business som har arvet fra basen
            AccountID = accountID; //propertien LastName sættes lig med konstruktør parameter lastName  
            
            RegistrationDate = registrationDate;
            CustomerID = customerID;
            AccountCurrency = accountCurrency;
            AccountBalance = accountBalance;  

        }

                
        public int AccountID { get; }  //Unik nummer for konto. Intet "set", da AccountID aldrig skal ændres, public da AccountID skal kunne sættes i et object udenfor klassen.
        public DateTimeOffset RegistrationDate { get; } //properties erklæres for at man kan tilgå dem på objektet
        public int CustomerID { get; } // AccountHolderID skal sættes lig med Kunde-id for kontoindehaveren, - bankkundens kundeid
        public Currency AccountCurrency { get; } // Kontoens valuta-type 
        public decimal AccountBalance { get; set; } // Balancen på kontoen, Balance = Saldo, dvs det aktuelle beløb, der er til rådighed på kontoen efter at have taget højde for indskud, udbetalinger og andre transaktioner.
    }
}
