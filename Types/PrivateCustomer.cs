namespace Mette_miniBank.Types
{

    // Private banking består af high-net-worth klienter i banken. 
    // jeg skal også have defineret en RetailCustomer, som er en PersonalCustomer, dvs low-end. 


    // Klasse for private kunder defineres. 
    // Private kunder har et cpr nummer. 
    // - Efternavn nedarves fra basen ** men bør flyttes og kun bruges i PrivateCustomer
    // - Kundeid nedarves fra basen


    internal class PrivateCustomer(string lastName, int customerID, string cprNumber) : CustomerBase(lastName, customerID) //Inheriteance og initialisering af felter fra basen
    {
        public string CprNumber { get; } = cprNumber;  // jeg erklærer cprnumber og sætter værdien cprNumber i ét hug. 
    }
}
