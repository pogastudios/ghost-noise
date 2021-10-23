using System;

namespace Packages.GhostNoise.Interpolation
{
	///<summary>
	/// Linear interpolator is the fastest and has the lowest quality, only ensuring continuity of noise values, not their derivatives.
	///</summary>
	internal class LinearSCurve : SCurve
	{
		public override float Interpolate(float t) => t;
	}
}