using System.Collections;
using ModelDTO;

namespace RepositoryTests;

public class BadApplications: IEnumerable<object[]>
{
    private readonly List<object[]> _data = new List<object[]>
    {
        new object[] { new ApplicationDTO(null,null,"",null,null,DateTime.Now, Guid.NewGuid(),true)},
        new object[] { new ApplicationDTO(null,null,null,null,null,DateTime.Now, Guid.NewGuid(),true)},
        new object[] { new ApplicationDTO(null,null,null,null,"",DateTime.Now, Guid.NewGuid(),true)},
        new object[] { new ApplicationDTO(null,"lol",null,null,null,DateTime.Now, Guid.NewGuid(),true)},
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
