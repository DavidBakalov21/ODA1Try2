namespace Runner;

using Profile;
public interface ILoopRunner
{
    void RunLoop();
    CommandParser Parser { get; }
}


public class LoopRunner : ILoopRunner
{
    public ProfileOrganizer profileOrganizer { get; set; }
    private CommandExecutor Executor { get; }
    public LoopRunner(ProfileOrganizer organizer)
    {
        profileOrganizer = organizer;
        Executor = new CommandExecutor(); 
        //Executor.SetOrganizer(); <--- figure out what to do with the fact that this has AccOrganizer as a member var.
    }
    
    public CommandParser Parser { get; } = new CommandParser();
    public void RunLoop()
    {
        String userInput = "";
        profileOrganizer.accountsOrganizer.LoadAccounts();

        Console.WriteLine("Enter account name: ");
        userInput = Console.ReadLine();

        Account currentAccount = Organizer.GetAccount(userInput);
        if (currentAccount == null)
        {
            currentAccount = new Account(userInput, 0, Currency.USD);
            Organizer.AddAccount(currentAccount);
            Organizer.SaveAccounts();
        }
        Console.WriteLine($"\nLogged in {currentAccount.Name}\n");

        while (true) 
        {
            Console.WriteLine("Enter a command: ");
            userInput = Console.ReadLine();
            
            var argsList = Parser.getCommandArgs(userInput);

            if (userInput.StartsWith("add money"))
            {
                Executor.AddMoney(currentAccount, argsList);
            }
            else if (userInput.StartsWith("add account"))
            {
                Executor.AddAccount(argsList);
            }
            else if (userInput.StartsWith("spend money"))
            {
                Executor.Spend(currentAccount, argsList);
            }
            else if(userInput.StartsWith("info transaction"))
            {
                Executor.InfoTransactions();
            }
            else if (userInput.StartsWith("info accounts"))
            {
                Executor.InfoAccounts();
            }
            else if (userInput.StartsWith("export transactions"))
            {
                Executor.ExportTransactions();
            }
            else if (userInput.StartsWith("options"))
            {
                Executor.ShowActions();
            }
            else if (userInput.StartsWith("action info"))
            {
                Executor.ShowAccountData(argsList);
            }
            else if (userInput.StartsWith("action convert"))
            {
                Executor.Convert(argsList);
            }
            else if (userInput.StartsWith("action history"))
            {
                Executor.ShowActionHistory(argsList);
            }
            else if (userInput.StartsWith("exit"))
            {
                Console.WriteLine("Are you sure you want to exit? (y/n): ");
                userInput = Console.ReadLine();

                if (userInput.ToLower() == "y")
                {
                    break;
                }
            }
        } 
    }
}