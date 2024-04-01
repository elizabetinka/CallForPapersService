using System;
using System.Collections.Generic;

namespace ModelDTO;

public class UserDTO : IPerson
{
    public Guid Id { get; }
    public string? Password { get; set; }
    
    public string? Login { get;set; }

    public IList<ApplicationDTO> Applications { get; set; }

    public UserDTO(Guid id, string password, string login, IList<ApplicationDTO> applications)
    {
        Id = id;
        Password = password;
        Login = login;
        Applications = applications;
    }
    
    public UserDTO(){}
}