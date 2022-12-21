using Overture.Data.String.Validity;
using System.Net.Security;

namespace Overture.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {

    }

    
}
public record Moo
    {
        public required SslClientHelloInfo Foo {get;init;}
    }

//[Validated<string, AllLettersAreLowerCase>]
[Alias<string[]>]
public partial record struct Foo { }

[Alias<string[]>]
public readonly partial struct ValidationErrors { };