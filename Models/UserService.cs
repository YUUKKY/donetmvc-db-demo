using donetmvc_db_demo.Models;
using MySqlConnector;

namespace dotnetcoresample.Models;

public class UserService
{
    public List<User> CurrentUsers()
    {
        using var db = new UserContext();
        var users = db.Users.ToList();
        return users;
    }

    public void InsertUser(User user)
    {
        using var db = new UserContext();
        db.Users.Add(user);
        db.SaveChanges();
    }
}