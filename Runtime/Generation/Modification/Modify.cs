using System;

namespace Packages.GhostNoise.Generation.Modification
{
	/// <summary>
	/// This generator takes a source generator and applies a function to its output.
	/// </summary>
	public class Modify: Generator
	{
		private Func<float, float> _mod;
		private Generator _src;

		///<summary>
		/// Create new generator
		///</summary>
		///<param name="source">Source generator</param>
		///<param name="modifier">Modifier function to apply</param>
		public Modify(Generator source, Func<float, float> modifier)
		{
			_src = source;
			_mod = modifier;
		}

		public override float GetValue(float x, float y, float z) => _mod(_src.GetValue(x, y, z));
	}
}