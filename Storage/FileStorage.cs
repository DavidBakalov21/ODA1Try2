namespace Storage;
using Profile;
using Account;
using System.IO;
using System.Collections.Generic;

public class FileUserStorage : IUserStorage
{
    public string FilePath { get; }

    public FileUserStorage(string filePath)
    {
        FilePath = filePath;
    }

    public void Save(List<IUser> users)
    {
        List<string> lines = new List<string>();

        foreach (var user in users)
        {
            // Add user info
            lines.Add($"USER,{user.Name},{user.Plan}");

            // Add user accounts
            foreach (var account in user.Organizer.Accounts)
            {
                lines.Add($"ACCOUNT,{account.Name},{account.Money},{account.Currency}");

                // Add transactions for each account
                foreach (var transaction in account.Transactions)
                {
                    lines.Add($"TRANSACTION,{transaction.Type},{transaction.Quantity},{transaction.AccountName},{transaction.Comment},{transaction.Date:O}");
                }
            }
        }

        File.WriteAllLines(FilePath, lines);
    }

    public List<IUser> Load()
    {
        List<IUser> users = new List<IUser>();
        if (!File.Exists(FilePath))
        {
            return users; // Return an empty list if the file doesn't exist
        }

        var lines = File.ReadAllLines(FilePath);

        IUser currentUser = null;
        IAccountOrganizer currentOrganizer = null;
        IAccount currentAccount = null;

        foreach (var line in lines)
        {
            var parts = line.Split(',');

            if (parts[0] == "USER")
            {
                // Create new user
                currentOrganizer = new IAccountOrganizer();
                currentUser = new User
                {
                    Name = parts[1],
                    Plan = Enum.Parse<Plan>(parts[2]),
                    Organizer = currentOrganizer
                };
                users.Add(currentUser);
            }
            else if (parts[0] == "ACCOUNT")
            {
                // Create a new account for the current user
                currentAccount = new Account
                {
                    Name = parts[1],
                    Money = double.Parse(parts[2]),
                    Currency = Enum.Parse<Currency>(parts[3]),
                    Transactions = new List<ITransaction>()
                };
                currentOrganizer.AddAccount(currentAccount);
            }
            else if (parts[0] == "TRANSACTION")
            {
                // Add a transaction to the current account
                var transaction = new Transaction(
                    parts[1],
                    double.Parse(parts[2]),
                    parts[3],
                    parts[4],
                    DateTime.Parse(parts[5])
                );
                currentAccount.AddTransaction(transaction);
            }
        }

        return users;
    }
}
