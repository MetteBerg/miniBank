namespace Mette_miniBank.Types
{

    // En base klasse for kunder defineres. 
    // Værdier der vil arves af alle sub-kunde-klasser
    // Kundeid.
    // Registeringsdato - skal tilføjes.
    // Efternavn. (ønsker dog at dette felt flyttes til PrivatKonto
    // Jeg definerer derefter to sub-klasser, for private kunder og for virksomhedskunder i deres egen cs-fil
    // Dette fordi private kunder kendetegnes ved cpr-nr, og virksomheder ved CVR nummer, og desuden defineres ud fra selskabsform mm , 
    // hvilket gør at virksomheder vil have andre og forskellige variable end private kunder har, 
    // Da kundeid er defineret i base sikres, at der aldrig bliver to ens kundeid´er uanset private eller business.


    internal abstract class CustomerBase   // Jeg ønsker, at der aldrig kan instantieres et object fra CustomerBase, da der så vil mangle felter og gør dermed denne klasse abstrakt
    {
        //Constructor:
        protected CustomerBase(string lastName, int customerID)  //protected, så kan kun tilgås fra klassen selv og sub-klasser.
        {
            LastName = lastName;  //propertien LastName sættes lig med konstruktør parameter lastName  
            // ** ønske LastName skal flyttes til PrivateCustomer, da tilsvarende felt hedder CompanyName for Business Customer
            CustomerID = customerID;
            // ** RegistrationDate = registrationDate;
        }
        // modsat privatecustomer class, så  skiller jeg her erklæring og værdien ad.  To forskellige metoder, der gør det samme. 
        public string LastName { get; set; }  // så man kan ændre efternavn
        public int CustomerID { get; }  //intet "set", da ID aldrig skal ændres.
        // ** public DateTime RegistrationDate { get; }
    }

}
