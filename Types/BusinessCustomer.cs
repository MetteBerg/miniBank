namespace Mette_miniBank.Types
{

    // Klasse for virksomhedskunder defineres. 
    // Virksomhedskunder har et cvr nummer. 
    // - Efternavn nedarves fra basen - lastName er for business kunder company name.
    // - Kundeid nedarves fra basen
    
    // **NB - lastName skal fjernes fra CustomerBase - erstattes af businessName i BusinessCutomer og lastName i PrivateCustomer

    internal class BusinessCustomer(string lastName, int customerID, string cvrNumber, string businessName) : CustomerBase(lastName, customerID)
    {
        public string CvrNumber { get; } = cvrNumber;  // jeg erklærer CVR-nummer og sætter værdien cvrNumber på én gang. 
        public string BusinessName { get; } = businessName;
    }
}
