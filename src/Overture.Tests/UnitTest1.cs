using Overture.Data.String.Validity;
using System.Net.Security;

namespace Overture.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var v = ValidationErrors.Create(null!);
    }

    
}
public record Moo
    {
        public required SslClientHelloInfo Foo {get;init;}
    }

//[Validated<string, AllLettersAreLowerCase>]
[Alias<string[]>]
public partial record struct Foo { }

[Validated<string, IsNotNullEmptyOrWhiteSpace>]
public readonly partial record struct ValidationErrors { };