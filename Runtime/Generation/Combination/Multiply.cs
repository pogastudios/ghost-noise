using UnityEngine;

namespace Packages.GhostNoise.Generation.Combination
{
	/// <summary>
	/// Generator that multiplies two noise values
	/// </summary>
	public class Multiply: Generator
	{
		private readonly Generator _genA;
		private readonly Generator _genB;

		public Multiply(Generator a, Generator b)
		{
			_genA = a;
			_genB = b;
		}


		public override float GetValue(float x, float y, float z) => _genA.GetValue(x, y, z) * _genB.GetValue(x, y, z);
		public override float GetValue(Vector3 p) => _genA.GetValue(p) * _genB.GetValue(p);
	}
}