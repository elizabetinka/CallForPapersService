using System;

namespace ModelDTO;

public class AdminDTO : IPerson
{
    public Guid Id { get; }
    public string? Password { get; set; }
    public string? Login { get; set;}

    public AdminDTO(Guid id, string password, string login)
    {
        Password = password;
        Login = login;
    }
    public AdminDTO(){}
}