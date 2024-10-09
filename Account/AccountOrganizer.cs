namespace Account;

public interface IAccountOrganizer{
    public void AddAccount(IAccount account);
    public IAccount GetAccount(String account);
    public List<IAccount> Accounts { get; }
}

class AccountsOrganizer: IAccountOrganizer{
    
    public List<IAccount> Accounts { get; private set; }= new List<IAccount>();

    public void AddAccount(IAccount account)
    {
        Accounts.Add(account);
    }

    public IAccount GetAccount(String name)
    {
        foreach (var account in Accounts)
        {
            if (account.Name == name)
            {
                return account;
            }
        }

        return null;
    }

}
