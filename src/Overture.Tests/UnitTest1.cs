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

[Validated<string, AllLettersAreLowerCase>]
public partial record struct Foo { }