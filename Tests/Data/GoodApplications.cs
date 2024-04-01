using System.Collections;
using ModelDTO;

namespace RepositoryTests;

public class GoodApplications: IEnumerable<object[]>
{
    private readonly List<object[]> _data = new List<object[]>
    {
        new object[] { new ApplicationDTO(Guid.NewGuid(),null,"name","description","plan",DateTime.Now, Guid.NewGuid(),true)},
        new object[] { new ApplicationDTO(Guid.NewGuid(),null,null,"description",null,DateTime.Now, Guid.NewGuid(),true)},
        new object[] { new ApplicationDTO(Guid.NewGuid(),null,null,null,"plan",DateTime.Now, Guid.NewGuid(),true)},
        new object[] { new ApplicationDTO(Guid.NewGuid(),"Report",null,null,null,DateTime.Now, Guid.NewGuid(),true)},
        new object[] {new ApplicationDTO(Guid.NewGuid(),"Report","name","description","plan",DateTime.Now, Guid.NewGuid(),true)}
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
