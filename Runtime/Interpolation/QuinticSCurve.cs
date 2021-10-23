using System;

namespace Packages.GhostNoise.Interpolation
{
	internal class QuinticSCurve: SCurve
	{
		public override float Interpolate(float t)
		{
			var t3 = t * t * t;
			var t4 = t3 * t;
			var t5 = t4 * t;
			return 6 * t5 - 15 * t4 + 10 * t3;
		}
	}
}