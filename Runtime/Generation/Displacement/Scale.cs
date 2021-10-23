using UnityEngine;

namespace Packages.GhostNoise.Generation.Displacement
{
	///<summary>
	/// This generator scales its source by given vector.
	///</summary>
	public class Scale : Generator
	{
		private readonly Generator _src;
		private readonly float _scaleX;
		private readonly float _scaleY;
		private readonly float _scaleZ;

		///<summary>
		/// Create new scaling
		///</summary>
		///<param name="source">Source generator</param>
		///<param name="v">Scale value</param>
		public Scale(Generator source, Vector3 v)
			: this(source, v.x, v.y, v.z)
		{

		}
		///<summary>
		/// Create new scaling
		///</summary>
		///<param name="source">Source generator</param>
		///<param name="x">Scale amount along X axis</param>
		///<param name="y">Scale amount along Y axis</param>
		///<param name="z">Scale amount along Z axis</param>
		public Scale(Generator source, float x, float y, float z)
		{
			_src = source;
			_scaleZ = z;
			_scaleY = y;
			_scaleX = x;
		}

		public override float GetValue(float x, float y, float z) => _src.GetValue(x * _scaleX, y * _scaleY, z * _scaleZ);

	}
}