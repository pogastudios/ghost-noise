using UnityEngine;

namespace Packages.GhostNoise.Generation
{
    ///<summary>
    /// Constant "noise". This generator returns constant value, ignoring input coordinates. Used for arithmetic operations on noise generators
    ///</summary>
    public class Constant : Generator
    {
        private readonly float _value;

        ///<summary>
        /// Create new constant generator
        ///</summary>
        ///<param name="value">Value returned by generator</param>
        public Constant(float value)
        {
            _value = value;
        }

        /// <inheritdoc />
        public override float GetValue(float x, float y, float z) => _value;
        public override float GetValue(Vector3 pos) => _value;
    }
}