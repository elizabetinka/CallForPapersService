using System.Collections;
using CallForPapers.ServicesPresentationDto;

namespace RepositoryTests;

public class BadApplications: IEnumerable<object[]>
{
    private readonly List<object[]> _data = new List<object[]>
    {
        new object[] { new  CreateApplicationRequestDto(null,null,"",null,null)},
        new object[] { new  CreateApplicationRequestDto(null,null,null,null,null)},
        new object[] { new  CreateApplicationRequestDto(null,null,null,null,"")},
        new object[] { new  CreateApplicationRequestDto(null,"lol",null,null,null)},
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
