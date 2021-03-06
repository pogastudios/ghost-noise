using Packages.GhostNoise.Generation;
using Packages.GhostNoise.Generation.Combination;
using UnityEngine;

namespace Packages.GhostNoise
{
    /// <summary>
    /// The base class for any noise generator. 
    /// </summary>
    public abstract class Generator
    {
        #region API

        /// <summary>
        ///  Returns noise value at given point. 
        ///  </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="z">Z coordinate</param><returns>Noise value</returns>
        public abstract float GetValue(float x, float y, float z);

        /// <summary>
        ///  Returns noise value at given point. 
        ///  </summary>
        /// <param name="v">Point coordinates</param>
        public virtual float GetValue(Vector3 v) => GetValue(v.x, v.y, v.z);

        #endregion

        #region Operators

        ///<summary>
        /// Overloaded + 
        /// Returns new generator that sums these two
        ///</summary>
        ///<param name="g1"></param>
        ///<param name="g2"></param>
        ///<returns></returns>
        public static Generator operator +(Generator g1, Generator g2)
        {
            return new FunctionGenerator((x, y, z) => g1.GetValue(x, y, z) + g2.GetValue(x, y, z));
        }
        ///<summary>
        /// Overloaded + 
        /// Returns new generator that adds a constant value
        ///</summary>
        ///<param name="g1"></param>
        ///<param name="f"></param>
        ///<returns></returns>
        public static Generator operator +(Generator g1, float f)
        {
            return new FunctionGenerator((x, y, z) => g1.GetValue(x, y, z) + f);
        }
        ///<summary>
        /// Overloaded unary - 
        /// Returns inverse of argument generator
        ///</summary>
        ///<param name="g1"></param>
        ///<returns></returns>
        public static Generator operator -(Generator g1)
        {
            return -1 * g1;
        }
        ///<summary>
        /// Overloaded - 
        /// Returns new generator that subtracts second argument from first
        ///</summary>
        ///<param name="g1"></param>
        ///<param name="g2"></param>
        ///<returns></returns>
        public static Generator operator -(Generator g1, Generator g2)
        {
            return new FunctionGenerator((x, y, z) => g1.GetValue(x, y, z) - g2.GetValue(x, y, z)); ;
        }
        ///<summary>
        /// Overloaded - 
        /// Returns new generator that subtracts a constant value
        ///</summary>
        ///<param name="g1"></param>
        ///<param name="f"></param>
        ///<returns></returns>
        public static Generator operator -(Generator g1, float f)
        {
            return new FunctionGenerator((x, y, z) => g1.GetValue(x, y, z) - f);
        }
        ///<summary>
        /// Overloaded - 
        /// Returns new generator that subtracts generator from a constant value
        ///</summary>
        ///<param name="g1"></param>
        ///<param name="f"></param>
        ///<returns></returns>
        public static Generator operator -(float f, Generator g1)
        {
            return new FunctionGenerator((x, y, z) => f - g1.GetValue(x, y, z));
        }
        ///<summary>
        /// Overloaded *
        /// Returns new generator that multiplies these two
        ///</summary>
        ///<param name="g1"></param>
        ///<param name="g2"></param>
        ///<returns></returns>
        public static Generator operator *(Generator g1, Generator g2)
        {
            return new FunctionGenerator((x, y, z) => g1.GetValue(x, y, z) * g2.GetValue(x, y, z)); ;
        }
        ///<summary>
        /// Overloaded *
        /// Returns new generator that multiplies noise by a constant value
        ///</summary>
        ///<param name="g1"></param>
        ///<param name="f"></param>
        ///<returns></returns>
        public static Generator operator *(Generator g1, float f)
        {
            return new FunctionGenerator((x, y, z) => g1.GetValue(x, y, z) * f);
        }
        ///<summary>
        /// Overloaded /
        /// Returns new generator that divides values of argument generators. Beware of zeroes!
        ///</summary>
        ///<param name="g1"></param>
        ///<param name="g2"></param>
        ///<returns></returns>
        public static Generator operator /(Generator g1, Generator g2)
        {
            return new FunctionGenerator((x, y, z) => g1.GetValue(x, y, z) / g2.GetValue(x, y, z));
        }
        ///<summary>
        /// Overloaded /
        /// Returns new generator that divides noise by a constant value
        ///</summary>
        ///<param name="g1"></param>
        ///<param name="f"></param>
        ///<returns></returns>
        public static Generator operator /(Generator g1, float f)
        {
            return new FunctionGenerator((x, y, z) => g1.GetValue(x, y, z) / f);
        }
        ///<summary>
        /// Overloaded /
        /// Returns new generator that divides constant value by noise values
        ///</summary>
        ///<param name="g1"></param>
        ///<param name="f"></param>
        ///<returns></returns>
        public static Generator operator /(float f, Generator g1)
        {
            return new FunctionGenerator((x, y, z) => f / g1.GetValue(x, y, z));
        }

        ///<summary>
        /// Conversion operator. Float values may be implicitly converted to a generator that return this value
        ///</summary>
        ///<param name="f"></param>
        ///<returns></returns>
        public static implicit operator Generator(float f)
        {
            return new Constant(f);
        }

        #endregion
    }
}