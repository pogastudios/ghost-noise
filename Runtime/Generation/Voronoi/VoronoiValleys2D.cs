namespace Packages.GhostNoise.Generation.Voronoi
{
	/// <summary>
	/// This generator creates a "valleys" Voronoi diargam, that returns difference between two closest distances. Resulting noise has highest value at control points and 0 away from control points.
	/// </summary>
	public class VoronoiValleys2D : VoronoiDiagramBase2D
	{
		/// <summary>
		/// Create new Voronoi diagram using seed. Control points will be obtained using random displacment seeded by supplied value
		/// </summary>
		/// <param name="seed">Seed value</param>
		public VoronoiValleys2D(int seed)
			: base(seed)
		{
		}

		
		protected override float GetResult(float min1, float min2, float min3)
		{
			return min2 - min1;
		}
	}
}