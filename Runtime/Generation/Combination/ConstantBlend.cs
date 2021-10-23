using UnityEngine;

namespace Packages.GhostNoise.Generation.Combination
{
    /// <summary>
    /// This generator blends two noises together, using a value;
    /// </summary>
    public class ConstantBlend : Generator
    {
		private readonly Generator _genA;
		private readonly Generator _genB;

        public float Weight { get; set; }

        public ConstantBlend(Generator a, Generator b)
		{
			_genA = a;
			_genB = b;
		}

		public override float GetValue(float x, float y, float z)
		{
			var w = Mathf.Clamp01(Weight);
			return _genA.GetValue(x, y, z) * (1 - w) + _genB.GetValue(x, y, z) * w;
		}
	}
}