﻿using System.Collections.ObjectModel;
using Overture.Math.Pure.Algebra.Operations;
using Overture.Math.Pure.Algebra.Structure;

namespace Overture.Math.Pure.Algebra.Linear;


public interface VectorSpace<T, FAdd, FMul>
    where T : Field<T>
    where FAdd : Field<T>, Addition
    where FMul : Field<T>, Multiplication
{

}


public class Basis<T, FAdd, FMul>
    where T : Field<T>
    where FAdd : Field<T>, Addition
    where FMul : Field<T>, Multiplication
{
    public Basis(params T[] scalars) => Scalars = new ReadOnlyCollection<T>(scalars);

    private ReadOnlyCollection<T> Scalars { get; }

    public Vector<T, FAdd, FMul> Create() =>
        new Vector(new T[Scalars.Count]);

    private struct Vector : Vector<T, FAdd, FMul>
    {
        public ReadOnlyCollection<T> Elements { get; }

        public Vector(T[] elements) => Elements = new ReadOnlyCollection<T>(elements);
    }

}


