using System;

namespace Packages.GhostNoise.Interpolation
{
    internal class CubicSCurve : SCurve
    {
        public override float Interpolate(float t) => t * t * (3f - 2f * t);
    }
}