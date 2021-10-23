using System;

namespace Packages.GhostNoise.Generation.Voronoi
{
    /// <summary>
    /// This generator creates a "pits" Voronoi diargam, that simply returns distance to closest control point. Resulting noise has value 0 at control points (forming pits) and higher values away from control points.
    /// </summary>
    public class VoronoiPits : VoronoiDiagramBase
    {
        /// <summary>
        /// Create new Voronoi diagram using seed. Control points will be obtained using random <see cref="GradientNoise"/> displacment seeded by supplied value
        /// </summary>
        /// <param name="seed">Seed value</param>
        public VoronoiPits(int seed) : base(seed)
        {
        }

        protected override float GetResult(float min1, float min2, float min3)
        {
            return min1;
        }
    }
}