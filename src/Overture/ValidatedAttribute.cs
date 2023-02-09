using Overture.Data.String.Validity;
using Overture.Math.Pure.Numbers;

namespace Overture;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = true)]
public class ValidatedAttribute<T, V> : Attribute where V : Validity<T>
{

}

public class StringLength : ValidatedAttribute<string, LengthIsInClosedInterval>
{
    public StringLength(int lowerBound, int upperBound)
    {
        LengthIsInClosedInterval.UpperBound = (Integer)upperBound;
        LengthIsInClosedInterval.LowerBound = (Integer)lowerBound;
    }
}


