
using UnityEngine;

namespace Packages.GhostNoise.Generation
{
    /// <summary>
    /// This generator returns its source unchanged. However, it caches last returned value, and does not recalculate it if called several times for the same point.
    /// This is handy if you use same noise generator in different places.
    /// 
    /// Note that displacement, fractal and Voronoi generators call GetValue at different points for their respective source generators.  
    /// This wil trash the Cache and negate any performance benefit, so there's no point in using Cache with these generators.
    /// </summary>
    public class Cache : Generator
    {
        private readonly Generator _source;

        private float _x;
        private float _y;
        private float _z;

        private Vector3 _pos;

        private float _cached;

        ///<summary>
        /// Create new caching generator
        ///</summary>
        ///<param name="source">Source generator</param>
        public Cache(Generator source)
        {
            _source = source;
        }

        /// <inheritdoc/>
        public override float GetValue(Vector3 v)
        {
            if (_pos == v)
                return _cached;

            _pos = v;
            return _cached = _source.GetValue(v);
        }

        /// <inheritdoc/>
        public override float GetValue(float x, float y, float z)
        {
            if (x == _x && y == _y && z == _z)
                return _cached;

            _x = x;
            _y = y;
            _z = z;
            return _cached = _source.GetValue(x, y, z);
        }
    }
}