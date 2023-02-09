using Overture.Data;
using static Overture.Control.Validated.Extensions;
using static Overture.Control.Nullable.Extensions;

namespace Overture.Domain.Data;

[Alias<Guid>]
public partial struct StreamId : Read<StreamId>
{
    public static Validated<StreamId> Parse(string s) =>
        Guid
            .TryParse(s, out var guid)
            .Map(succeeded => succeeded ? Valid((StreamId)guid) : Invalid<StreamId>($"The string '{s}' is not a valid Guid"))!;

    public static Validated<StreamId> Parse(string s, string validationErrorMessage) =>
        Guid
            .TryParse(s, out var guid)
            .Map(succeeded => succeeded ? Valid((StreamId)guid) : Invalid<StreamId>(validationErrorMessage))!;
}
