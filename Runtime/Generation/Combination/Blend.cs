using System;
using UnityEngine;

namespace Packages.GhostNoise.Generation.Combination
{
    /// <summary>
    /// This generator blends two noises together, using third as a blend weight. Note that blend weight's value is clamped to [0,1] range
    /// </summary>
    public class Blend : Generator
	{
		private readonly Generator _genA;
		private readonly Generator _genB;
		private readonly Generator _genWeight;

		///<summary>
		/// Create new blend generator
		///</summary>
		///<param name="a">First generator to blend (this is returned if weight==0)</param>
		///<param name="b">Second generator to blend (this is returned if weight==1)</param>
		///<param name="weight">Blend weight source</param>
		public Blend(Generator a, Generator b, Generator weight)
		{
			_genA = a;
			_genWeight = weight;
			_genB = b;
		}

		
		public override float GetValue(float x, float y, float z)
		{
			var w = Mathf.Clamp01(_genWeight.GetValue(x, y, z));
			return _genA.GetValue(x, y, z) * (1 - w) + _genB.GetValue(x, y, z) * w;
		}
	}
}