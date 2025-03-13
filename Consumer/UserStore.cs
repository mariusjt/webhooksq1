namespace Consumer;

public class UserStore
{
    private List<User> Users { get; set; } = [new() {Id = 0, Name = "Marius"}, new() {Id = 1, Name = "John"}];
    
    public List<User> GetUsers() => Users;
    public User GetUserById(int id) => Users.FirstOrDefault(u => u.Id == id);
    public User CreateUser(User user)
    {
        user.Id = Users.Max(u => u.Id) + 1;
        Users.Add(user);
        return user;
    }
    public User UpdateUser(User user)
    {
        var existingUser = Users.FirstOrDefault(u => u.Id == user.Id);
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }
        existingUser.Name = user.Name;
        return existingUser;
    }
    public void DeleteUser(int id)
    {
        var existingUser = Users.FirstOrDefault(u => u.Id == id);
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }
        Users.Remove(existingUser);
    }
}