using System;
using UnityEngine;

namespace Packages.GhostNoise.Generation.Displacement
{
    /// <summary>
    /// This generator rotates its source around origin.
    /// </summary>
    public class Rotate : Generator
    {
        private Generator _src;
        private Quaternion _rot;

        ///<summary>
        /// Create new rotation using a quaternion
        ///</summary>
        ///<param name="source">Source generator</param>
        ///<param name="rotation">Rotation</param>
        public Rotate(Generator source, Quaternion rotation)
        {
            _src = source;
            _rot = rotation;
        }

        ///<summary>
        /// Create new rotation using Euler angles
        ///</summary>
        ///<param name="source">Source generator</param>
        ///<param name="angleX">Rotation around X axis</param>
        ///<param name="angleY">Rotation around Y axis</param>
        ///<param name="angleZ">Rotation around Z axis</param>
        public Rotate(Generator source, float angleX, float angleY, float angleZ)
            : this(source, Quaternion.Euler(angleX, angleY, angleZ))
        {

        }


        public override float GetValue(float x, float y, float z)
        {
            Vector3 v = _rot * new Vector3(x, y, z);
            return _src.GetValue(v);
        }
    }
}