using UnityEngine;

namespace Packages.GhostNoise.Generation.Modification
{
	///<summary>
	/// This generator modifies source noise by applying a curve transorm to it. Curves can be edited using Unity editor's CurveFields, or created procedurally.
	///</summary>
	public class Curve : Generator
	{
		private Generator _src;
		private AnimationCurve _curve;

		///<summary>
		/// Create a new curve generator
		///</summary>
		///<param name="source">Source generator</param>
		///<param name="curve">Curve to use</param>
		public Curve(Generator source, AnimationCurve curve)
		{
			_src = source;
			_curve = curve;
		}

	
		public override float GetValue(float x, float y, float z)
		{
			float v = _src.GetValue(x, y, z);
			return _curve.Evaluate(v);
		}
	}
}