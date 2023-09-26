using System.ComponentModel.DataAnnotations;

namespace HomeWorkEleven.Data.Models;

public class User
{
    public User(string name, int age, Role role)
    {
        Name = name;
        Age = age;
        Role = role;
    }

    public string Name { get; set; }

    [Required(ErrorMessage = "Please enter your date of birth")]
    public int Age { get; set; }

    public Role Role { get; set; }
}

public class Role
{
    public Role(string roleName)
    {
        RoleName = roleName;
    }

    public string RoleName { get; set; }
}