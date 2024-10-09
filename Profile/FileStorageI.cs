namespace Profile;

public interface IUserStorage
{
    void Save(List<IUser> tasks);
    List<IUser> Load();
}