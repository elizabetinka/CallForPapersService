using System.Collections;
using InfrastructureOrm.Model;
using ModelDTO;

namespace RepositoryTests;

public class BadUsers : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new List<object[]>
    {
        new object[] { "",""},
        new object[] {  "\\\\\\","" },
        new object[] { "      ","mm" },
        new object[] { "","___" },
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}