namespace Profile;
using Account;
public interface IUser
{
    Plan Plan {get; set;}
    String Name {get; set;}
    IAccountOrganizer Organizer {get; set;}
}
public class User: IUser
{
    public Plan Plan { get; set; }
    public string Name { get; set; }

    public IAccountOrganizer Organizer { get; set; }
    public User(String name, Plan plan, IAccountOrganizer organizer)
    {
        Name = name;
        Plan = plan;
        Organizer=organizer;
    }
}