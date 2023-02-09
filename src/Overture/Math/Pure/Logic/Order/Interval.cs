using Overture.Data;

namespace Overture.Math.Pure.Logic.Order;

public interface Interval<T> : Set<T>
    where T : Order<T>
{
}
