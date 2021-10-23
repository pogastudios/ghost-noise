using UnityEngine;

namespace Packages.GhostNoise.Generation.Combination
{
	/// <summary>
	/// This generator returns minimum value of its two source generators
	/// </summary>
	public class Min : Generator
	{
		private readonly Generator _genA;
		private readonly Generator _genB;

		public Min(Generator a, Generator b)
		{
			_genA = a;
			_genB = b;
		}

		
		public override float GetValue(float x, float y, float z) => Mathf.Min(_genA.GetValue(x, y, z), _genB.GetValue(x, y, z));
		public override float GetValue(Vector3 p) => Mathf.Min(_genA.GetValue(p), _genB.GetValue(p));

	}
}