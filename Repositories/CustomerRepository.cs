using Mette_miniBank.Types;


namespace Mette_miniBank.Repositories  // generelt så vidt muligt kun hente data gennem metoder, disse metoder lægges  her i repository
{
    internal class CustomerRepository  //kunder oprettes internt og kan kun tilgås via metode, her benyttes altså indkapsling (eng. :  encapsulation)
    {
        private List<PrivateCustomer> privateCustomers = new List<PrivateCustomer>();  //listen er kun kendt inden for klassen/objektet

        public CustomerRepository()   //construtoren er public, det skal den altid være ellers kan andre dele af koden ikke oprette )
        {

            // Da data for kunder  ikke ligger i fil eller i en database (endnu), oprettes kunderne her
            // her kunne intialsiering af objekterne laves mere elegant se side 385 i bogen

            PrivateCustomer mette = new PrivateCustomer("Berg", 2, "123456-1234");
            privateCustomers.Add(mette);

            PrivateCustomer freja = new PrivateCustomer("Fabrin", 3, "654321-1238"); // variabel navne altid camelCase
            privateCustomers.Add(freja);

            PrivateCustomer Maja = new PrivateCustomer("Schrøder", 4, "6543871-1236"); // variabel navne altid camelCase
            privateCustomers.Add(Maja);
        }

        // Metode til at hente kunderne
        public List<PrivateCustomer> GetPrivateCustomers()
        {
            return privateCustomers;
        }

        public PrivateCustomer? GetPrivateCustomer(int customerId)  //metode for at finde og returnere det objekt der indeholder det customer id, som er input til metoden. 
        {
            foreach(var customer in privateCustomers)
            {
                if(customer.CustomerID == customerId)
                {
                    return customer;  //Returnerer hele det fundne objekt med alle værdier
                }
            }

            return null;  // hvis vi ikke kan finde den kunde, returner null
        }
    }
}
