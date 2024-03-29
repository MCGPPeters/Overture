﻿using Overture.Math.Pure.Algebra.Structure;

namespace Overture.Math.Pure.Numbers.ℤ;

public class Addition : Group<int>, Algebra.Operations.Addition
{
    public static int Identity => 0;

    public static Func<int, int, int> Combine => (x, y) => x + y;

    public static Func<int, int> Invert => a => -a;

}
