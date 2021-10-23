using System;

namespace Packages.GhostNoise.Generation
{
    /// <summary>
    /// This generator creates "noise" that is actually a function of coordinates. Use it to create regular patterns that are then perturbed by noise
    /// </summary>
    public class FunctionGenerator : Generator
    {
        private readonly Func<float, float, float, float> _func;

        /// <summary>
        /// Create new function generator
        /// </summary>
        /// <param name="func">Value function</param>
        public FunctionGenerator(Func<float, float, float, float> func)
        {
            _func = func;
        }

        public override float GetValue(float x, float y, float z) => _func(x, y, z);
    }
}