using System;
using UnityEngine;

namespace Packages.GhostNoise.Generation.Displacement
{
    ///<summary>
    /// This generator translates its source by given vector.
    ///</summary>
    public class Translate : Generator
    {
        private readonly Generator m_Source;
        private readonly float _offsetX;
        private readonly float _offsetY;
        private readonly float _offsetZ;

        ///<summary>
        /// Create new translation
        ///</summary>
        ///<param name="source">Source generator</param>
        ///<param name="v">Translate value</param>
        public Translate(Generator source, Vector3 v)
            : this(source, v.x, v.y, v.z)
        {

        }
        ///<summary>
        /// Create new translation
        ///</summary>
        ///<param name="source">Source generator</param>
        ///<param name="x">Translate amount along X axis</param>
        ///<param name="y">Translate amount along Y axis</param>
        ///<param name="z">Translate amount along Z axis</param>
        public Translate(Generator source, float x, float y, float z)
        {
            m_Source = source;
            _offsetZ = z;
            _offsetY = y;
            _offsetX = x;
        }


        public override float GetValue(float x, float y, float z) => m_Source.GetValue(x + _offsetX, y + _offsetY, z + _offsetZ);
    }
}