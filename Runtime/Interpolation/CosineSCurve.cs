using System;

namespace Packages.GhostNoise.Interpolation
{
	internal class CosineSCurve: SCurve
	{
		public override float Interpolate(float t) => (float)((1 - Math.Cos(t * 3.1415927)) * .5);
	}
}