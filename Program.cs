using Mette_miniBank.Engines;
using Mette_miniBank.Types;
using Mette_miniBank.Repositories; //
using Mette_miniBank.Types;   // i Types er alle klasserne defineret.

namespace Mette_miniBank
{
    internal class Program
    {
        static void Main(string[] args)
        {

            bool exitProgram = false;


            CustomerRepository customerRepository = new CustomerRepository(); // Den statiske liste af kunder oprettes og kan nu tilgås via metode

            AccountRepository accountRepository = new AccountRepository(); // Den statiske liste af konti oprettes og kan nu tilgås via metode

            TransactionRepository transactionRepository = new TransactionRepository();

            TransactionEngine transactionEngine = new TransactionEngine(transactionRepository);

            Console.WriteLine("\n\n  Velkommen til Mette miniBank");

            while (!exitProgram)   //hvis exit program er true kommer jeg ud af løkken, og da der ikke er mere kode tilbage stopper programmet. 
            {
                exitProgram = MainMenu(customerRepository, accountRepository, transactionRepository, transactionEngine); //returnerer en boolean og sætter exitProgram lig den boolean. 
            }
        }

        /// <summary>
        /// Udskriver hovedmenu.
        /// </summary>
        static bool MainMenu(CustomerRepository customerRepository, AccountRepository accountRepository, TransactionRepository transactionRepository, TransactionEngine transactionEngine)
        {
            Console.WriteLine(" \n\n --   Hovedmenu  --\n");

            //Console.WriteLine("  Vælg fra listen:\n");
            Console.WriteLine("  1. Oversigt over kunder");
            Console.WriteLine("  2. Indtast kundenummer for at komme videre til transaktionsmenu");
            // Console.WriteLine("  3. menu til senere brug");
            // Console.WriteLine("  4. menu til senere brug");
            Console.WriteLine("\n  9. Afslut program\n");

            // Indlæs brugerens valg
            Console.Write("  Vælg en handling på hovedmenuen: ");
            string? chooseItemMainMenu = Console.ReadLine();

            // Konverter brugerens valg til et heltal
            int switchMainmenu;
            bool isValidchoice = int.TryParse(chooseItemMainMenu, out switchMainmenu);

            Console.Clear();  //Fjern hovedmenu, så næste menu står øverst.

            // Brugerens valg behandles
            switch (switchMainmenu)
            {
                case 1: //Et overblik over bankens kunder - nu udskrives en liste i konsolvindue over alle kunderne
                    CustomerList(customerRepository);
                    break;
                case 2: // Overblik på en kunde og videre til transaktionsmenu for valg at transaktion
                    PrivateCustomer selectedCustomer = ChooseCustomer(customerRepository, accountRepository); //nb hele objektet privatecutomer tages med
                    //Console.WriteLine($"   Valgte kunde: {selectedCustomer.LastName}");
                    CheckingAccount selectedAccount = ChooseAccount(selectedCustomer, accountRepository);
                    //Console.WriteLine($"   Valgte konto: {selectedAccount.AccountID}, balance={selectedAccount.AccountBalance}");

                    // Så skal en af kundens konto vælges med menunummer og føres over i metode Transaktionsvalg 
                    TransactionMenu(transactionEngine, accountRepository, transactionRepository, selectedCustomer, selectedAccount);  //Menu hvor man kan for valgt konto, med indsætte,hæve, overføre samt liste bevægelse.  
                    break;

                case 9:
                    Console.WriteLine("Programmet afsluttes.");
                    return true;   // når "exit"  bliver sand stopper switchen. Jeg behøver ikke "break", da der returneres
                default:
                    Console.WriteLine("  Ugyldigt valg. Prøv igen.");
                    return false;
            }

            return false;
        }

        static void CustomerList(CustomerRepository customerRepository)
        {
            Console.WriteLine("\n  --  Overblik over alle kunder --  \n ");

            // En liste over navne på kunder.
            // Jeg looper igennem en liste af private kunder, listen returneres fra repository via GetPrivateCustomers()
            // Hvert object i listen er af typen PrivateCustomer og lægges ind i en variabel som hedder privateCustomer (PascalCase for klassenavn, camelCase for variable)
            foreach (PrivateCustomer privateCustomerForList in customerRepository.GetPrivateCustomers())
            {
                Console.WriteLine($"          Kundenavn: {privateCustomerForList.LastName}, Id: {privateCustomerForList.CustomerID}");

            }
            Console.Write("\n  Tast retur for at komme tilbage til hovedmenu: "); //ved at anvende Write og ikke Writeline, bliver cursoren på samme linje
            Console.ReadKey();
            Console.Clear();
        }

        static PrivateCustomer ChooseCustomer(CustomerRepository customerRepository, AccountRepository accountRepository)  
        {
            Console.WriteLine("\n  --  Overblik over konti for valgt kunde -- \n");
            Console.Write("\n  Indtast kundenummer for at få en oversigt over konti og tast retur: ");

            string? chooseItemMainMenu = Console.ReadLine(); // Kundenummer indtastes.
            //overvej om øget læsbarhed dersom ordet "Select" og ikke "choose" benyttes.


            // Konverter brugerens valg til et heltal
            int selectedCustomerId;
            bool isValidchoice = int.TryParse(chooseItemMainMenu, out selectedCustomerId); // test for om der er indtastet et heltal

            if (!isValidchoice)
            {
                Console.WriteLine("  \n Det indtastede kundenummer var ikke korrekt - det skal være et heltal.");
                Console.Write("  \nTast retur for at indtaste kundenummer igen:");
                Console.ReadKey();
                Console.Clear();
                return ChooseCustomer(customerRepository, accountRepository);  //hvis ikke heltal, vis menu igen og returner den valgte kunde
            }

            // Nu leder jeg i listen af privatecustomers efter den kunde som har det indtastede kundenummer
            PrivateCustomer? privateCustomer = customerRepository.GetPrivateCustomer(selectedCustomerId);
            if (privateCustomer == null)
            {
                Console.WriteLine("\n  Der findes ingen kunder på det indtastede nummer. ");
                Console.Write("\n  Tast retur for at indtaste kundenummer igen:  ");  
                Console.ReadKey();
                Console.Clear();
                return ChooseCustomer(customerRepository, accountRepository); ;  //hvis kunde ikke findes så tilbage til hovedmenu
            }

            return privateCustomer;
        }

        static CheckingAccount ChooseAccount(PrivateCustomer privateCustomer, AccountRepository accountRepository)


        {
            // Der indtastes en kunde og derefter listes kundens konti.
            Console.WriteLine($"\n  Følgende kunde er valgt: {privateCustomer.LastName}\n"); 

            List<CheckingAccount> customerAccounts = accountRepository.GetCheckingAccounts(privateCustomer.CustomerID);  // Jeg henter listen af konti og gemmer den i en variabel så jeg kan burge dne mere end en gang

            int accountCounter = 0;
            foreach (CheckingAccount checkingAccountForList in customerAccounts)
            {
                accountCounter++;
                Console.WriteLine($"         {accountCounter}. Kontonummer: {checkingAccountForList.AccountID}, balancen er: {checkingAccountForList.AccountBalance} {checkingAccountForList.AccountCurrency}");
            }
            Console.Write(" \n  Indtast menu-nummer for den konto der ønskes valgt og tast retur: ");
            
            string? chosenAccount = Console.ReadLine(); // Kundenummer indtastes. 

            // Konverter brugerens valg til et heltal
            int selectedAccountListNumber;
            bool isValidKeyType = int.TryParse(chosenAccount, out selectedAccountListNumber); // test for om der er indtastet et heltal

            if (!isValidKeyType)
            {
                Console.Write("\n  Dit valg skal være et tal. Tast retur for at komme tilbage til menu:  ");
                Console.ReadKey();
                Console.Clear();
                return ChooseAccount(privateCustomer, accountRepository);  //hvis ikke heltal så tilbage til hovedmenu
            }

            if (selectedAccountListNumber > accountCounter || selectedAccountListNumber == 0)
            {
                Console.Write("  Det valgte nummer findes ikke på listen. Tast retur for at få muligheder udskrevet igen:");
                Console.ReadKey();
                Console.Clear();
                return ChooseAccount(privateCustomer, accountRepository);
            }

            CheckingAccount selectedAccount = customerAccounts[selectedAccountListNumber - 1]; //array starter med nul, derfor nødt til minus 1
            //Console.Write("hej");
            Console.Clear() ;
            return selectedAccount;
        }

        static void TransactionMenu(TransactionEngine transactionEngine, AccountRepository accountRepository, TransactionRepository transactionRepository, PrivateCustomer customer, CheckingAccount checkingAccount)   // Menu TransaktionsValg med indsætte,hæve, overføre og liste bevægelser
        {
            bool exitTransactionMenu = false;
            while (!exitTransactionMenu)
            {
                                
                Console.WriteLine("\n  -- Transaktionsmenu -- \n");
                
                Console.Write($"\n Det valgte kontonummer er: [{checkingAccount.AccountID}] med saldo på {checkingAccount.AccountBalance} {checkingAccount.AccountCurrency}    \n\n");
                Console.WriteLine("   1. Indsætte");
                Console.WriteLine("   2. Hæve");
                Console.WriteLine("   3. Overføre ");
                Console.WriteLine("   4. Liste over transaktioner ");
                Console.WriteLine("\n   8. Tilbage til hovedmenu \n");

                // Indlæs brugerens valg

                //Console.Write($" Vælg en handling på transaktionsmenuen: ");
              
                Console.Write(" Vælg en handling på transaktionsmenuen: "); //Her kunne med fordel skrives ID på kontoen
                string? chooseItemTransactionMenu = Console.ReadLine();

                // Konverter brugerens valg til et heltal
                int switchTransactionMenu;
                bool isValidchoice = int.TryParse(chooseItemTransactionMenu, out switchTransactionMenu);

                Console.Clear();  //Fjerner transaktionsmenu, så handlinger står øverst.

                // Behandle valgmuligheder for transaktioner her
                switch (switchTransactionMenu)
                {
                    case 1:
                        // Behandl mulighed for at indsætte "kontanter" her
                        Deposit(transactionEngine, checkingAccount);
                        Console.WriteLine($"\n    Ny balance: {checkingAccount.AccountBalance} {checkingAccount.AccountCurrency} ");
                        Console.WriteLine("\n    Tast retur for at komme tilbage til transaktionsmenu.");
                        Console.ReadKey();
                        Console.Clear();

                        break;
                    case 2:
                        // Behandl mulighed  her at hæve "kontanter" her
                        Withdraw(transactionEngine, checkingAccount);
                        Console.WriteLine($"\n  Ny balance: {checkingAccount.AccountBalance} {checkingAccount.AccountCurrency}");
                        Console.WriteLine("\n    Tast retur for at komme tilbage til transaktionsmenu.");
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case 3:
                        // Behandl mulighed  her at overføre mellem to konti
                        Transfer(transactionEngine, accountRepository, customer, checkingAccount);
                        break;

                    case 4:
                        // Behandl mulighed  her at få en oversigt over transaktioner på konto
                        DisplayTransactions(transactionRepository, checkingAccount);
                        break;

                    case 8:
                        // Gå tilbage til hovedmenu
                        //Console.WriteLine("\n  Du føres tilbage til hovedmenu. \n");
                        exitTransactionMenu = true;  // switchen stopper og man går tilbage til hovedmenu, hvor listen for punkter på hovedmenu så vises. 
                        break;

                    default:
                        Console.WriteLine("  Ugyldigt valg. Prøv igen.");
                        break;

                }

                
            }
        }

        static bool Deposit(TransactionEngine transactionEngine, CheckingAccount checkingAccount)
        {
            Console.WriteLine($"\n  Du valgte at indsætte kontanter på kontonummer: [{checkingAccount.AccountID}]");
            Console.WriteLine($"\n    Saldo på kontoen er =  {checkingAccount.AccountBalance} {checkingAccount.AccountCurrency} ");
            Console.Write("\n    Indtast beløb og tryk enter:  ");
            string? amountString = Console.ReadLine();
            bool isValidAmount = decimal.TryParse(amountString, out decimal amount);

            if (!isValidAmount)
            {
                Console.Write("    Du skal indtaste et decimal tal. Indtast retur for at prøve igen: \n");
                Console.ReadKey();
                return Deposit(transactionEngine, checkingAccount);  //hvis ikke gyldigt decimal tal, vis prompt igen
                //finpudsning af program påkrævet her, man kan ikke komme ud af program på dette stadie, hvis der ikke indtastes et decimaltal
            }

            transactionEngine.DepositToAccount(checkingAccount, amount);

            return true;
        }

        static bool Withdraw(TransactionEngine transactionEngine, CheckingAccount checkingAccount)
        {
            Console.WriteLine($"\n  Du valgte at hæve kontanter på konto [{ checkingAccount.AccountID}] med saldo {checkingAccount.AccountBalance}  {checkingAccount.AccountCurrency}");
          
            //Console.WriteLine($"\n  Den valgte konto er: [{checkingAccount.AccountID}], og saldo på kontoen er {checkingAccount.AccountBalance} {checkingAccount.AccountCurrency}");
            Console.Write("\n  Indtast beløb og tryk enter:    ");
            string? amountString = Console.ReadLine();
            bool isValidAmount = decimal.TryParse(amountString, out decimal amount);

            if (!isValidAmount)
            {
                Console.WriteLine("Det skal være et decimal tal.");
                return Withdraw(transactionEngine, checkingAccount);  //hvis ikke gyldigt decimal tal, vis prompt igen
                //finpudsning af program her, man kan ikke komme ud af program her, hvis der ikke indtastet et decimaltal
            }

            transactionEngine.WithdrawFromAccount(checkingAccount, amount);

            return true;
        }

        static bool Transfer(TransactionEngine transactionEngine, AccountRepository accountRepository, PrivateCustomer customer, CheckingAccount checkingAccount)
        {
            CheckingAccount targetAccount = ChooseAccount(customer, accountRepository);

            Console.WriteLine($"\n     Nuværende saldo på fra-kontonummer [{checkingAccount.AccountID}]: {checkingAccount.AccountBalance} {checkingAccount.AccountCurrency}");
            Console.WriteLine($"\n     Nuværende saldo på til-kontonummer [{targetAccount.AccountID}]: {targetAccount.AccountBalance} {targetAccount.AccountCurrency}");



            decimal amount = ChooseTransferAmount();
            transactionEngine.TransferToAccount(checkingAccount, targetAccount, amount);

            Console.WriteLine($"\n     Ny saldo på fra-kontonummer [{checkingAccount.AccountID}]: {checkingAccount.AccountBalance} {checkingAccount.AccountCurrency}");
            Console.WriteLine($"\n     Ny saldo på til-kontonummer [{targetAccount.AccountID}]: {targetAccount.AccountBalance} {targetAccount.AccountCurrency}");
            Console.WriteLine("\n    Tast retur for at komme tilbage til transaktionsmenu.");
            Console.ReadKey();
            Console.Clear();
            return true;
        }

        static decimal ChooseTransferAmount()
        {
            Console.Write("\n Indtast beløb der ønskes overført og tryk enter: ");
            string? amountString = Console.ReadLine();
            bool isValidAmount = decimal.TryParse(amountString, out decimal amount);

            if (!isValidAmount)
            {
                Console.WriteLine("Det skal være et decimal tal.");
                return ChooseTransferAmount();  //hvis ikke gyldigt decimal tal, vis prompt igen
                //finpudsning af program nødvendigt her, man kan ikke komme ud af if-sætning her, hvis der ikke bliver indtastet et decimaltal, no escape....
            }

            return amount;
        }

        static void DisplayTransactions(TransactionRepository transactionRepository, CheckingAccount account)
        {
            Console.WriteLine($"\n  Du valgte at få en oversigt over bevægelser på kontonummer [{account.AccountID}] ");

            Console.WriteLine("\n       Tidspunkt\t        Beløb");
            foreach (Transaction transaction in transactionRepository.GetTransactions(account.AccountID))
            {
                string formattedTransactionTime = transaction.TransactionTime.ToString("s").Replace('T', ' ');

                Console.WriteLine($"       {formattedTransactionTime}\t{(transaction.FromAccountID == account.AccountID ? -1: 1) * transaction.Amount}");

            }
            Console.WriteLine("\n Tast retur for at komme tilbage til transaktionsmenu.");
            Console.ReadKey();
            Console.Clear();
        }

    }




}


