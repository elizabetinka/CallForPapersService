using System.Collections;
using CallForPapers.ServicesPresentationDto;


namespace RepositoryTests;

public class GoodApplications: IEnumerable<object[]>
{
    private readonly List<object[]> _data = new List<object[]>
    {
        new object[] { new CreateApplicationRequestDto(Guid.NewGuid(),null,"description","plan"," ")},
        new object[] { new  CreateApplicationRequestDto(Guid.NewGuid(),null,null,"description",null)},
        new object[] { new  CreateApplicationRequestDto(Guid.NewGuid(),null,null,null,"plan")},
        new object[] { new  CreateApplicationRequestDto(Guid.NewGuid(),"Report",null,null,null)},
        new object[] {new  CreateApplicationRequestDto(Guid.NewGuid(),"Report","name","description","plan")}
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
