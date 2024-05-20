namespace Mette_miniBank.Interfaces
{
    internal interface ITransaction
    {
        public void Deposit(decimal amount);
        public void Withdraw(decimal amount);
        public int GetAccountId();
    }
}
