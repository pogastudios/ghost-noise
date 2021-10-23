using System;

namespace Packages.GhostNoise.Generation.Modification
{
    /// <summary>
    /// This generator binarizes its source noise, returning only value 0 and 1. A constant treshold value is user for binarization. I.e. result will be 0 where source value is less than treshold,
    /// and 1 elsewhere.
    /// </summary>
    public class Binarize : Generator
    {
        private readonly Generator _src;
        private readonly float _thresh;

        ///<summary>
        /// Create new binarize generator
        ///</summary>
        ///<param name="source">Source generator</param>
        ///<param name="treshold">Treshold value</param>
        public Binarize(Generator source, float treshold)
        {
            _src = source;
            _thresh = treshold;
        }

        public override float GetValue(float x, float y, float z)
        {
            return _src.GetValue(x, y, z) > _thresh ? 1 : 0;
        }
    }
}