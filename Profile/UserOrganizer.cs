using System.Data.SqlTypes;

namespace Profile;

public interface IUserOrganizer
{
    public void AddUser(IUser account);
    public IUser GetUser(String account);
    
}

public class UserOrganizer: IUserOrganizer{
    
    public List<IUser> Users = new List<IUser>();
    private IUserStorage storage;
    public UserOrganizer()
    {
        Users = storage.Load();
    }
    public void AddUser(IUser account)
    {
        Users.Add(account);
    }
    
    public IUser GetUser(String name)
    {
        foreach (var user in Users)
        {
            if (user.Name == name)
            {
                return user;
            }
        }

        return null;
    }

}
