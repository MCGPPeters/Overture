using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overture;

/// <summary>
/// 
/// </summary>
public static class BackOff
{
    public static TimeSpan[] Exponentially(int maxNumberOfTimes = 3) => Enumerable
        .Range(1, maxNumberOfTimes)
        .Select(
            x =>
            {
                Random rnd = new();
                TimeSpan seconds = TimeSpan.FromSeconds(System.Math.Pow(2, x));
                // add random number of milliseconds to decrease the chance globally synchronized retries
                TimeSpan milliseconds = TimeSpan.FromMilliseconds(rnd.Next());

                return seconds.Add(milliseconds);

            }).ToArray();
}
