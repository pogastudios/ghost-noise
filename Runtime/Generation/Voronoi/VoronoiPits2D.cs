using System;

namespace Packages.GhostNoise.Generation.Voronoi
{
	/// <summary>
	/// This generator creates a "pits" Voronoi diargam, that simply returns distance to closest control point. Resulting noise has value 0 at control points (forming pits) and higher values away from control points.
	/// </summary>
	public class VoronoiPits2D : VoronoiDiagramBase2D
	{
		/// <summary>
		/// Create new Voronoi diagram using seed. Control points will be obtained using random displacment seeded by supplied value
		/// </summary>
		/// <param name="seed">Seed value</param>
		public VoronoiPits2D(int seed)
			: base(seed)
		{
		}

		#region Overrides of VoronoiDiagramBase

		/// <summary>
		/// Override this method to calculate final value using distances to closest control points.
		/// Note that distances that get passed to this function are adjusted for frequency (i.e. distances are scaled so that 
		/// control points are in unit sized cubes)
		/// </summary>
		/// <param name="min1">Distance to closest point</param>
		/// <param name="min2">Distance to second-closest point</param>
		/// <param name="min3">Distance to third-closest point</param>
		/// <returns></returns>
		protected override float GetResult(float min1, float min2, float min3)
		{
			return min1;
		}

		#endregion
	}
}