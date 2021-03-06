using System;
using UnityEngine;

namespace Packages.GhostNoise.Generation.Patterns
{
    ///<summary>
    /// Generates planes parallel to YZ plane. Resulting "noise" has value -1 on YZ plane, 1 at step distance, -1 at 2*step etc. 
    ///</summary>
    public class Planes : FunctionGenerator
    {
        ///<summary>
        /// Create new planes pattern
        ///</summary>
        ///<param name="step">step</param>
        ///<exception cref="ArgumentException">When step &lt;=0 </exception>
        public Planes(float step)
            : base((x, y, z) => MathHelpers.Saw(x / step))
        {
            if (step <= 0)
                throw new ArgumentException("Step must be > 0");
        }
    }
}