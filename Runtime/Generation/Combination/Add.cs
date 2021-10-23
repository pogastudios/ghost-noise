using UnityEngine;

namespace Packages.GhostNoise.Generation.Combination
{
    /// <summary>
    /// Generator that adds two noise values together
    /// </summary>
    public class Add : Generator
    {
        private readonly Generator _generatorA;
        private readonly Generator _generatorB;

        ///<summary>
        /// Create new generator
        ///</summary>
        ///<param name="a">First generator to add</param>
        ///<param name="b">Second generator to add</param>
        public Add(Generator a, Generator b)
        {
            _generatorA = a;
            _generatorB = b;
        }


        public override float GetValue(float x, float y, float z) => _generatorA.GetValue(x, y, z) + _generatorB.GetValue(x, y, z);
        public override float GetValue(Vector3 v) => _generatorA.GetValue(v) + _generatorB.GetValue(v);
    }
}