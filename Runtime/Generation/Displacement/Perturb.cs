using System;
using UnityEngine;

namespace Packages.GhostNoise.Generation.Displacement
{
	/// <summary>
	/// This generator perturbs its source, using a user-supplied function to obtain displacement values. In other words, <see cref="Perturb"/> nonuniformly displaces each value of
	/// its source.
	/// </summary>
	public class Perturb: Generator
	{
		private readonly Generator _src;
        private readonly Func<Vector3, Vector3> _displacementSrc;

		///<summary>
		/// Create new perturb generator
		///</summary>
		///<param name="source">Source generator</param>
		///<param name="displacementSource">Displacement generator</param>
        public Perturb(Generator source, Func<Vector3, Vector3> displacementSource)
		{
			_src = source;
			_displacementSrc = displacementSource;
		}

		
		public override float GetValue(float x, float y, float z)
		{
			Vector3 displacement = _displacementSrc(new Vector3(x, y, z));
			return _src.GetValue(x + displacement.x, y + displacement.y, z + displacement.z);
		}
	}
}