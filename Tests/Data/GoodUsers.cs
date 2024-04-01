using System.Collections;
using InfrastructureOrm.Model;
using ModelDTO;

namespace RepositoryTests;

public class GoodUsers : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new List<object[]>
    {
        new object[] { "login","password"},
        new object[] {  "\\\\\\",".|.f," },
        new object[] { "rtewf","mm" },
        new object[] { "___","___" },
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}