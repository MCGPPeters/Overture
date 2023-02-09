using Overture.Data;

namespace Overture.Domain.Data;

public class ValidationErrorException : Exception
{
    public Reason[] Reasons { get; }

    public ValidationErrorException(params Reason[] reasons)
    {
        Reasons = reasons;
    }
}
